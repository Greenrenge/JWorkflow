using System.Collections;
using System.Collections.Generic;

namespace JWorkflow.Scaffolding
{
    public interface IWFFlow
    {
        string FlowName { get; }
        string PassedStage { get; set; }
        string AwaitingStage { get; set; }
        short CurrentVersion { get; set; }
        ICollection<IWFFlowRecord> Records { get; set; }
    }
}