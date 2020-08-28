#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace DMT.Models
{
    public class SCWCoupon
    {
        public int? couponId { get; set; }
        public decimal? couponValue { get; set; }
        public string abbreviation { get; set; }
        public string description { get; set; }
    }

    public class SCWCouponList
    {
        public List<SCWCouponList> list { get; set; }
        public SCWStatus status { get; set; }
    }
}

/*
{
    "list": [
        {
            "couponId": 1,
            "couponValue": 30.0,
            "abbreviation": "30",
            "description": "30 บาท"
        },
        {
            "couponId": 2,
            "couponValue": 35.0,
            "abbreviation": "35",
            "description": "35 บาท"
        },
        {
            "couponId": 3,
            "couponValue": 70.0,
            "abbreviation": "70",
            "description": "70 บาท"
        },
        {
            "couponId": 4,
            "couponValue": 80.0,
            "abbreviation": "80",
            "description": "80 บาท"
        }
    ],
    "status": {
        "code": "S200",
        "message": "Success"
    }
}
*/