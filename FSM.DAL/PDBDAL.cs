using FSM.Common.Exceptions;
using FSM.Domain;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using FSM.Common.Maths;

namespace FSM.DAL
{
    public class PDBDAL
    {
        private Atom ProcessLine(string line)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    return new Atom()
                    {
                        Type = line.Slice<string>(1, 6).ToEnum<AtomType>(),
                        Id = line.Slice<int>(7, 11),
                        Name = line.Slice<string>(13, 16),
                        Residue = line.Slice<string>(18, 20),
                        Chain = line.Slice<char>(22, 22),
                        X = line.Slice<double>(31, 38),
                        Y = line.Slice<double>(39, 46),
                        Z = line.Slice<double>(47, 54)
                    };
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

        private PDB LoadPDBFileToMemory(object path)
        {
            var buffer = new List<Atom>();

            var lines = File.ReadAllLines(path.ToString()).Where(line => line.Length > 0).ToArray();

            for (int i = 0; i < lines.Length; i++)
            {
                buffer.Add(ProcessLine(lines[i]));
            }

            return (new PDB(path.ToString(), buffer));
        }

        public IEnumerable<PDB> GetPDBFiles(string path)
        {
            if (Directory.Exists(path))
            {
                var pdbFilePaths = Directory.EnumerateFiles(path, "*.pdb", SearchOption.AllDirectories);

                foreach (var pdbFilePath in pdbFilePaths)
                {
                    yield return LoadPDBFileToMemory(pdbFilePath);
                }
            }
        }

        public List<PDB> GetCalculateMolecularInteractivityInterface(List<PDB> pdbFiles)
        {
            var result = new List<PDB>(pdbFiles);

            result.Clear();

            foreach (var pdb in pdbFiles)
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
