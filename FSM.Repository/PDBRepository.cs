using FSM.Common.Exceptions;
using FSM.Domain;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using FSM.Common;
using FSM.Common.Events;
using FSM.Common.Maths;

namespace FSM.Repository
{
    public class PDBRepository
    {
        private string _pdbFilesPath;

        public event LoadPDBFilesEventHandler LoadPDBFile;

        public event LoadAllPDBFilesEventHandler LoadComplete;

        public List<PDB> PDB { get; private set; }

        public List<Atom> Atoms
        {
            get
            {
                return PDB.SelectMany(pdb => pdb.Atoms).ToList();
            }
        }

        public PDBRepository(string path)
        {
            PDB = new List<PDB>();
            _pdbFilesPath = path;
        }

        private void ProcessLineQueued(dynamic param)
        {
            param.Buffer.Add(ProcessLine(param.Line));
        }

        private Atom ProcessLine(string line)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    var atom = new Atom();

                    atom.Type = line.Slice<string>(1, 6).ToEnum<AtomType>();
                    atom.Id = line.Slice<int>(7, 11);
                    atom.Name = line.Slice<string>(13, 16);
                    atom.Residue = line.Slice<string>(18, 20);
                    atom.Chain = line.Slice<char>(22, 22);
                    atom.X = line.Slice<double>(31, 38);
                    atom.Y = line.Slice<double>(39, 46);
                    atom.Z = line.Slice<double>(47, 54);

                    return atom;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new LoadPDBFileToMemoryException(
                        line, ex.Message, ex
                    );
            }
        }

        private void LoadPDBFileToMemory(object path)
        {
            var buffer = new List<Atom>();

            var lines = File.ReadAllLines(path.ToString()).Where(line => line.Length > 0).ToArray();

            for (int i = 0; i < lines.Length; i++)
            {
                buffer.Add(ProcessLine(lines[i]));
            }

            PDB.Add(new PDB(path.ToString(), buffer));
        }

        public void LoadPDBFilePaths()
        {
            try
            {
                if (Directory.Exists(_pdbFilesPath))
                {
                    var pdbFilePaths = Directory.EnumerateFiles(_pdbFilesPath, "*.pdb", SearchOption.AllDirectories);

                    foreach (var pdbFilePath in pdbFilePaths)
                    {
                        LoadPDBFileToMemory(pdbFilePath);

                        LoadPDBFile?.Invoke(new LoadPDBFilesEventArgs(pdbFilePath));
                    }

                    LoadComplete?.Invoke(new LoadAllPDBFilesEventArgs(PDB.Count(), Atoms.Count()));
                }
            }
            catch (Exception ex)
            {
                throw new OperationCanceledException(
                        string.Format("Cannot read *.pdb files from \"{0}\"", _pdbFilesPath), ex
                    );
            }
        }

        public IList<PDB> CalculateMolecularInteractivityInterface()
        {
            var result = new List<PDB>(PDB);

            result.Clear();

            foreach (var pdb in PDB)
            {
                var atoms = pdb.Atoms.Where(
                        atom => atom.Type.Equals(AtomType.ATOM)
                    );
                var hetatoms = pdb.Atoms.Where(
                        atom => atom.Type.Equals(AtomType.HETATM)
                    );

                foreach (var atom in atoms)
                {
                    foreach (var hetatom in hetatoms)
                    {
                        var distance = Formulas.EuclideanDistance(atom, hetatom);

                        if (distance <= 7.0d)
                        {
                            pdb.Atoms.Add(atom);
                        }
                    }
                }

                if (pdb.HasAtoms)
                {
                    result.Add(pdb);
                }
            }

            return result;
        }
    }
}
