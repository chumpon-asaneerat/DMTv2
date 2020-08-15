﻿#region Using

using DMT.Models;
using NLib.Reflection;
using System;
using System.Collections;

#endregion

namespace DMT.Services
{
    #region NRestError

    /// <summary>
    /// The NRestError class.
    /// </summary>
    public class NRestError
    {
        #region Public Properties

        /// <summary>
        /// Checks has errors.
        /// </summary>
        public bool hasError
        {
            get { return (errNum != 0); }
            set { }
        }
        /// <summary>
        /// Gets or sets error number.
        /// </summary>
        public int errNum { get; set; }
        /// <summary>
        /// Gets or sets error message.
        /// </summary>
        public string errMsg { get; set; }

        #endregion
    }

    #endregion

    #region NRestResult

    /// <summary>
    /// The NRestResult class.
    /// </summary>
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

        /// <summary>
        /// Set Web Service (REST API) Connection Error.
        /// </summary>
        public virtual void RestConenctFailed()
        {
            var err = ErrNums.RestConenctFailed;
            this.errors.errNum = (int)err;
            this.errors.errMsg = ErrConsts.ErrMsg(err);
        }
        /// <summary>
        /// Set Web Service (REST API) Response Error.
        /// </summary>
        public virtual void RestResponseError()
        {
            var err = ErrNums.RestResponseError;
            this.errors.errNum = (int)err;
            this.errors.errMsg = ErrConsts.ErrMsg(err);
        }
        /// <summary>
        /// Set Unknown Error.
        /// </summary>
        public virtual void UnknownError()
        {
            var err = ErrNums.UnknownError;
            this.errors.errNum = (int)err;
            this.errors.errMsg = ErrConsts.ErrMsg(err);
        }
        /// <summary>
        /// Set Parameter Is Null Error.
        /// </summary>
        public virtual void ParameterIsNull()
        {
            var err = ErrNums.ParameterIsNull;
            this.errors.errNum = (int)err;
            this.errors.errMsg = ErrConsts.ErrMsg(err);
        }
        /// <summary>
        /// Set Success.
        /// </summary>
        public virtual void Success()
        {
            var err = ErrNums.Success;
            this.errors.errNum = (int)err;
            this.errors.errMsg = ErrConsts.ErrMsg(err);
        }
        /// <summary>
        /// Set Error.
        /// </summary>
        /// <param name="ex">The exception instance.</param>
        public virtual void Error(Exception ex)
        {
            var err = ErrNums.Exception;
            this.errors.errNum = (int)err;
            this.errors.errMsg = ex.Message;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets error instance.
        /// </summary>
        public NRestError errors { get; set; }
        /// <summary>
        /// Checks if operation has success.
        /// </summary>
        public virtual bool Ok 
        { 
            get { return !this.errors.hasError; } 
            set { } 
        }

        #endregion
    }

    #endregion

    #region NRestResult<T>

    /// <summary>
    /// The NRestResult class.
    /// </summary>
    /// <typeparam name="T">The Type of data.</typeparam>
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

