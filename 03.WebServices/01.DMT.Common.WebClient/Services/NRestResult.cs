using DMT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DMT.Services
{
    public class NRestError
    {
        public bool hasError { get; set; }
        public int errNum { get; set; }
        public string errMsg { get; set; }
    }

    public class NRestResult
    {
        public NRestResult() : base()
        {
            this.errors = new NRestError();
            UnknownError();
        }

        public NRestError errors { get; set; }

        public virtual void ConenctFailed()
        {
            this.errors.errNum = -1;
            this.errors.errMsg = "No Web Service connection failed.";
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

    public class NRestResult<T> : NRestResult
    {
        public NRestResult() : base()
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

    public class NRestResult<T, O> : NRestResult
    {
        public NRestResult() : base()
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

    public static class NRestResultExtensionMethods
    {
        public static NRestResult<T> ToRest<T>(this NDbResult<T> value)
        {
            NRestResult<T> ret = new NRestResult<T>();

            if (null != value)
            {
                ret.data = value.data;
                ret.errors.errNum = value.errors.errNum;
                ret.errors.errMsg = value.errors.errMsg;
            }
            else
            {

            }

            return ret;
        }

        public static NRestResult<T, O> ToRest<T, O>(this NDbResult<T, O> value)
        {
            NRestResult<T, O> ret = new NRestResult<T, O>();

            if (null != value)
            {
                ret.data = value.data;
                ret.errors.errNum = value.errors.errNum;
                ret.errors.errMsg = value.errors.errMsg;
                ret.Out = value.Out;
            }
            else
            {
                ret
            }

            return ret;
        }
    }
}
