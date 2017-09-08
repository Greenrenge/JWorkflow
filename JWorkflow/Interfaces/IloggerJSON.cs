using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWorkflow.Logger
{
    public interface ILoggerJSON : ILogger
    {
        bool LogJSON(string action, object data);
        //Task<bool> LogJSONAsync(string action, object data);
    }
}
