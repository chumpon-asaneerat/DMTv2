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

    #region NDbResult<T, O>

    public class NDbResult<T, O> : NDbResult
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
        public O output { get; set; }

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
        {
            return (null != value && !value.errors.hasError) ? true : false;
        }

        public static bool Success<T, O>(this NDbResult<T, O> value)
        {
            return (null != value && !value.errors.hasError) ? true : false;
        }

        #endregion

        #region Value

        public static T Value<T>(this NDbResult<T> value)
            where T : new()
        {
            T ret;
            if (typeof(T) == typeof(IList))
            {
                ret = (null != value && !value.errors.hasError && null != value.data) ?
                    value.data : new T();
            }
            else
            {
                ret = (null != value && !value.errors.hasError) ? value.data : default(T);
            }
            return ret;
        }

        public static T Value<T, O>(this NDbResult<T, O> value)
            where T : new()
        {
            T ret;
            if (typeof(T) == typeof(IList))
            {
                ret = (null != value && !value.errors.hasError && null != value.data) ?
                    value.data : new T();
            }
            else
            {
                ret = (null != value && !value.errors.hasError) ? value.data : default(T);
            }
            return ret;
        }

        #endregion
    }

    #endregion
}
