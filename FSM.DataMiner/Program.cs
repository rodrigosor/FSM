using System;
using System.Linq;

namespace FSM.DataMiner
{
    class Program
    {
        static void Main(string[] args)
        {
            Repository.Instance.LoadPDBFile += OnLoadPDBFile;

            Console.WriteLine("Iniciando carregamento dos arquivos *.pdb");

            Repository.Instance.LoadPDBFilePaths();

            Console.ReadKey();
        }

        private static void OnLoadPDBFile(Common.Events.LoadPDBFilesEventArgs e)
        {
            Console.WriteLine("Arquivo {0} carregado na memória.", e.PDBFilePath);
        }
    }
}
