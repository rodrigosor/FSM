using FSM.Repository;
using System;
using System.Configuration;

namespace FSM.DataMiner
{
    public sealed class Repository
    {
        private static Lazy<PDBRepository> _lazyPDBRepository = new Lazy<PDBRepository>(
                BuildPDBRespositoryInstance
            );

        public static PDBRepository Instance
        {
            get
            {
                return _lazyPDBRepository.Value;
            }
        }

        private static PDBRepository BuildPDBRespositoryInstance()
        {
            var pdbRepository = new PDBRepository(
                    ConfigurationManager.AppSettings["PDBFilesPath"]
                );
            return pdbRepository;
        }
    }
}
