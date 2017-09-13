using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JWorkflow.Scaffolding
{
    public class JGroup : JGroup<JUserAction>, IWFActiveFlag, IWFGroup
    {
        public JGroup():base()
        {

        }
        //We could add more props here
    }

    //UserAction can be used here as : WFUserAction<long,long> and IUserAction
    public class JGroup<TUserAction> : WFGroup,IWFActiveFlag,IWFGroup
        where TUserAction:WFUserAction<long,long>,IWFUserAction
    {
        public JGroup()
        {
            this.UserActions = new List<TUserAction>();
        }

        [Column(TypeName = "NVARCHAR")]
        [StringLength(100)]
        public string GroupFullName { get; set; }


        public virtual ICollection<TUserAction> UserActions { get; set; }

        #region IWFGroup
        public ICollection<IWFUserAction> OwnedBy
        {
            get
            {
                return UserActions as ICollection<IWFUserAction>;
            }
        }
        #endregion


        #region IJActiveFlag
        [NotMapped]
        bool IWFActiveFlag.IsActive
        {
            get
            {
                return Active == "A";
            }
        }

        void IWFActiveFlag.SetActive()
        {
            Active = "A";
        }

        void IWFActiveFlag.SetActiveFlagToHistory()
        {
            Active = "N";
        }
        #endregion IJActiveFlag
    }
}
