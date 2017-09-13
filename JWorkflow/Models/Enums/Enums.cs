using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWorkflow.Models
{
    public enum NODETYPE
    {
        SYSTEM,
        SUBMIT,
        APPROVE,
    }
    public enum PermissionType
    {
        GROUP,
        INDIVIDUAL,
    }
    public enum FLOWACTION
    {
        SUBMIT,
        APPROVE,
        REJECT,
    }
}
