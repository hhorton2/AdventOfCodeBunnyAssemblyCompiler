using NUnit.Framework;

namespace AdventOfCodeTDD.Tests
{
    public class InstructionSet_should_
    {
        private IInstructionSet GenerateInstructionSet()
        {
            return new InstructionSet();
        }
        [Test]
        public void return_an_instruction_when_get_instruction_called()
        {
            var instructionSet = GenerateInstructionSet();
            var result = instructionSet.GetInstruction(0);

            Assert.AreNotEqual(null, result);
        }


        [Test]
        public void return_an_empty_instruction_if_instruction_pointer_is_higher_than_instruction_count()
        {
            var instructionSet = GenerateInstructionSet();
            instructionSet.AddInstruction("inc A");
            var result = instructionSet.GetInstruction(21);

            Assert.AreEqual("", result);
        }

        [Test]
        public void return_correct_instruction_when_adding_to_instruction_set()
        {
            var instructionSet = GenerateInstructionSet();
            instructionSet.AddInstruction("inc A");
            var result = instructionSet.GetInstruction(0);

            Assert.AreEqual("inc A", result);

        }

        [Test]
        public void return_empty_instruction_if_instruction_pointer_is_negative()
        {
            var instructionSet = GenerateInstructionSet();
            instructionSet.AddInstruction("inc A");
            var result = instructionSet.GetInstruction(-1);
          
            Assert.AreEqual("", result );
        }
        

        
    }
}