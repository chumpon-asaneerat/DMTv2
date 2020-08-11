#region Using

using DMT.Models;
using System;

#endregion

namespace DMT.Services
{
    #region NRestError

    public class NRestError
    {
        #region Public Properties

        public bool hasError
        {
            get { return (errNum != 0); }
            set { }
        }
        public int errNum { get; set; }
        public string errMsg { get; set; }

        #endregion
    }

    #endregion

    #region NRestResult

    public class NRestResult
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public NRestResult() : base()
        {
            this.errors = new NRestError();
            UnknownError();
        }

        #endregion

        #region Virtual Methods

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

        #endregion

        #region Public Properties

        public NRestError errors { get; set; }

        #endregion
    }

    #endregion

    #region NRestResult<T>

    public class NRestResult<T> : NRestResult
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public NRestResult() : base() { }

        #endregion

        #region Override Methods

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

        #endregion

        #region Public Properties

        public T data { get; set; }

        #endregion
    }

    #endregion

    #region NRestResult<T, O>

    public class NRestResult<T, O> : NRestResult
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public NRestResult() : base() { }

        #endregion

        #region Override Methods

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

        #endregion

        #region Public Properties

        public T data { get; set; }
        public O Out { get; set; }

        #endregion
    }

    #endregion

    #region NRestResult Extension Methods

    public static class NRestResultExtensionMethods
    {
        public static NRestResult ToRest(this NDbResult value)
        {
            NRestResult ret = new NRestResult();

            if (null != value)
            {
                ret.errors.errNum = value.errors.errNum;
                ret.errors.errMsg = value.errors.errMsg;
            }
            else
            {

            }

            return ret;
        }

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

    #endregion
}
