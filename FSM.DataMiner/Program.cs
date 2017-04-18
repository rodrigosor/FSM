using FSM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSM.DataMiner
{
    class Program
    {
        static void Main(string[] args)
        {
            Repository.Instance.LoadPDBFile += OnLoadPDBFile;
            Repository.Instance.LoadPDBFilePaths();
        }

        private static void OnLoadPDBFile(Common.Events.LoadPDBFileToMemoryEventArgs e)
        {
            Console.Clear();
            Console.Write("Carregando arquivo {0} ({1}/{2}) ...", e.PDBFilePath, e.Length, e.Loaded);
        }
    }
}
