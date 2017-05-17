namespace AdventOfCodeTDD
{
    public interface IInstructionSet
    {
        string GetInstruction(int instructionPointer);
        void AddInstruction(string instruction);
    }
}