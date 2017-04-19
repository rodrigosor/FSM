using FSM.Common.Exceptions;
using FSM.Domain;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using FSM.Common;
using FSM.Common.Events;

namespace FSM.Repository
{
    public class PDBRepository
    {
        private string _pdbFilesPath;

        public event LoadPDBFilesEventHandler LoadPDBFile; 

        public IDictionary<string, IList<Atom>> PDB { get; private set; }

        public List<Atom> Atoms
        {
            get
            {
                return PDB.Values.SelectMany(atoms => atoms.Select(atom => atom)).ToList();
            }
        }

        public PDBRepository(string path)
        {
            PDB = new Dictionary<string, IList<Atom>>();
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

                    atom.RecordName = line.Slice<string>(1, 6);
                    atom.Serial = line.Slice<int>(7, 11);
                    atom.AtomName = line.Slice<string>(13, 16);
                    //atom.AlternateLocation = line.Slice<char>(17, 18);
                    atom.RecordName = line.Slice<string>(18, 20);
                    //atom.ChainID = line.Slice<char>(22, 23);
                    atom.ResidueSequenceNumber = line.Slice<int>(23, 26);
                    //atom.ICode = line.Slice<char>(27, 28);
                    atom.X = line.Slice<double>(31, 38);
                    atom.Y = line.Slice<double>(39, 46);
                    atom.Z = line.Slice<double>(47, 54);
                    atom.Occupancy = line.Slice<double>(55, 60);
                    atom.TemperatureFactor = line.Slice<double>(61, 66);
                    atom.ElementSymbol = line.Slice<string>(77, 78);
                    atom.Charge = line.Slice<string>(79, 80);

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

            PDB.Add(path.ToString(), buffer);
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
                }
            }
            catch (Exception ex)
            {
                throw new OperationCanceledException(
                        string.Format("Cannot read *.pdb files from \"{0}\"", _pdbFilesPath), ex
                    );
            }
        }
    }
}
