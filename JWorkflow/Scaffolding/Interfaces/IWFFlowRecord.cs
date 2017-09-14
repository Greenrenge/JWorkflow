using System;
using System.Collections;
using System.Collections.Generic;

namespace JWorkflow.Scaffolding
{
    public interface IWFFlowRecord
    {
        IWFUser ActionByUser { get; set; }
        string Action { get; set; }
        string ActionForStage { get; set; }
        string PreviousStage { get; set; }
        string RejectToStage { get; set; }
        DateTime ActionDate { get; set; }
        int FlowVersion { get; set; }
    }
}