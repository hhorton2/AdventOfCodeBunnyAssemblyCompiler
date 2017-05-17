using System;
using System.Collections.Generic;

namespace AdventOfCodeTDD
{
    class BunnyPasswordExtractor
    {
        private readonly IInstructionSet _instructionSet;

        static void Main(string[] args)
        {
            var lines = System.IO.File.ReadAllLines(@"C:\Users\hhorton\Desktop\input.txt");
            var instructions = new InstructionSet();
            foreach (var line in lines)
            {
                instructions.AddInstruction(line);
            }
            var bpe = new BunnyPasswordExtractor(instructions);
            Console.WriteLine(bpe.ExecuteInstructions());
            Console.WriteLine("Press any key to exit.");
            bpe.ExecuteInstruction("inc A");
            System.Console.ReadKey();
        }

        private readonly Dictionary<string, int> _registers = new Dictionary<string, int>();

        public BunnyPasswordExtractor(IInstructionSet instructionSet = null)
        {
            _instructionSet = instructionSet;
        }


        private void Increment(string register)
        {
            _registers[register] = GetRegisterValue(register) + 1;
            _registers["IP"] = GetRegisterValue("IP") + 1;
        }


        private void Decrement(string register)
        {
            _registers[register] = GetRegisterValue(register) - 1;
            _registers["IP"] = GetRegisterValue("IP") + 1;
        }


        public int GetRegisterValue(string register)
        {
            return _registers.ContainsKey(register) ? _registers[register] : 0;
        }

        private void CopyRegister(string from, string to)
        {
            _registers[to] = GetRegisterValue(from);
            _registers["IP"] = GetRegisterValue("IP") + 1;
        }

        private void CopyRegister(int value, string to)
        {
            _registers[to] = value;
            _registers["IP"] = GetRegisterValue("IP") + 1;
        }


        private void Jump(string register, int valueJump)
        {
            if (GetRegisterValue(register) != 0)
            {
                var currentIP = GetRegisterValue("IP");
                _registers["IP"] = currentIP + valueJump;
            }
            else
            {
                _registers["IP"] = GetRegisterValue("IP") + 1;
            }
        }

        private void Jump(int value, int valueJump)
        {
            if (value != 0)
            {
                var currentIP = GetRegisterValue("IP");
                _registers["IP"] = currentIP + valueJump;
            }
            else
            {
                _registers["IP"] = GetRegisterValue("IP") + 1;
            }
        }



        public void ExecuteInstruction(string instruction)
        {
            var newInstruction = instruction.Split(' ');


            if (newInstruction.Length <= 0) return;

            string command = newInstruction[0];
            if (command == "inc" && newInstruction.Length == 2) Increment(newInstruction[1]);
            else if (command == "dec" && newInstruction.Length == 2) Decrement(newInstruction[1]);
            else if (command == "jnz" && newInstruction.Length == 3)
                if (char.IsNumber(newInstruction[1][0]))
                {
                   Jump(int.Parse(newInstruction[1]), int.Parse(newInstruction[2]));
                }
                else
                {
                    Jump(newInstruction[1], int.Parse(newInstruction[2]));
                }
       
            else if (command == "cpy" && newInstruction.Length == 3)
            {
                if (char.IsNumber(newInstruction[1][0]))
                    CopyRegister(int.Parse(newInstruction[1]), newInstruction[2]);
                else
                    CopyRegister(newInstruction[1], newInstruction[2]);
            }
        }


        public int ExecuteInstructions()
        {
            string instruction = null;
            while ("" != (instruction = _instructionSet.GetInstruction(GetRegisterValue("IP"))))
            {
                Console.WriteLine(instruction);
                ExecuteInstruction(instruction);
            }
            return GetRegisterValue("A");
        }
    }
}