using System;

namespace FSM.Common.Events
{
    public class LoadAllPDBFilesEventArgs : EventArgs
    {
        public int PDBFilesLoaded { get; private set; }
        public int AtomsLoaded { get; set; }

        public LoadAllPDBFilesEventArgs(int pdbFilesLoaded, int atomsLoaded)
        {
            PDBFilesLoaded = pdbFilesLoaded;
            AtomsLoaded = atomsLoaded;
        }
    }
}
