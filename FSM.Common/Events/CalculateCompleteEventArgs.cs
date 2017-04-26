using FSM.Domain;
using System;
using System.Collections.Generic;

namespace FSM.Common.Events
{
    public class CalculateCompleteEventArgs : EventArgs
    {
        public List<PDB> Result { get; private set; }

        public CalculateCompleteEventArgs(List<PDB> result)
        {
            Result = result;
        }
    }
}
