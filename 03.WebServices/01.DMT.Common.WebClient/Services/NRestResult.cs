#region Using

using DMT.Models;
using NLib.Reflection;
using System;
using System.Collections;

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

        public virtual void RestConenctFailed()
        {
            var err = ErrNums.RestConenctFailed;
            this.errors.errNum = (int)err;
            this.errors.errMsg = ErrConsts.ErrMsg(err);
        }

        public virtual void UnknownError()
        {
            var err = ErrNums.UnknownError;
            this.errors.errNum = (int)err;
            this.errors.errMsg = ErrConsts.ErrMsg(err);
        }

        public virtual void ParameterIsNull()
        {
            var err = ErrNums.ParameterIsNull;
            this.errors.errNum = (int)err;
            this.errors.errMsg = ErrConsts.ErrMsg(err);
        }

        public virtual void Success()
        {
            var err = ErrNums.Success;
            this.errors.errNum = (int)err;
            this.errors.errMsg = ErrConsts.ErrMsg(err);
        }

        public virtual void Error(Exception ex)
        {
            var err = ErrNums.Exception;
            this.errors.errNum = (int)err;
            this.errors.errMsg = ex.Message;
        }

        #endregion

        #region Public Properties

        public NRestError errors { get; private set; }
        /// <summary>
        /// Checks if operation has success.
        /// </summary>
        public virtual bool Ok { get { return !this.errors.hasError; } }

        #endregion
    }

    #endregion

    #region NRestResult<T>

    public class NRestResult<T> : NRestResult
        where T : new()
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public NRestResult() : base() { }

        #endregion

        #region Override Methods

        public override void RestConenctFailed()
        {
            base.RestConenctFailed();
            this.data = Default();
        }

        public override void UnknownError()
        {
            base.UnknownError();
            this.data = Default();
        }

        public override void ParameterIsNull()
        {
            base.ParameterIsNull();
            this.data = Default();
        }

        public void Success(T data)
        {
            base.Success();
            this.data = (null != data) ? data : Default();
        }

        public override void Error(Exception ex)
        {
            base.Error(ex);
            this.data = Default();
        }

        #endregion

        #region Public Properties

        public T data { get; set; }
        /// <summary>
        /// Checks if has data (not null).
        /// </summary>
        public bool HasData { get { return (null != this.data); } }

        #endregion

        #region Static Methods

        public static T Default()
        {
            return (typeof(T) == typeof(IList)) ? new T() : default(T);
        }

        #endregion
    }

    #endregion

    #region NRestResult<T, O>

    public class NRestResult<T, O> : NRestResult
        where T: new()
        where O : new()
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public NRestResult() : base() { }

        #endregion

        #region Override Methods

        public override void RestConenctFailed()
        {
            base.RestConenctFailed();
            this.data = DefaultData();
            this.output = DefaultOutput();
        }

        public override void UnknownError()
        {
            base.UnknownError();
            this.data = DefaultData();
            this.output = DefaultOutput();
        }

        public override void ParameterIsNull()
        {
            base.ParameterIsNull();
            this.data = DefaultData();
            this.output = DefaultOutput();
        }

        public void Success(T data, O output)
        {
            base.Success();
            this.data = (null != data) ? data : DefaultData();
            this.output = (null != output) ? output : DefaultOutput();
        }

        public override void Error(Exception ex)
        {
            base.Error(ex);
            this.data = DefaultData();
            this.output = DefaultOutput();
        }

        #endregion

        #region Public Properties

        public T data { get; set; }
        /// <summary>
        /// Checks if has data (not null).
        /// </summary>
        public bool HasData { get { return (null != this.data); } }

        public O output { get; set; }
        /// <summary>
        /// Checks if has ouput (not null).
        /// </summary>
        public bool HasOutput { get { return (null != this.output); } }

        #endregion

        #region Static Methods

        public static T DefaultData()
        {
            return (typeof(T) == typeof(IList)) ? new T() : default(T);
        }

        public static O DefaultOutput()
        {
            return (typeof(O) == typeof(IList)) ? new O() : default(O);
        }

        #endregion
    }

    #endregion

    #region NRestResult Extension Methods

    public static class NRestResultExtensionMethods
    {
        #region ToRest (from NDbResult)

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
                ret.ParameterIsNull();
            }

            return ret;
        }

        public static NRestResult<T> ToRest<T>(this NDbResult<T> value)
            where T : new()
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
                ret.ParameterIsNull();
            }

            return ret;
        }

        public static NRestResult<T, O> ToRest<T, O>(this NDbResult<T, O> value)
            where T : new()
            where O : new()
        {
            NRestResult<T, O> ret = new NRestResult<T, O>();

            if (null != value)
            {
                ret.data = value.data;
                ret.errors.errNum = value.errors.errNum;
                ret.errors.errMsg = value.errors.errMsg;
                ret.output = value.output;
            }
            else
            {
                ret.ParameterIsNull();
            }

            return ret;
        }

        #endregion

        #region Success

        public static bool Success(this NRestResult value)
        {
            return (null != value && !value.errors.hasError) ? true : false;
        }

        public static bool Success<T>(this NRestResult<T> value)
            where T : new()
        {
            return (null != value && !value.errors.hasError) ? true : false;
        }

        public static bool Success<T, O>(this NRestResult<T, O> value)
            where T : new()
            where O : new()
        {
            return (null != value && !value.errors.hasError) ? true : false;
        }

        #endregion

        #region Value

        public static T Value<T>(this NRestResult<T> value)
            where T : new()
        {
            T ret = (null != value && !value.errors.hasError && null != value.data) ?
                value.data : Default<T>();
            return ret;
        }

        public static T Value<T, O>(this NRestResult<T, O> value)
            where T : new()
            where O : new()
        {
            T ret = (null != value && !value.errors.hasError && null != value.data) ?
                value.data : Default<T>();
            return ret;
        }

        #endregion

        #region Private Methods (static)

        public static T Default<T>()
            where T : new()
        {
            return (typeof(T) == typeof(IList)) ? new T() : default(T);
        }

        #endregion
    }

    #endregion
}
