using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWorkflow.Logger
{
    public interface ILogger
    {
        bool Log(string action, string data);
        Task<bool> LogAsync(string action, string data);
    }
   
}

