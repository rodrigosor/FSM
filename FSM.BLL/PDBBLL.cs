using FSM.DAL;
using FSM.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace FSM.BLL
{
    public sealed class PDBBLL
    {
        private PDBDAL _dal;
        private string _pdbFilePath;

        private static Lazy<PDBBLL> _lazyPDBRepository = new Lazy<PDBBLL>(
                () => new PDBBLL()
            );

        public static PDBBLL Instance
        {
            get
            {
                return _lazyPDBRepository.Value;
            }
        }

        private PDBBLL()
        {
            try
            {
                _dal = new PDBDAL();
                _pdbFilePath = ConfigurationManager.AppSettings["PDBFilesPath"];
            }
            catch (ConfigurationErrorsException cEx)
            {
                throw new OperationCanceledException("Please set the AppSetting entry with the *pdb files path.", cEx);
            }
        }

        private void WriteResultOnDisc(List<PDB> result)
        {
            var path = Path.Combine(
                        ConfigurationManager.AppSettings["PDBFilesPath"],
                        string.Format("Results\\{0:yyyyMMddHHmmss}\\", DateTime.Now)
                    );

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            foreach (var pdb in result)
            {
                var file = string.Concat(
                        path, Path.GetFileNameWithoutExtension(pdb.Path), ".txt"
                    );

                var buffer = new StringBuilder();

                foreach (var atom in pdb.Atoms)
                {
                    buffer.AppendLine(string.Format("{0}\t{1}\t{2}\t{3}\t{4}",
                            atom.Id, atom.Name, atom.X, atom.Y, atom.Z
                        ));
                }

                File.WriteAllText(file, buffer.ToString());
            }
        }

        public List<PDB> GetPDBFiles()
        {
            return _dal.GetPDBFiles(_pdbFilePath).ToList();
        }

        public List<PDB> GetCalculateMolecularInteractivityInterface(List<PDB> pdbFiles, bool writeResultOnDisc = false)
        {
            var result = _dal.GetCalculateMolecularInteractivityInterface(pdbFiles);
            if (writeResultOnDisc) this.WriteResultOnDisc(result);

            return result;
        }
    }
}
