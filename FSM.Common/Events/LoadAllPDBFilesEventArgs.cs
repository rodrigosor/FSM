namespace FSM.Common.Events
{
    public class LoadAllPDBFilesEventArgs
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
