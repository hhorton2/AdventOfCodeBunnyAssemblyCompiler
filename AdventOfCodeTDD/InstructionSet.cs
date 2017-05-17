using System.Collections.Generic;
using NUnit.Framework;

namespace AdventOfCodeTDD
{
    public class InstructionSet: IInstructionSet
    {
        private readonly List<string> _instructions = new List<string>();


        public string GetInstruction(int instructionPointer)
        {
            string instruction = "";
            if (_instructions.Count >= 0 && instructionPointer < _instructions.Count && instructionPointer >= 0)
            {
                instruction = _instructions[instructionPointer];
            }
            return instruction;
        }

        public void AddInstruction(string instruction)
        {
            if (isValidInstruction(instruction))
            {
                _instructions.Add(instruction);
            }
        }

        private bool isValidInstruction(string instruction)
        {
            return !string.IsNullOrEmpty(instruction);
        }
    }
}