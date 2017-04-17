using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSM.Repository
{
    public class PDBRespository
    {
        public void LoadPDBData(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    var pdbFiles = Directory.EnumerateFiles(
                            path,
                            "*.pdb",
                            SearchOption.AllDirectories
                        );
                }
            }
            catch (Exception ex)
            {
                throw new OperationCanceledException(
                        string.Format("Cannot read *.pdb files from \"{0}\"", path), ex
                    );
            }
        }
    }
}
