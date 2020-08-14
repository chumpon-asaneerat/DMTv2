#region Using

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// required for JsonIgnore.
using Newtonsoft.Json;
using NLib;
using NLib.Reflection;

#endregion

namespace DMT.Models
{
    #region NDbError

    public class NDbError
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

    #region NDbResult

    public class NDbResult
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public NDbResult() : base()
        {
            this.errors = new NDbError();
            UnknownError();
        }

        #endregion

        #region Virtual Methods

        public virtual void DbConenctFailed()
        {
            var err = ErrNums.DbConenctFailed;
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

        public NDbError errors { get; private set; }

        #endregion
    }

    #endregion

    #region NDbResult<T>

    public class NDbResult<T> : NDbResult
        where T: new()
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public NDbResult() : base()
        {
        }

        #endregion

        #region Override Methods

        public override void DbConenctFailed()
        {
            base.DbConenctFailed();
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

        public T data { get; private set; }

        #endregion

        #region Static Methods

        public static T Default()
        {
            return (typeof(T) == typeof(IList)) ? new T() : default(T);
        }

        #endregion
    }

    #endregion

    #region NDbResult<T, O>

    public class NDbResult<T, O> : NDbResult
        where T : new()
        where O : new()
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public NDbResult() : base() 
        { 
        }

        #endregion

        #region Override Methods

        public override void DbConenctFailed()
        {
            base.DbConenctFailed();
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

        public T data { get; private set; }
        public O output { get; private set; }

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

    #region NDbResult

    public static class NDbResultExtensionMethods
    {
        #region Success

        public static bool Success(this NDbResult value)
        {
            return (null != value && !value.errors.hasError) ? true : false;
        }

        public static bool Success<T>(this NDbResult<T> value)
            where T : new()
        {
            return (null != value && !value.errors.hasError) ? true : false;
        }

        public static bool Success<T, O>(this NDbResult<T, O> value)
            where T : new()
            where O : new()
        {
            return (null != value && !value.errors.hasError) ? true : false;
        }

        #endregion

        #region Value

        public static T Value<T>(this NDbResult<T> value)
            where T : new()
        {
            T ret = (null != value && !value.errors.hasError && null != value.data) ?
                value.data : Default<T>();
            return ret;
        }

        public static T Value<T, O>(this NDbResult<T, O> value)
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
            where T: new()
        {
            return (typeof(T) == typeof(IList)) ? new T() : default(T);
        }

        #endregion
    }

    #endregion
}
