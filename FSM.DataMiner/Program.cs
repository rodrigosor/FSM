using System;
using System.Linq;

namespace FSM.DataMiner
{
    class Program
    {
        static void Main(string[] args)
        {            
            Repository.Instance.LoadPDBFilePaths();

            Console.WriteLine(Repository.Instance.PDB.Count());

            Console.ReadKey();
        }
    }
}