        /// <summary>
        /// Set Web Service (REST API) Connection Error.
        /// </summary>
        public override void RestConenctFailed()
        {
            base.RestConenctFailed();
            this.data = Default();
        }
        /// <summary>
        /// Set Web Service (REST API) Response Error.
        /// </summary>
        public override void RestResponseError()
        {
            base.RestResponseError();
            this.data = Default();
        }
        /// <summary>
        /// Set Unknown Error.
        /// </summary>
        public override void UnknownError()
        {
            base.UnknownError();
            this.data = Default();
        }
        /// <summary>
        /// Set Parameter Is Null Error.
        /// </summary>
        public override void ParameterIsNull()
        {
            base.ParameterIsNull();
            this.data = Default();
        }
        /// <summary>
        /// Set Success.
        /// </summary>
        /// <param name="data">The Data instance.</param>
        public void Success(T data)
        {
            base.Success();
            this.data = (null != data) ? data : Default();
        }
        /// <summary>
        /// Set Error.
        /// </summary>
        /// <param name="ex">The exception instance.</param>
        public override void Error(Exception ex)
        {
            base.Error(ex);
            this.data = Default();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets Data Instance.
        /// </summary>
        public T data { get; set; }
        /// <summary>
        /// Checks if has data (not null).
        /// </summary>
        public bool HasData 
        { 
            get { return (null != this.data); }
            set { }
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets default instance.
        /// </summary>
        /// <returns>Returns default instance. If T is IList return new instance.</returns>
        public static T Default()
        {
            return (typeof(T) == typeof(IList)) ? new T() : default(T);
        }

        #endregion
    }

    #endregion

    #region NRestResult<T, O>

    /// <summary>
    /// The NRestResult class.
    /// </summary>
    /// <typeparam name="T">The Type of data.</typeparam>
    /// <typeparam name="O">The Type of output.</typeparam>
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

        /// <summary>
        /// Set Web Service (REST API) Connection Error.
        /// </summary>
        public override void RestConenctFailed()
        {
            base.RestConenctFailed();
            this.data = DefaultData();
            this.output = DefaultOutput();
        }
        /// <summary>
        /// Set Web Service (REST API) Response Error.
        /// </summary>
        public override void RestResponseError()
        {
            base.RestResponseError();
            this.data = DefaultData();
            this.output = DefaultOutput();
        }
        /// <summary>
        /// Set Unknown Error.
        /// </summary>
        public override void UnknownError()
        {
            base.UnknownError();
            this.data = DefaultData();
            this.output = DefaultOutput();
        }
        /// <summary>
        /// Set Parameter Is Null Error.
        /// </summary>
        public override void ParameterIsNull()
        {
            base.ParameterIsNull();
            this.data = DefaultData();
            this.output = DefaultOutput();
        }
        /// <summary>
        /// Set Success.
        /// </summary>
        /// <param name="data">The Data instance.</param>
        /// <param name="output">The Output instance.</param>
        public void Success(T data, O output)
        {
            base.Success();
            this.data = (null != data) ? data : DefaultData();
            this.output = (null != output) ? output : DefaultOutput();
        }
        /// <summary>
        /// Set Error.
        /// </summary>
        /// <param name="ex">The exception instance.</param>
        public override void Error(Exception ex)
        {
            base.Error(ex);
            this.data = DefaultData();
            this.output = DefaultOutput();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets Data Instance.
        /// </summary>
        public T data { get; set; }
        /// <summary>
        /// Checks if has data (not null).
        /// </summary>
        public bool HasData 
        { 
            get { return (null != this.data); }
            set { }
        }

        /// <summary>
        /// Gets Output Instance.
        /// </summary>
        public O output { get; set; }
        /// <summary>
        /// Checks if has ouput (not null).
        /// </summary>
        public bool HasOutput 
        { 
            get { return (null != this.output); }
            set { }
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets default instance.
        /// </summary>
        /// <returns>Returns default instance. If T is IList return new instance.</returns>
        public static T DefaultData()
        {
            return (typeof(T) == typeof(IList)) ? new T() : default(T);
        }
        /// <summary>
        /// Gets default instance.
        /// </summary>
        /// <returns>Returns default instance. If T is IList return new instance.</returns>
        public static O DefaultOutput()
        {
            return (typeof(O) == typeof(IList)) ? new O() : default(O);
        }

        #endregion
    }

    #endregion

    #region NRestResult Extension Methods

    /// <summary>
    /// The NRestResult Extension Methods class.
    /// </summary>
    public static class NRestResultExtensionMethods
    {
        #region Public Methods (static)

        public static T DefaultData<T>()
            where T : new()
        {
            return (typeof(T) == typeof(IList)) ? new T() : default(T);
        }

        public static O DefaultOutput<O>()
            where O : new()
        {
            return (typeof(O) == typeof(IList)) ? new O() : default(O);
        }

        #endregion

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

        #region Value

        public static T Value<T>(this NRestResult<T> value)
            where T : new()
        {
            T ret = (null != value && !value.errors.hasError && null != value.data) ?
                value.data : DefaultData<T>();
            return ret;
        }

        public static T Value<T, O>(this NRestResult<T, O> value)
            where T : new()
            where O : new()
        {
            T ret = (null != value && !value.errors.hasError && null != value.data) ?
                value.data : DefaultData<T>();
            return ret;
        }

        public static O Output<T, O>(this NRestResult<T, O> value)
            where T : new()
            where O : new()
        {
            O ret = (null != value && !value.errors.hasError && null != value.output) ?
                value.output : DefaultOutput<O>();
            return ret;
        }

        #endregion

        #region Functionals

        #endregion
    }

    #endregion
}
