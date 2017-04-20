namespace FSM.Domain
{
    public partial class PDB
    {
        public bool HasAtoms
        {
            get
            {
                return (Atoms.Count > 0);
            }
        }

        public void Clear()
        {
            Atoms.Clear();
        }
    }
}
