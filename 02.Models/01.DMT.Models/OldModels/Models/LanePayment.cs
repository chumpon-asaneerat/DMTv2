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
