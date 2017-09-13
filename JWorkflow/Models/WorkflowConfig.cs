using JWorkflow.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWorkflow
{

    public class WorkflowConfig
    {
        public string version { get; set; }
        public List<WorkFlowPath> WorkFlowPath { get; set; }
    }
    public class WorkFlowPath
    {
        public string ID { get; set; }
        public List<Node> Nodes { get; set; }
    }
    public class Node
    {
        public NODETYPE NodeType { get; set; }
        public string StageName { get; set; }
        public List<NodeCondition> ApproveNodes { get; set; }
        public List<NodeCondition> RejectNodes { get; set; }
        public List<Permission> Permissions { get; set; }
    }
    public class NodeCondition
    {
        public string Node { get; set; }
        public string Condition { get; set; }
    }
    public class Permission
    {
        public string Action { get; set; }
        public List<PermissionCard> PermissionCards { get; set; }
    }

    public class PermissionCard : IPermissionCard
    {
        public PermissionType Type { get; set; }
        public string RoleName { get; set; }
        public int RoleLevel { get; set; }
    }



    /// <summary>
    /// for generic create class that implement IPermissionIdentity
    /// </summary>
    public class PermissionIdentity : IPermissionIdentity 
    {
        private List<PermissionCard> _permission;
        public PermissionIdentity()
        {
            _permission = new List<PermissionCard>();
        }
        public string Username { get; set; }
        public IEnumerable<IPermissionCard> PermissionCards { get { return _permission as IEnumerable<IPermissionCard>; } }
        public List<PermissionCard> PermissionCardsList { get; }
    }

    /// <summary>
    /// To Return both Individual permission and Group permission for particular stage
    /// </summary>
    public class PermissionList : IPermissionList
    {
        private List<string> _individualPermissions;
        private List<PermissionCard> _groupPermissions;

        IEnumerable<string> IPermissionList.IndividualPermissions
        {
            get
            {
                return _individualPermissions as IEnumerable<string>;
            }
        }

        IEnumerable<IPermissionCard> IPermissionList.GroupPermissions
        {
            get
            {
                return _groupPermissions as IEnumerable<IPermissionCard>;
            }
        }

        public PermissionList()
        {
            _individualPermissions = new List<string>();
            _groupPermissions = new List<PermissionCard>();
        }
        public List<string> IndividualPermissions { get { return _individualPermissions; } }
        public List<PermissionCard> GroupPermissions { get { return _groupPermissions; } }
    }
}
