using JWorkflow.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWorkflow
{
    public interface IPermissionList
    {
        IEnumerable<string> IndividualPermissions { get; }
        IEnumerable<IPermissionCard> GroupPermissions { get; }
    }
    public interface IPermissionIdentity
    {
        string Username { get; }
        IEnumerable<IPermissionCard> PermissionCards { get; }
    }
    public interface IPermissionCard
    {
        string RoleName { get; }
        int RoleLevel { get; }
    }


    class Workflow<T> where T : class, new()
    {
        private WorkFlowConditionConfig<T> _conditionConfig;
        private WorkFlowPath _workflowPath;
        private Node _submitNode;
        private Node _finishNode;
        private IDictionary<string, Node> _nodeDict;

        private ILogger _logger;
        private void log(string action, string msg)
        {
            msg = " " + _workflowPath.ID + " " + msg;
            if (_logger != null) _logger.Log(action, msg);
        }
        private bool CheckPermission(IEnumerable<Permission> permissions, IPermissionIdentity identity)
        {
            foreach (var permission in permissions)
            {
                foreach (var permissionCard in permission.PermissionCards)
                {
                    if (permissionCard.Type == PermissionType.GROUP)
                    {
                        if (identity.PermissionCards.Any(x => x.RoleName.ToLower() == permissionCard.RoleName.ToLower() && (x.RoleLevel == permissionCard.RoleLevel || permissionCard.RoleLevel == -1)))
                        {
                            return true;
                        }
                    }
                    else if (permissionCard.Type == PermissionType.INDIVIDUAL)
                    {
                        if (identity.Username.ToLower() == permissionCard.RoleName.ToLower())
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public Workflow(WorkFlowPath path, WorkFlowConditionConfig<T> conditionSetting, ILogger logger = null)
        {
            _workflowPath = path;
            _conditionConfig = conditionSetting;
            if (logger != null) _logger = logger;
            foreach (var node in _workflowPath.Nodes)
            {
                if (node.NodeType == NODETYPE.SUBMIT && _submitNode == null)
                {
                    _submitNode = node;
                    _submitNode.StageName = _submitNode.StageName.ToUpper();
                    _nodeDict.Add(node.StageName.ToUpper(), node);
                    continue;
                }
                if (node.NodeType == NODETYPE.SUBMIT && _submitNode != null)
                {
                    log("ERROR", "CONFIG ERROR:DUBLICATION FOUND FOR SUBMIT NODE");
                    continue;
                }
                if (node.NodeType == NODETYPE.SYSTEM && node.ApproveNodes.Count == 1 && node.ApproveNodes[0].Node.ToUpper() == "FINISH" && _finishNode == null)
                {
                    _finishNode = node;
                    _finishNode.StageName = _finishNode.StageName.ToUpper();
                    _nodeDict.Add(node.StageName.ToUpper(), node);
                    continue;
                }
                if (node.NodeType == NODETYPE.SYSTEM && node.ApproveNodes.Count == 1 && node.ApproveNodes[0].Node.ToUpper() == "FINISH" && _finishNode != null)
                {
                    log("ERROR", "CONFIG ERROR:DUBLICATION FOUND FOR FINISH NODE");
                    continue;
                }
                else
                {
                    _nodeDict.Add(node.StageName.ToUpper(), node);
                }
            }
            if (_submitNode == null) log("ERROR", "CONFIG ERROR:NOT FOUND SUBMIT NODE");
            if (_finishNode == null) log("ERROR", "CONFIG ERROR:NOT FOUND FINISH NODE");
        }
        public string FlowPathName { get { return _workflowPath.ID; } }//MT_1
        public bool isFlowInitiator(IPermissionIdentity identity)
        {
            //check if current user has right to initiate flow or not
            if (_submitNode == null)
            {
                return false;
            }
            else
            {
                var submitPermission = _submitNode.Permissions.Where(x => x.Action.ToLower() == "Submit".ToLower());
                return CheckPermission(submitPermission, identity);
            }
        }
        public bool isPermitToStage(IPermissionIdentity identity, string awaitingStage, string action=null)
        {
            if(action==null)
            {
                if (awaitingStage.ToUpper() == "SUBMIT") action = "submit";
                else action = "approve";
            }
            //use for check user has authorize to action(approve) at awaiiting stage or not
            awaitingStage = awaitingStage.ToUpper();
            if (!_nodeDict.ContainsKey(awaitingStage))
            {
                log("ERROR", "CONFIG ERROR:CANNOT FIND STAGE IN CONFIG : " + awaitingStage);
                return false;
            }
           
            var stagePermissions = _nodeDict[awaitingStage].Permissions.Where(x => x.Action.ToLower() == action.ToLower());
            return CheckPermission(stagePermissions, identity);

        }
        public IPermissionList GetPermissionList(string stageName, string action=null)
        {
            if (action == null)
            {
                if (stageName.ToUpper() == "SUBMIT") action = "submit";
                else action = "approve";
            }
            var output = new PermissionList();
            stageName = stageName.ToUpper();
            if (!_nodeDict.ContainsKey(stageName))
            {
                log("ERROR", "CONFIG ERROR:CANNOT FIND STAGE IN CONFIG : " + stageName);
            }
            else
            {
                var stagePermissions = _nodeDict[stageName].Permissions.Where(x => x.Action.ToLower() == action.ToLower());
                foreach (var permission in stagePermissions)
                {
                    foreach (var permissionCard in permission.PermissionCards)
                    {
                        if (permissionCard.Type == PermissionType.INDIVIDUAL)
                        {
                            output.IndividualPermissions.Add(permissionCard.RoleName);
                        }
                        else if (permissionCard.Type == PermissionType.GROUP)
                        {
                            output.GroupPermissions.Add(new PermissionCard { RoleName = permissionCard.RoleName, RoleLevel = permissionCard.RoleLevel });
                        }
                    }
                }
            }
            return output as IPermissionList;
        }
        public string FindNextStage(T obj, string passedStage, bool approve = true)
        {
            passedStage = passedStage.ToUpper();
            if (passedStage == _finishNode.StageName && approve)
            {
                //Next is finish node
                return "FINISH";
            }
            else
            {
                if (!_nodeDict.ContainsKey(passedStage))
                {
                    log("ERROR", "CONFIG ERROR:CANNOT FIND NEXT STAGE FOR " + passedStage);
                    return "ERROR";
                }
                List<NodeCondition> paths;
                paths = approve ? _nodeDict[passedStage].ApproveNodes : _nodeDict[passedStage].RejectNodes;
                NodeCondition defaultPath = null;
                foreach (var configPath in paths)
                {
                    if (configPath.Condition == null && defaultPath == null)
                    {
                        defaultPath = configPath;
                        continue;
                    }
                    if (configPath.Condition == null && defaultPath != null)
                    {
                        log("WARNING", "CONFIG WARNING:DUBLICATION OF DEFAULT PATH FOR " + passedStage + " (" + (approve ? "Approve" : "Reject") + ")");
                        continue;
                    }
                    string conditionName = configPath.Condition;
                    if (!_conditionConfig.ContainsKey(conditionName))
                    {
                        log("ERROR", "CONFIG ERROR:CANNOT FIND CONDITION CONFIG FOR " + conditionName + " (" + (approve ? "Approve" : "Reject") + ")");
                        continue;
                    }
                    else
                    {
                        if (_conditionConfig.ExcuteCondition(conditionName, obj)) return configPath.Node.ToUpper();//Excute and found next node
                        else continue;
                    }

                }
                if (defaultPath == null)
                {
                    log("ERROR", "CONFIG ERROR:CANNOT FIND NEXT STAGE(DEFAULT PATH) FOR " + passedStage + " (" + (approve ? "Approve" : "Reject") + ")");
                    return "ERROR";
                }
                else
                {
                    return defaultPath.Node.ToUpper();
                }
            }

        }
    }
}
