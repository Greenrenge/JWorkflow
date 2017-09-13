namespace JWorkflow.Scaffolding
{
    public interface IWFUserAction: IWFUserAction<long, long>
    {

    }
    public interface IWFUserAction<TUserKey,TGroupKey>
    {
        string RoleName { get; }
        short RoleLevel { get; }

        TUserKey UserId { get; }
        TGroupKey GroupId { get; }
    }
}