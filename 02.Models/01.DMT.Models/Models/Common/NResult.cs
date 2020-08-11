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
    public class NError
    {
        public bool hasError { get; set; }
        public int errNum { get; set; }
        public string errMsg { get; set; }
    }

    public class NResult
    {
        public NResult() : base()
        {
            this.errors = new NError();
            this.errors.errNum = -9999;
            this.errors.errMsg = "Unknown error.";
        }

        public NError errors { get; set; }

        public virtual void DatabaseNotConnected()
        {
            this.errors.errNum = -1;
            this.errors.errMsg = "No database connection.";
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

    public class NResult<T> : NResult
        where T : class, new()
    {
        public NResult() : base()
        {
        }

        public T data { get; set; }

        public override void DatabaseNotConnected()
        {
            base.DatabaseNotConnected();
            this.data = new T();
        }

        public override void Success()
        {
            base.Success();
        }

        public override void Error(Exception ex)
        {
            base.Error(ex);
            this.data = new T();
        }
    }

    public class NResult<T, O> : NResult
        where T : class, new()
        where O : class, new()
    {
        public NResult() : base() 
        { 
        }

        public T data { get; set; }
        public O Out { get; set; }

        public override void DatabaseNotConnected()
        {
            base.DatabaseNotConnected();
            this.data = new T();
            this.Out = new O();
        }

        public override void Success()
        {
            base.Success();
        }

        public override void Error(Exception ex)
        {
            base.Error(ex);
            this.data = new T();
            this.Out = new O();
        }
    }
}
