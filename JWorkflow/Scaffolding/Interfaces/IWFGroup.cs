using System.Collections.Generic;

namespace JWorkflow.Scaffolding
{
    public interface IWFGroup
    {
        string GroupName { get; }
        ICollection<IWFUserAction> OwnedBy { get; }
    }
}