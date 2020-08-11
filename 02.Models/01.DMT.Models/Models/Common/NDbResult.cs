#region Using

using System;
using System.Collections.Generic;
using System.Linq;

// required for JsonIgnore.
using Newtonsoft.Json;
using NLib;
using NLib.Reflection;

#endregion

namespace DMT.Models
{
    public class NDbError
    {
        public bool hasError { get; set; }
        public int errNum { get; set; }
        public string errMsg { get; set; }
    }

    public class NDbResult
    {
        public NDbResult() : base()
        {
            this.errors = new NDbError();
            UnknownError();
        }

        public NDbError errors { get; set; }

        public virtual void ConenctFailed()
        {
            this.errors.errNum = -1;
            this.errors.errMsg = "Database connection failed.";
        }

        public virtual void UnknownError()
        {
            this.errors.errNum = -9999;
            this.errors.errMsg = "Unknown error.";
        }

        public virtual void Success()
        {
            this.errors.errNum = 0;
            this.errors.errMsg = "Success.";
        }

        public virtual void Error(Exception ex)
        {
            this.errors.errNum = -1;
            this.errors.errMsg = ex.Message;
        }
    }

    public class NDbResult<T> : NDbResult
    {
        public NDbResult() : base()
        {
        }

        public T data { get; set; }

        public override void ConenctFailed()
        {
            base.ConenctFailed();
        }

        public override void UnknownError()
        {
            base.UnknownError();
        }

        public override void Success()
        {
            base.Success();
        }

        public override void Error(Exception ex)
        {
            base.Error(ex);
        }
    }

    public class NDbResult<T, O> : NDbResult
    {
        public NDbResult() : base() 
        { 
        }

        public T data { get; set; }
        public O Out { get; set; }

        public override void ConenctFailed()
        {
            base.ConenctFailed();
        }

        public override void UnknownError()
        {
            base.UnknownError();
        }

        public override void Success()
        {
            base.Success();
        }

        public override void Error(Exception ex)
        {
            base.Error(ex);
        }
    }
}
