using BaseClassEnitiy.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseClassEnitiy.Scaffolding
{
    public class JFlowRecord :Entity<long>, IWFFlowRecord,IWFActiveFlag
    {
        ////[Column(TypeName = "NVARCHAR")]
        ////[StringLength(50)]
        ////[Required, Index(IsUnique = true)]

        //[Index]
        //public long FlowId { get; set; }

        //public long UserId { get; set; }







        //[ForeignKey("FlowId")]
        //public virtual JFlow Flow { get; }

        //[ForeignKey("FlowId")]
        //public virtual JUser User { get; }


        //public string Action
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }

        //    set
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        //public DateTime ActionDate
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }

        //    set
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        //public string ActionForStage
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }

        //    set
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        //public IWFUser Owner
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }

        //    set
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        //public string PreviousStage
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }

        //    set
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        //public string RejectToStage
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }

        //    set
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        //public short Version
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }

        //    set
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        //public bool IsActive
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        //public void SetActiveFlagToHistory()
        //{
        //    throw new NotImplementedException();
        //}

        //public void SetActive()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
