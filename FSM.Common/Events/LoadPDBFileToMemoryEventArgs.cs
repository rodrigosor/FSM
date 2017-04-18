using System;

namespace FSM.Common.Events
{
    public class LoadPDBFileToMemoryEventArgs : EventArgs
    {
        public string PDBFilePath { get; private set; }

        public int Length { get; private set; }

        public int Loaded { get; private set; }

        public LoadPDBFileToMemoryEventArgs(string pdbFilePath, int length, int loaded)
        {
            PDBFilePath = pdbFilePath;
            Length = length;
            Loaded = loaded;
        }
    }
}
