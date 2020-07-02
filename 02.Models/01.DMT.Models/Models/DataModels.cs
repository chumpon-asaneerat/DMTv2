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
    #region Payment

    /// <summary>
    /// The Payment Data Model Class.
    /// </summary>
    //[Table("Payment")]
    public class Payment : NTable<Payment>
    {
        #region Intenral Variables

        private string _PaymentId = string.Empty;
        private string _PaymentNameEN = string.Empty;
        private string _PaymentNameTH = string.Empty;

        private int _Status = 0;
        private DateTime _LastUpdate = DateTime.MinValue;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public Payment() : base() { }

        #endregion

        #region Public Proprties

        /// <summary>
        /// Gets or sets PaymentId
        /// </summary>
        [PrimaryKey, MaxLength(20)]
        [PeropertyMapName("PaymentId")]
        public string PaymentId
        {
            get
            {
                return _PaymentId;
            }
            set
            {
                if (_PaymentId != value)
                {
                    _PaymentId = value;
                    this.RaiseChanged("PaymentId");
                }
            }
        }
        /// <summary>
        /// Gets or sets PaymentNameEN
        /// </summary>
        [MaxLength(50)]
        [PeropertyMapName("PaymentNameEN")]
        public string PaymentNameEN
        {
            get
            {
                return _PaymentNameEN;
            }
            set
            {
                if (_PaymentNameEN != value)
                {
                    _PaymentNameEN = value;
                    this.RaiseChanged("PaymentNameEN");
                }
            }
        }
        /// <summary>
        /// Gets or sets PaymentNameTH
        /// </summary>
        [MaxLength(50)]
        [PeropertyMapName("PaymentNameTH")]
        public string PaymentNameTH
        {
            get
            {
                return _PaymentNameTH;
            }
            set
            {
                if (_PaymentNameTH != value)
                {
                    _PaymentNameTH = value;
                    this.RaiseChanged("PaymentNameTH");
                }
            }
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

        #endregion
    }

    #endregion

    #region LaneAttendance

    /// <summary>
    /// The LaneAttendance Data Model Class.
    /// </summary>
    //[Table("LaneAttendance")]
    public class LaneAttendance : NTable<LaneAttendance>
    {
        #region Intenral Variables

        private Guid _PKId = Guid.NewGuid();

        private string _JobId = string.Empty;

        private string _TSBId = string.Empty;
        private string _TSBNameEN = string.Empty;
        private string _TSBNameTH = string.Empty;

        private int _LaneNo = 0;
        private string _LaneId = string.Empty;
        private string _UserId = string.Empty;
        private string _FullNameEN = string.Empty;
        private string _FullNameTH = string.Empty;

        private DateTime _Begin = DateTime.MinValue;
        private DateTime _End = DateTime.MinValue;

        private int _Status = 0;
        private DateTime _LastUpdate = DateTime.MinValue;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public LaneAttendance() : base()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets PKId
        /// </summary>
        [PrimaryKey]
        [PeropertyMapName("PKId")]
        public Guid PKId
        {
            get
            {
                return _PKId;
            }
            set
            {
                if (_PKId != value)
                {
                    _PKId = value;
                    this.RaiseChanged("PKId");
                }
            }
        }
        /// <summary>
        /// Gets or sets JobId
        /// </summary>
        [MaxLength(20)]
        [PeropertyMapName("JobId")]
        public string JobId
        {
            get
            {
                return _JobId;
            }
            set
            {
                if (_JobId != value)
                {
                    _JobId = value;
                    this.RaiseChanged("JobId");
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
        /// Gets or sets TSBNameEN.
        /// </summary>
        [MaxLength(100)]
        [PeropertyMapName("TSBNameEN")]
        public string TSBNameEN
        {
            get
            {
                return _TSBNameEN;
            }
            set
            {
                if (_TSBNameEN != value)
                {
                    _TSBNameEN = value;
                    this.RaiseChanged("TSBNameEN");
                }
            }
        }
        /// <summary>
        /// Gets or sets TSBNameTH.
        /// </summary>
        [MaxLength(100)]
        [PeropertyMapName("TSBNameTH")]
        public string TSBNameTH
        {
            get
            {
                return _TSBNameTH;
            }
            set
            {
                if (_TSBNameTH != value)
                {
                    _TSBNameTH = value;
                    this.RaiseChanged("TSBNameTH");
                }
            }
        }
        /// <summary>
        /// Gets or sets Lane No.
        /// </summary>
        [PeropertyMapName("LaneNo")]
        public int LaneNo
        {
            get
            {
                return _LaneNo;
            }
            set
            {
                if (_LaneNo != value)
                {
                    _LaneNo = value;
                    this.RaiseChanged("LaneNo");
                }
            }
        }
        /// <summary>
        /// Gets or sets LaneId
        /// </summary>
        [MaxLength(10)]
        [PeropertyMapName("LaneId")]
        public string LaneId
        {
            get
            {
                return _LaneId;
            }
            set
            {
                if (_LaneId != value)
                {
                    _LaneId = value;
                    this.RaiseChanged("LaneId");
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
        /// Gets or sets FullNameEN
        /// </summary>
        [MaxLength(100)]
        [PeropertyMapName("FullNameEN")]
        public string FullNameEN
        {
            get
            {
                return _FullNameEN;
            }
            set
            {
                if (_FullNameEN != value)
                {
                    _FullNameEN = value;
                    this.RaiseChanged("FullNameEN");
                }
            }
        }
        /// <summary>
        /// Gets or sets FullNameTH
        /// </summary>
        [MaxLength(100)]
        [PeropertyMapName("FullNameTH")]
        public string FullNameTH
        {
            get
            {
                return _FullNameTH;
            }
            set
            {
                if (_FullNameTH != value)
                {
                    _FullNameTH = value;
                    this.RaiseChanged("FullNameTH");
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

        public static LaneAttendance Create(Lane lane, User supervisor)
        {
            LaneAttendance inst = Create();
            TSB tsb = TSB.GetCurrent();
            if (null != tsb) tsb.AssignTo(inst);
            if (null != lane) lane.AssignTo(inst);
            if (null != supervisor) supervisor.AssignTo(inst);
            return inst;
        }

        public static List<LaneAttendance> Search(UserShift shift)
        {
            if (null == shift) return new List<LaneAttendance>();
            lock (sync)
            {
                string cmd = string.Empty;
                cmd += "SELECT * FROM LaneAttendance ";
                cmd += " WHERE Begin >= ? ";
                cmd += "   AND End <= ? ";
                return NQuery.Query<LaneAttendance>(
                    cmd,
                    shift.Begin,
                    shift.End,
                    DateTime.MinValue).ToList();
            }
        }

        public static List<LaneAttendance> Search(Lane lane)
        {
            if (null == lane) return new List<LaneAttendance>();
            lock (sync)
            {
                string cmd = string.Empty;
                cmd += "SELECT * FROM LaneAttendance ";
                cmd += " WHERE LaneId = ? ";
                return NQuery.Query<LaneAttendance>(
                    cmd,
                    lane.LaneId).ToList();
            }
        }

        public static LaneAttendance GetCurrentByLane(Lane lane)
        {
            if (null == lane) return null;
            lock (sync)
            {
                string cmd = string.Empty;
                cmd += "SELECT * FROM LaneAttendance ";
                cmd += " WHERE LaneId = ? ";
                cmd += "   AND End = ? ";
                return NQuery.Query<LaneAttendance>(
                    cmd,
                    lane.LaneId,
                    DateTime.MinValue).FirstOrDefault();
            }
        }

        public static List<LaneAttendance> Search(DateTime date)
        {
            if (null == date || date == DateTime.MinValue) return new List<LaneAttendance>();
            lock (sync)
            {
                string cmd = string.Empty;
                cmd += "SELECT * FROM LaneAttendance ";
                cmd += " WHERE Begin >= ? ";
                cmd += "   AND End <= ? ";
                return NQuery.Query<LaneAttendance>(
                    cmd,
                    date,
                    DateTime.MinValue).ToList();
            }
        }

        #endregion
    }

    public class LaneAttendanceCreate
    {
        public Lane Lane { get; set; }
        public User User { get; set; }
    }

    #endregion

    #region LanePayment

    /// <summary>
    /// The LanePayment Data Model Class.
    /// </summary>
    //[Table("LanePayment")]
    public class LanePayment : NTable<LanePayment>
    {
        #region Intenral Variables

        private Guid _PKId = Guid.NewGuid();

        private string _ApproveCode = string.Empty;

        private string _TSBId = string.Empty;
        private string _TSBNameEN = string.Empty;
        private string _TSBNameTH = string.Empty;

        private int _LaneNo = 0;
        private string _LaneId = string.Empty;
        private string _UserId = string.Empty;
        private string _FullNameEN = string.Empty;
        private string _FullNameTH = string.Empty;

        private string _PaymentId = string.Empty;
        private string _PaymentNameEN = string.Empty;
        private string _PaymentNameTH = string.Empty;

        private DateTime _PaymentDate = DateTime.MinValue;
        private decimal _Amount = decimal.Zero;

        private int _Status = 0;
        private DateTime _LastUpdate = DateTime.MinValue;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public LanePayment() : base()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets PKId
        /// </summary>
        [PrimaryKey]
        [PeropertyMapName("PKId")]
        public Guid PKId
        {
            get
            {
                return _PKId;
            }
            set
            {
                if (_PKId != value)
                {
                    _PKId = value;
                    this.RaiseChanged("PKId");
                }
            }
        }
        /// <summary>
        /// Gets or sets Approve Code.
        /// </summary>
        [MaxLength(20)]
        [PeropertyMapName("ApproveCode")]
        public string ApproveCode
        {
            get
            {
                return _ApproveCode;
            }
            set
            {
                if (_ApproveCode != value)
                {
                    _ApproveCode = value;
                    this.RaiseChanged("ApproveCode");
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
        /// Gets or sets TSBNameEN.
        /// </summary>
        [MaxLength(100)]
        [PeropertyMapName("TSBNameEN")]
        public string TSBNameEN
        {
            get
            {
                return _TSBNameEN;
            }
            set
            {
                if (_TSBNameEN != value)
                {
                    _TSBNameEN = value;
                    this.RaiseChanged("TSBNameEN");
                }
            }
        }
        /// <summary>
        /// Gets or sets TSBNameTH.
        /// </summary>
        [MaxLength(100)]
        [PeropertyMapName("TSBNameTH")]
        public string TSBNameTH
        {
            get
            {
                return _TSBNameTH;
            }
            set
            {
                if (_TSBNameTH != value)
                {
                    _TSBNameTH = value;
                    this.RaiseChanged("TSBNameTH");
                }
            }
        }
        /// <summary>
        /// Gets or sets Lane No.
        /// </summary>
        [PeropertyMapName("LaneNo")]
        public int LaneNo
        {
            get
            {
                return _LaneNo;
            }
            set
            {
                if (_LaneNo != value)
                {
                    _LaneNo = value;
                    this.RaiseChanged("LaneNo");
                }
            }
        }
        /// <summary>
        /// Gets or sets LaneId
        /// </summary>
        [MaxLength(10)]
        [PeropertyMapName("LaneId")]
        public string LaneId
        {
            get
            {
                return _LaneId;
            }
            set
            {
                if (_LaneId != value)
                {
                    _LaneId = value;
                    this.RaiseChanged("LaneId");
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
        /// Gets or sets FullNameEN
        /// </summary>
        [MaxLength(100)]
        [PeropertyMapName("FullNameEN")]
        public string FullNameEN
        {
            get
            {
                return _FullNameEN;
            }
            set
            {
                if (_FullNameEN != value)
                {
                    _FullNameEN = value;
                    this.RaiseChanged("FullNameEN");
                }
            }
        }
        /// <summary>
        /// Gets or sets FullNameTH
        /// </summary>
        [MaxLength(100)]
        [PeropertyMapName("FullNameTH")]
        public string FullNameTH
        {
            get
            {
                return _FullNameTH;
            }
            set
            {
                if (_FullNameTH != value)
                {
                    _FullNameTH = value;
                    this.RaiseChanged("FullNameTH");
                }
            }
        }
        /// <summary>
        /// Gets or sets PaymentId
        /// </summary>
        [MaxLength(20)]
        [PeropertyMapName("PaymentId")]
        public string PaymentId
        {
            get
            {
                return _PaymentId;
            }
            set
            {
                if (_PaymentId != value)
                {
                    _PaymentId = value;
                    this.RaiseChanged("PaymentId");
                }
            }
        }
        /// <summary>
        /// Gets or sets PaymentNameEN
        /// </summary>
        [MaxLength(50)]
        [PeropertyMapName("PaymentNameEN")]
        public string PaymentNameEN
        {
            get
            {
                return _PaymentNameEN;
            }
            set
            {
                if (_PaymentNameEN != value)
                {
                    _PaymentNameEN = value;
                    this.RaiseChanged("PaymentNameEN");
                }
            }
        }
        /// <summary>
        /// Gets or sets PaymentNameTH
        /// </summary>
        [MaxLength(50)]
        [PeropertyMapName("PaymentNameTH")]
        public string PaymentNameTH
        {
            get
            {
                return _PaymentNameTH;
            }
            set
            {
                if (_PaymentNameTH != value)
                {
                    _PaymentNameTH = value;
                    this.RaiseChanged("PaymentNameTH");
                }
            }
        }
        /// <summary>
        /// Gets or sets Payment Date.
        /// </summary>
        [PeropertyMapName("PaymentDate")]
        public DateTime PaymentDate
        {
            get { return _PaymentDate; }
            set
            {
                if (_PaymentDate != value)
                {
                    _PaymentDate = value;
                    // Raise event.
                    RaiseChanged("PaymentDate");
                    RaiseChanged("PaymentDateString");
                    RaiseChanged("PaymentTimeString");
                }
            }
        }
        /// <summary>
        /// Gets Begin Date String.
        /// </summary>
        [JsonIgnore]
        [Ignore]
        public string PaymentDateString
        {
            get
            {
                var ret = (this.PaymentDate == DateTime.MinValue) ? "" : this.PaymentDate.ToThaiDateTimeString("dd/MM/yyyy");
                return ret;
            }
            set { }
        }
        /// <summary>
        /// Gets Begin Time String.
        /// </summary>
        [JsonIgnore]
        [Ignore]
        public string PaymentTimeString
        {
            get
            {
                var ret = (this.PaymentDate == DateTime.MinValue) ? "" : this.PaymentDate.ToThaiTimeString();
                return ret;
            }
            set { }
        }
        /// <summary>
        /// Gets or sets Amount.
        /// </summary>
        [PeropertyMapName("Amount")]
        public decimal Amount
        {
            get { return _Amount; }
            set
            {
                if (_Amount != value)
                {
                    _Amount = value;
                    // Raise event.
                    RaiseChanged("Amount");
                }
            }
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

        public static LanePayment Create(Lane lane, User collector,
            Payment payment, DateTime date, decimal amount)
        {
            LanePayment inst = Create();
            TSB tsb = TSB.GetCurrent();
            if (null != tsb) tsb.AssignTo(inst);
            if (null != lane) lane.AssignTo(inst);
            if (null != collector) collector.AssignTo(inst);
            if (null != payment) payment.AssignTo(inst);
            inst.PaymentDate = date;
            inst.Amount = amount;
            return inst;
        }

        public static List<LanePayment> Search(UserShift shift)
        {
            if (null == shift) return new List<LanePayment>();
            lock (sync)
            {
                string cmd = string.Empty;
                cmd += "SELECT * FROM LanePayment ";
                cmd += " WHERE Begin >= ? ";
                cmd += "   AND End <= ? ";
                return NQuery.Query<LanePayment>(
                    cmd,
                    shift.Begin,
                    shift.End,
                    DateTime.MinValue).ToList();
            }
        }

        public static List<LanePayment> Search(Lane lane)
        {
            if (null == lane) return new List<LanePayment>();
            lock (sync)
            {
                string cmd = string.Empty;
                cmd += "SELECT * FROM LanePayment ";
                cmd += " WHERE LaneId = ? ";
                return NQuery.Query<LanePayment>(
                    cmd,
                    lane.LaneId).ToList();
            }
        }

        public static LanePayment GetCurrentByLane(Lane lane)
        {
            if (null == lane) return null;
            lock (sync)
            {
                string cmd = string.Empty;
                cmd += "SELECT * FROM LanePayment ";
                cmd += " WHERE LaneId = ? ";
                cmd += "   AND End = ? ";
                return NQuery.Query<LanePayment>(
                    cmd,
                    lane.LaneId,
                    DateTime.MinValue).FirstOrDefault();
            }
        }

        public static List<LanePayment> Search(DateTime date)
        {
            if (null == date || date == DateTime.MinValue) return new List<LanePayment>();
            lock (sync)
            {
                string cmd = string.Empty;
                cmd += "SELECT * FROM LanePayment ";
                cmd += " WHERE Begin >= ? ";
                cmd += "   AND End <= ? ";
                return NQuery.Query<LanePayment>(
                    cmd,
                    date,
                    DateTime.MinValue).ToList();
            }
        }

        #endregion
    }

    public class LanePaymentCreate
    {
        public Lane Lane { get; set; }
        public User User { get; set; }
        public Payment Payment { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }

    #endregion
}

namespace DMT.Models
{
    #region Search nested classes.

    public static class Search
    {
        public static class Roles
        {
            public class ById : NSearch<ById>
            {
                public string RoleId { get; set; }

                public static ById Create(string roleId)
                {
                    var ret = new ById();
                    ret.RoleId = roleId;
                    return ret;
                }
            }
        }

        public static class Users
        {
            public class ByCardId : NSearch<ByCardId>
            {
                public string CardId { get; set; }

                public static ByCardId Create(string cardId)
                {
                    var ret = new ByCardId();
                    ret.CardId = cardId;
                    return ret;
                }
            }

            public class ByLogIn : NSearch<ByLogIn>
            {
                public string UserId { get; set; }
                public string Password { get; set; }

                public static ByLogIn Create(string userId, string pwd)
                {
                    var ret = new ByLogIn();
                    ret.UserId = userId;
                    ret.Password = pwd;
                    return ret;
                }
            }

            public class ById : NSearch<ById>
            {
                public string UserId { get; set; }

                public static ById Create(string userId)
                {
                    var ret = new ById();
                    ret.UserId = userId;
                    return ret;
                }
            }
        }

        public static class Lanes
        {
            public static class Current
            {
                public class AttendanceByLane : NSearch<AttendanceByLane>
                {
                    public Lane Lane { get; set; }

                    public static AttendanceByLane Create(Lane lane)
                    {
                        var ret = new AttendanceByLane();
                        ret.Lane = lane;
                        return ret;
                    }
                }

                public class PaymentByLane : NSearch<PaymentByLane>
                {
                    public Lane Lane { get; set; }

                    public static PaymentByLane Create(Lane lane)
                    {
                        var ret = new PaymentByLane();
                        ret.Lane = lane;
                        return ret;
                    }
                }
            }

            public static class Attendances
            {
                public class ByDate : NSearch<ByDate>
                {
                    public DateTime Date { get; set; }

                    public static ByDate Create(DateTime date)
                    {
                        var ret = new ByDate();
                        ret.Date = date;
                        return ret;
                    }
                }

                public class ByUserShift : NSearch<ByUserShift>
                {
                    public UserShift Shift { get; set; }

                    public static ByUserShift Create(UserShift shift)
                    {
                        var ret = new ByUserShift();
                        ret.Shift = shift;
                        return ret;
                    }
                }

                public class ByLane : NSearch<ByLane>
                {
                    public Lane Lane { get; set; }

                    public static ByLane Create(Lane lane)
                    {
                        var ret = new ByLane();
                        ret.Lane = lane;
                        return ret;
                    }
                }
            }

            public static class Payments
            {
                public class ByDate : NSearch<ByDate>
                {
                    public DateTime Date { get; set; }

                    public static ByDate Create(DateTime date)
                    {
                        var ret = new ByDate();
                        ret.Date = date;
                        return ret;
                    }
                }

                public class ByUserShift : NSearch<ByUserShift>
                {
                    public UserShift Shift { get; set; }

                    public static ByUserShift Create(UserShift shift)
                    {
                        var ret = new ByUserShift();
                        ret.Shift = shift;
                        return ret;
                    }
                }

                public class ByLane : NSearch<ByLane>
                {
                    public Lane Lane { get; set; }

                    public static ByLane Create(Lane lane)
                    {
                        var ret = new ByLane();
                        ret.Lane = lane;
                        return ret;
                    }
                }
            }
        }
    }

    #endregion
}
