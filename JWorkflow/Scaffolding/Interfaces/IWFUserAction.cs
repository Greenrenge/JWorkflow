namespace JWorkflow.Scaffolding
{
    public interface IWFUserAction
    {
        short RoleLevel { get; }
        string GroupName { get; }
        string UserName { get; }
    }
}