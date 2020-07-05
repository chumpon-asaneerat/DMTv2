#region Using

using System;
using System.Collections.Generic;
using System.Linq;

using SQLite;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensions.Extensions;

// required for JsonIgnore.
using Newtonsoft.Json;
using NLib;
using NLib.Reflection;

#endregion

namespace DMT.Models
{
    #region TSBShift

    //[Table("TSBShift")]
    public class TSBShift : NTable<TSBShift>
    {
        #region Intenral Variables

        private int _TSBShiftId = 0;
        private string _TSBId = string.Empty;
        private int _ShiftId = 0;
        private string _UserId = string.Empty;

        private DateTime _Begin = DateTime.MinValue;
        private DateTime _End = DateTime.MinValue;

        private int _Status = 0;
        private DateTime _LastUpdate = DateTime.MinValue;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public TSBShift() : base() { }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets PK Id.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        [PeropertyMapName("TSBShiftId")]
        public int TSBShiftId
        {
            get
            {
                return _TSBShiftId;
            }
            set
            {
                if (_TSBShiftId != value)
                {
                    _TSBShiftId = value;
                    this.RaiseChanged("TSBShiftId");
                }
            }
        }
        /// <summary>
        /// Gets or sets TSBId.
        /// </summary>
        [MaxLength(10)]
        [PeropertyMapName("TSBId")]
        public string TSBId
        {
            get
            {
                return _TSBId;
            }
            set
            {
                if (_TSBId != value)
                {
                    _TSBId = value;
                    this.RaiseChanged("TSBId");
                }
            }
        }
        /// <summary>
        /// Gets or sets ShiftId.
        /// </summary>
        [PeropertyMapName("ShiftId")]
        public int ShiftId
        {
            get
            {
                return _ShiftId;
            }
            set
            {
                if (_ShiftId != value)
                {
                    _ShiftId = value;
                    this.RaiseChanged("ShiftId");
                }
            }
        }
        /// <summary>
        /// Gets or sets UserId
        /// </summary>
        [MaxLength(10)]
        [PeropertyMapName("UserId")]
        public string UserId
        {
            get
            {
                return _UserId;
            }
            set
            {
                if (_UserId != value)
                {
                    _UserId = value;
                    this.RaiseChanged("UserId");
                }
            }
        }
        /// <summary>
        /// Gets or sets Begin Date.
        /// </summary>
        [PeropertyMapName("Begin")]
        public DateTime Begin
        {
            get { return _Begin; }
            set
            {
                if (_Begin != value)
                {
                    _Begin = value;
                    // Raise event.
                    RaiseChanged("Begin");
                    RaiseChanged("BeginDateString");
                    RaiseChanged("BeginTimeString");
                }
            }
        }
        /// <summary>
        /// Gets or sets End Date.
        /// </summary>
        [PeropertyMapName("End")]
        public DateTime End
        {
            get { return _End; }
            set
            {
                if (_End != value)
                {
                    _End = value;
                    // Raise event.
                    RaiseChanged("End");
                    RaiseChanged("EndDateString");
                    RaiseChanged("EndTimeString");
                }
            }
        }
        /// <summary>
        /// Gets Begin Date String.
        /// </summary>
        [JsonIgnore]
        [Ignore]
        public string BeginDateString
        {
            get
            {
                var ret = (this.Begin == DateTime.MinValue) ? "" : this.Begin.ToThaiDateTimeString("dd/MM/yyyy");
                return ret;
            }
            set { }
        }
        /// <summary>
        /// Gets End Date String.
        /// </summary>
        [JsonIgnore]
        [Ignore]
        public string EndDateString
        {
            get
            {
                var ret = (this.End == DateTime.MinValue) ? "" : this.End.ToThaiDateTimeString("dd/MM/yyyy");
                return ret;
            }
            set { }
        }
        /// <summary>
        /// Gets Begin Time String.
        /// </summary>
        [JsonIgnore]
        [Ignore]
        public string BeginTimeString
        {
            get
            {
                var ret = (this.Begin == DateTime.MinValue) ? "" : this.Begin.ToThaiTimeString();
                return ret;
            }
            set { }
        }
        /// <summary>
        /// Gets End Time String.
        /// </summary>
        [JsonIgnore]
        [Ignore]
        public string EndTimeString
        {
            get
            {
                var ret = (this.End == DateTime.MinValue) ? "" : this.End.ToThaiTimeString();
                return ret;
            }
            set { }
        }
        /// <summary>
        /// Gets or sets Status (1 = Sync, 0 = Unsync, etc..)
        /// </summary>
        [PeropertyMapName("Status")]
        public int Status
        {
            get
            {
                return _Status;
            }
            set
            {
                if (_Status != value)
                {
                    _Status = value;
                    this.RaiseChanged("Status");
                }
            }
        }
        /// <summary>
        /// Gets or sets LastUpdated (Sync to DC).
        /// </summary>
        [PeropertyMapName("LastUpdate")]
        public DateTime LastUpdate
        {
            get { return _LastUpdate; }
            set
            {
                if (_LastUpdate != value)
                {
                    _LastUpdate = value;
                    this.RaiseChanged("LastUpdate");
                }
            }
        }

        #endregion

        #region Static Methods

        public static TSBShift Create(Shift shift, User supervisor)
        {
            TSBShift inst = Create();
            TSB tsb = TSB.GetCurrent();
            if (null != tsb) tsb.AssignTo(inst);
            if (null != shift) shift.AssignTo(inst);
            if (null != supervisor) supervisor.AssignTo(inst);
            return inst;
        }

        public static void ChangeShift(TSBShift shift)
        {
            lock (sync)
            {
                if (null == shift) return;
                var last = GetCurrent();
                if (null != last)
                {
                    // End shift.
                    last.End = DateTime.Now;
                    Save(last);
                }
                // Begin new shift.
                shift.Begin = DateTime.Now;
                Save(shift);
            }
        }

        public static TSBShift GetCurrent()
        {
            lock (sync)
            {
                string cmd = string.Empty;
                cmd += "SELECT * FROM TSBShift ";
                cmd += " WHERE End = ? ";
                return NQuery.Query<TSBShift>(cmd, DateTime.MinValue).FirstOrDefault();
            }
        }

        #endregion
    }

    #endregion

    #region TSBShiftCreate

    public class TSBShiftCreate
    {
        public Shift Shift { get; set; }
        public User User { get; set; }
    }

    #endregion
}
