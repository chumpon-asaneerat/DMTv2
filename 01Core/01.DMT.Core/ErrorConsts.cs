#region Using

using System;
using System.Collections.Generic;

#endregion

namespace DMT
{
    public enum ErrNums : int
    {
        Success = 0,
        // Local Database Connection (100-149)
        DbConenctFailed = 100,
        // Web Service Connection (150-199)
        RestConenctFailed = 150,
        // Models - Common (200-210)
        ParameterIsNull = 200,
        // Unknown (999)
        UnknownError = 999
    }

    public class ErrConsts
    {
        private static Dictionary<ErrNums, string> _msgs;

        static ErrConsts()
        {
            _msgs = new Dictionary<ErrNums, string>();
            _msgs.Add(ErrNums.Success, "Success.");
            // Local Database
            _msgs.Add(ErrNums.DbConenctFailed, "Database connection failed..");
            // Model common
            _msgs.Add(ErrNums.ParameterIsNull, "Parameter is null.");
            _msgs.Add(ErrNums.UnknownError, "Unknown error.");
        }

        public static string ErrMsg(ErrNums value)
        {
            if (_msgs.ContainsKey(value))
                return _msgs[value];
            else return _msgs[ErrNums.UnknownError];
        }
    }
}
