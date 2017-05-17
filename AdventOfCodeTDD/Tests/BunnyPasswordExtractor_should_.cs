using Moq;
using NUnit.Framework;

namespace AdventOfCodeTDD.Tests
{
    public class BunnyPasswordExtractor_should_
    {
        private BunnyPasswordExtractor GenerateBunnyPasswordExtractorWithInstructionSet(string[] instructionsStrings)
        {
            var instructions = new Mock<IInstructionSet>();
            instructions.Setup(i => i.GetInstruction(It.IsAny<int>())).Returns((int i) =>
            {
                return (i < instructionsStrings.Length && i >= 0) ? instructionsStrings[i] : "";
            });
            return new BunnyPasswordExtractor(instructions.Object);
        }

        private BunnyPasswordExtractor GenerateBunnyPasswordExtractor()
        {
            return new BunnyPasswordExtractor();
        }

        [Test]
        public void initialize_registers_to_zero()
        {
            var bpe = GenerateBunnyPasswordExtractor();
            Assert.AreEqual(0, bpe.GetRegisterValue("A"));
        }

        [Test]
        public void increment_register()
        {
            var bpe = GenerateBunnyPasswordExtractor();
            var register = "A";

            int before = bpe.GetRegisterValue(register);
            bpe.ExecuteInstruction("inc " + register);
            int after = bpe.GetRegisterValue(register);

            Assert.AreEqual(before + 1, after);

        }

        [Test]
        public void decrement_register()
        {
            var bpe = GenerateBunnyPasswordExtractor();
            var register = "A";

            int before = bpe.GetRegisterValue(register);
            bpe.ExecuteInstruction("dec " + register);
            int after = bpe.GetRegisterValue(register);

            Assert.AreEqual(before - 1, after);

        }

        [Test]
        public void copy_register_value_to_second_register()
        {
            var bpe = GenerateBunnyPasswordExtractor();
            var originRegister = "A";
            var destinationRegister = "B";

            bpe.ExecuteInstruction("inc A");

            int original = bpe.GetRegisterValue(originRegister);
            bpe.ExecuteInstruction($"cpy {originRegister} {destinationRegister}");
          
            Assert.AreEqual(original, bpe.GetRegisterValue(destinationRegister));

        }

        [Test]
        public void not_jump_if_register_is_zero()
        {
            var bpe = GenerateBunnyPasswordExtractor();

            var beforeJumpedPointerValue = bpe.GetRegisterValue("IP");
            bpe.ExecuteInstruction("jnz A 2");
            var afterJumpedPointerValue = bpe.GetRegisterValue("IP");

            Assert.AreEqual(beforeJumpedPointerValue + 1, afterJumpedPointerValue);
        }

        [Test]
        public void jump_if_register_is_not_zero()
        {
            var bpe = GenerateBunnyPasswordExtractor();
            var registerNotZero = "A";
            bpe.ExecuteInstruction("inc " + registerNotZero);

            var beforeJumpedPointerValue = bpe.GetRegisterValue("IP");
            var offset = -1;
            bpe.ExecuteInstruction("jnz " + registerNotZero + " " + offset);
            var afterJumpedPointerValue = bpe.GetRegisterValue("IP");

            Assert.AreEqual(beforeJumpedPointerValue + offset, afterJumpedPointerValue);
        }

        [Test]
        public void jump_if_literal_value_is_not_zero()
        {
            var bpe = GenerateBunnyPasswordExtractor();
            var beforeJumpedPointerValue = bpe.GetRegisterValue("IP");
            var offset = -1;
            bpe.ExecuteInstruction("jnz 1 " + offset);
            var afterJumpedPointerValue = bpe.GetRegisterValue("IP");

            Assert.AreEqual(beforeJumpedPointerValue + offset, afterJumpedPointerValue);
        }

        [Test]
        public void return_register_A_when_reaches_end_of_instructions()
        {
            string[] instructions = new string[0];
            var bpe = GenerateBunnyPasswordExtractorWithInstructionSet(instructions);

            bpe.ExecuteInstruction("inc A");
            var endValue = bpe.ExecuteInstructions();

            Assert.AreEqual(1, endValue);
        }

        [Test]
        public void register_A_after_executing_instuctions()
        {
            string[] instructionStrings = new[] {"cpy 41 A", "inc A", "inc A",  "dec A", "jnz A 2", "dec A"};

            var bpe = GenerateBunnyPasswordExtractorWithInstructionSet(instructionStrings);

            var A = bpe.ExecuteInstructions();

           Assert.AreEqual(42, A);
        }
   

        [Test]
        public void copy_literal_value_to_register()
        {
            var bpe = GenerateBunnyPasswordExtractor();
            var originLiteral = 41;
            var destinationRegister = "B";

            bpe.ExecuteInstruction($"cpy {originLiteral} {destinationRegister}");

            Assert.AreEqual(originLiteral, bpe.GetRegisterValue(destinationRegister));

        }

    }
}
