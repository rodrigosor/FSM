using System.Collections.Generic;

namespace FSM.Domain
{
    public class PDB
    {
        public string Path { get; private set; }
        public IList<Atom> Atoms { get; private set; }

        public PDB(string path, IList<Atom> atoms)
        {
            Path = path;
            Atoms = atoms;
        }
    }
}
