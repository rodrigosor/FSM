using FSM.Common;
using FSM.Common.Events;
using FSM.Common.Exceptions;
using FSM.Domain;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace FSM.Repository
{
    public class PDBRepository
    {
        public event LoadPDBFileToMemoryEventHandler LoadPDBFile;

        private string _pdbFilesPath;

        public IList<Atom> Atoms { get; private set; }

        public PDBRepository(string path)
        {
            Atoms = new List<Atom>();
            _pdbFilesPath= path;
        }

        private void LoadPDBFileToMemory(string path)
        {
            var lines = File.ReadAllText(path).Split('\n').Where(length => !string.IsNullOrWhiteSpace(length)).ToList();

            for (int i = 0; i < lines.Count(); ++i)
            {
                var line = lines[i];

                try
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        var atom = new Atom();

                        atom.RecordName = line.Slice<string>(1, 6);
                        atom.Serial = line.Slice<int>(7, 11);
                        atom.AtomName = line.Slice<string>(13, 16);
                        atom.AlternateLocation = line.Slice<char>(17, 18);
                        atom.RecordName = line.Slice<string>(18, 20);
                        atom.ChainID = line.Slice<char>(22, 23);
                        atom.ResidueSequenceNumber = line.Slice<int>(23, 26);
                        atom.ICode = line.Slice<char>(27, 28);
                        atom.X = line.Slice<double>(31, 38);
                        atom.Y = line.Slice<double>(39, 46);
                        atom.Z = line.Slice<double>(47, 54);
                        atom.Occupancy = line.Slice<double>(55, 60);
                        atom.TemperatureFactor = line.Slice<double>(61, 66);
                        atom.ElementSymbol = line.Slice<string>(77, 78);
                        atom.Charge = line.Slice<string>(79, 80);

                        Atoms.Add(atom);

                        LoadPDBFile?.Invoke(new LoadPDBFileToMemoryEventArgs(path, lines.Count(), i));
                    }
                }
                catch (Exception ex)
                {
                    throw new LoadPDBFileToMemoryException(
                            line, ex.Message, ex
                        );
                }
            }
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
