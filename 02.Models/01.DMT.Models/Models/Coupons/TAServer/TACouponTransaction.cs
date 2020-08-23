#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace DMT.Models
{
    public class TAServerCouponTransaction
    {
        public int? CouponPK { get; set; }

        //public int? TransactionId { get; set; }

        public DateTime? TransactionDate { get; set; }

        public string TSBId { get; set; }

        public int? CouponType { get; set; }
        public string SerialNo { get; set; }
        public decimal? Price { get; set; }
        public int? CouponStatus { get; set; }

        public string LaneId { get; set; }
        public string UserId { get; set; }
        public DateTime? UserReceiveDate { get; set; }

        public DateTime? SoldDate { get; set; }
        public string SoldBy { get; set; }

        public int? FinishFlag { get; set; }

        public int? SapChooseFlag { get; set; }
        public DateTime? SapChooseDate { get; set; }
    }
}
