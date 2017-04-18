using System;

namespace FSM.Common.Events
{
    public class LoadPDBFilesEventArgs : EventArgs
    {
        public string PDBFilePath { get; private set; }

        public LoadPDBFilesEventArgs(string pdbFilePath)
        {
            PDBFilePath = pdbFilePath;
        }
    }
}
