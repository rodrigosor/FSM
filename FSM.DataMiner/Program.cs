﻿using FSM.Repository;
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
            var respository = new PDBRespository();
            respository.LoadPDBData(@"C:\Libs\PDB");
        }
    }
}
