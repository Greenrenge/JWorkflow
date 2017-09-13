using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWorkflow
{
    public class WorkFlowConditionConfig<T> where T : class, new()
    {
        private IDictionary<string, Func<T, bool>> _conditionDictionary;
        public WorkFlowConditionConfig()
        {
            _conditionDictionary = new Dictionary<string, Func<T, bool>>();
        }
        public bool RegisterCondition(string name, Func<T, bool> comparator)
        {
            if (_conditionDictionary.ContainsKey(name)) return false;
            else
            {
                _conditionDictionary.Add(name, comparator);
                return true;
            }
        }
        public bool UnregisterCondition(string name)
        {
            if (!_conditionDictionary.ContainsKey(name)) return false;
            else
            {
                return _conditionDictionary.Remove(name);
            }
        }
        public bool ExcuteCondition(string conditionName, T obj)
        {
            if (!_conditionDictionary.ContainsKey(conditionName)) return false;
            else
            {
                var func = _conditionDictionary[conditionName];
                return func.Invoke(obj);
            }
        }
        public bool ContainsKey(string key)
        {
            return _conditionDictionary.ContainsKey(key);
        }
    }
}
