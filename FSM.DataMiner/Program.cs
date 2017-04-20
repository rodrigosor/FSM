﻿using FSM.Common.Events;
using System;
using System.Linq;

namespace FSM.DataMiner
{
    class Program
    {
        static void Main(string[] args)
        {
            Repository.Instance.LoadComplete += OnLoadComplete;

            Console.WriteLine("FSM v.0.0.1");
            Console.WriteLine("Iniciando carregamento dos arquivos *.pdb");

            Repository.Instance.LoadPDBFilePaths();
        }

        private static void OnLoadComplete(LoadAllPDBFilesEventArgs e)
        {
            Console.Clear();
            Console.WriteLine("{0} arquivo(s) *.pdb carregado(s) na memória.", e.PDBFilesLoaded);
        }

    }
}
