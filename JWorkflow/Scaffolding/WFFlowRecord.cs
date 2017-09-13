using BaseClassEnitiy.Models;
using JWorkflow.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace JWorkflow.Scaffolding
{
    //ต้องเพิ่ม record ขึ้นเรื่อยๆด้วย
    public class WFFlowRecord<TUser,TFlow>:Entity<long>,IWFActiveFlag
    where TUser: IWFUser
    where TFlow :
    {
        public string FormId { get; set; }

        [ForeignKey("FormId")]
        public TFlow Flow { get; set; }

        public long User { get; set; }

        [ForeignKey("UserId")]
        public TUser ActionByUser { get; set; }

        public string Action { get; set; }
        [NotMapped]
        public FLOWACTION FlowAction
        {
            get
            {
                return (FLOWACTION)Enum.Parse(typeof(FLOWACTION), Action);
            }
            set
            {
                Action = value.ToString();
            }
        }

        public string ActionForStage { get; set; }
        public string StageBefore { get; set; }
        public string RejectToStage { get; set; }
        public string Comment { get; set; }
        public DateTime ActionDate { get; set; }
        public int FlowVersion { get; set; }

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