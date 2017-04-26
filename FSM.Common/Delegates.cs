using FSM.Common.Events;

namespace FSM.Common
{
    public delegate void LoadPDBFilesEventHandler(LoadPDBFilesEventArgs e);

    public delegate void LoadAllPDBFilesEventHandler(LoadAllPDBFilesEventArgs e);

    public delegate void CalculateCompleteEventHandler(CalculateCompleteEventArgs e);
}
