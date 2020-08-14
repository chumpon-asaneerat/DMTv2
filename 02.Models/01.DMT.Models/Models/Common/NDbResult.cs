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

        public virtual void ParameterIsNull()
        {
            this.errors.errNum = -2;
            this.errors.errMsg = "Parameter is null.";
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

        public NDbError errors { get; set; }

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

        public override void ConenctFailed()
        {
            base.ConenctFailed();
            this.data = Default();
        }

        public override void UnknownError()
        {
            base.UnknownError();
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

        public override void ConenctFailed()
        {
            base.ConenctFailed();
            this.data = Default();
        }

        public override void UnknownError()
        {
            base.UnknownError();
            this.data = Default();
        }

        public void Success(T data, O output)
        {
            base.Success();
            this.data = data;
            this.output = output;
        }

        public override void Error(Exception ex)
        {
            base.Error(ex);
            this.data = Default();
        }

        #endregion

        #region Public Properties

        public T data { get; set; }
        public O output { get; set; }

        #endregion

        #region Static Methods

        public static T Default()
        {
            return (typeof(T) == typeof(IList)) ? new T() : default(T);
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
