using System.Collections;
using System.Collections.Generic;

namespace JWorkflow.Scaffolding
{
    public interface IWFUser
    {
        string UserName { get; }
        ICollection<IWFUserAction> Claims { get; }
    }
}