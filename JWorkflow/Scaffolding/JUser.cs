using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JWorkflow.Scaffolding
{
    public class JUser : JUser<JUserAction>, IWFUser, IWFActiveFlag
    {
        public JUser():base()
        {

        }
        //We could add more props here
    }

    public class JUser<TUserAction> :WFUser,IWFUser,IWFActiveFlag
        where TUserAction:WFUserAction<long,long>,IWFUserAction
    {
        public JUser()
        {
            this.UserActions = new List<TUserAction>();
        }


        [Column(TypeName = "NVARCHAR")]
        [StringLength(50)]
        [Required, Index(IsUnique = true)]
        public virtual ICollection<TUserAction> UserActions { get; set; }


        [NotMapped]
        public ICollection<IWFUserAction> Claims
        {
            get
            {
                return UserActions as ICollection<IWFUserAction>;
            }
        }
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
