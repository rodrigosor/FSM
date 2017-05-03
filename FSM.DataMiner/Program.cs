using FSM.BLL;
using FSM.Common.Events;
using System;
using System.Configuration;
using System.IO;
using System.Text;

namespace FSM.DataMiner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("FSM v.0.0.1");
            Console.WriteLine("Iniciando carregamento dos arquivos *.pdb...");
            var pdbFiles = PDBBLL.Instance.GetPDBFiles();

            Console.WriteLine("Arquivos carregados executando cálculos...");
            var result = PDBBLL.Instance.GetCalculateMolecularInteractivityInterface(pdbFiles, writeResultOnDisc: true);

            Console.WriteLine("Cálculos finalizados arquivos de resultados foram salvos.");
            //Repository.Instance.LoadComplete += OnLoadComplete;
            //Repository.Instance.CalculateComplete += OnCalculateComplete;
            //Repository.Instance.LoadPDBFilePaths();

            Console.ReadKey();
        }

        //private static void OnLoadComplete(LoadAllPDBFilesEventArgs e)
        //{
        //    Console.Clear();
        //    Console.WriteLine("{0} arquivo(s) *.pdb carregado(s) na memória.", e.PDBFilesLoaded);
        //    Console.WriteLine("Calculando Interface de Interação Molecular.");

        //    Repository.Instance.CalculateMolecularInteractivityInterface();
        //}

        //private static void OnCalculateComplete(CalculateCompleteEventArgs e)
        //{
        //    Console.Clear();
        //    Console.WriteLine("Gravando arquivos de saida.");

        //    var path = Path.Combine(
        //                ConfigurationManager.AppSettings["PDBFilesPath"],
        //                string.Format("Results\\{0:yyyyMMddHHmmss}\\", DateTime.Now)
        //            );

        //    if (!Directory.Exists(path))
        //    {
        //        Directory.CreateDirectory(path);
        //    }

        //    foreach (var pdb in e.Result)
        //    {
        //        var file = string.Concat(
        //                path, Path.GetFileNameWithoutExtension(pdb.Path), ".txt"
        //            );

        //        var buffer = new StringBuilder();

        //        foreach (var atom in pdb.Atoms)
        //        {
        //            buffer.AppendLine(string.Format("{0}\t{1}\t{2}\t{3}\t{4}",
        //                    atom.Id, atom.Name, atom.X, atom.Y, atom.Z
        //                ));
        //        }

        //        File.WriteAllText(file, buffer.ToString());
        //        Console.WriteLine("Arquivo {0} gravado com sucesso.", file);
        //    }

        //    Console.WriteLine("Gravação concluída.");
        //}
    }
}
