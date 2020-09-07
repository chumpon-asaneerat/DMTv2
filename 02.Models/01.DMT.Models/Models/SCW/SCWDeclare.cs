#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace DMT.Models
{
    public class SCWDeclareCash
    {
        public int currencyId { get; set; }
        public int currencyDenomId { get; set; }
        public decimal denomValue { get; set; }

        public int number { get; set; }
        public decimal total { get; set; }
    }

    public class SCWDeclareCoupon
    {
        public int couponId { get; set; }
        public decimal couponValue { get; set; }

        public int number { get; set; }
        public decimal total { get; set; }
    }

    public class SCWDeclareCouponBook
    {
        public int couponBookId { get; set; }
        public decimal couponBookValue { get; set; }
        public int number { get; set; }
        public decimal total { get; set; }
    }

    public class SCWDeclareFreePass
    {
        public int cardAllowId { get; set; }
        public int number { get; set; }
    }

    public class SCWDeclareQRCode
    {
        public string approvalCode { get; set; }
        public DateTime trxDate { get; set; }
        public decimal amount { get; set; }
    }

    public class SCWDeclareEMV
    {
        public string approvalCode { get; set; }
        public DateTime trxDate { get; set; }
        public decimal amount { get; set; }
    }

    public class SCWDeclare
    {
        public int? networkId { get; set; }
        public int? plazaId { get; set; }
        public string staffId { get; set; }
        public string bagNumber { get; set; }
        public string safetyBeltNumber { get; set; }
        public decimal? cashTotalAmount { get; set; }
        public decimal? couponTotalAmount { get; set; }
        public decimal? couponBookTotalAmount { get; set; }
        public decimal? cardAllowTotalAmount { get; set; }
        public decimal? qrcodeTotalAmount { get; set; }
        public decimal? emvTotalAmount { get; set; }
        public decimal? otherTotalAmount { get; set; }
        public string cashRemark { get; set; }
        public string otherRemark { get; set; }

        public string chiefId { get; set; }
        public string chiefName { get; set; }
        public int? shiftTypeId { get; set; }

        public DateTime? declareDateTime { get; set; }
        public DateTime? attendanceDateTime { get; set; }
        public DateTime? departureDateTime { get; set; }
        public DateTime? operationDate { get; set; }

        public List<SCWJob> jobList { get; set; }

        public List<SCWDeclareCash> cashList { get; set; }
        public List<SCWDeclareCoupon> couponList { get; set; }
        public List<SCWDeclareCouponBook> couponBookList { get; set; }
        public List<SCWDeclareFreePass> cardAllowList { get; set; }
        public List<SCWDeclareQRCode> qrcodeList { get; set; }
        public List<SCWDeclareEMV> emvList { get; set; }
    }
}

/*

{
    "networkId": 31,
    "plazaId": 1,
    "staffId": "00114",
    "bagNumber": "232323",
    "safetyBeltNumber": "232323",
    "cashTotalAmount": 7000,
    "couponTotalAmount": 7000,
    "couponBookTotalAmount": 20,
    "cardAllowTotalAmount": 0,
    "qrcodeTotalAmount": 0,
    "emvTotalAmount": 0,
    "otherTotalAmount": 5000,
    "cashRemark": "หมายเหตุ เงินสด 7000",
    "otherRemark": "หมายเหตุ เงินอื่นๆ 0 บาท",
    "chiefId": "00333",
    "chiefName": "นายทดสอบ ระบบ",
    "shiftTypeId": 3,
    "declareDateTime": "2020-09-03T08:10:22.000+07",
    "attendanceDateTime": "2020-09-03T05:02:00.000+07",
    "departureDateTime": "2020-09-03T10:10:00.000+07",
    "operationDate": "2020-09-03T00:00:00.000+07",
    "jobList": [
        {
            "networkId": 31,
            "plazaId": 1,
            "laneId": 1,
            "jobNo": 39,
            "staffId": "41023",
            "bojDateTime": "2020-08-07T13:28:11.000+07",
            "eojDateTime": "2020-08-07T13:58:34.000+07"
        },
        {
            "networkId": 31,
            "plazaId": 1,
            "laneId": 1,
            "jobNo": 41,
            "staffId": "41023",
            "bojDateTime": "2020-08-07T14:33:14.000+07",
            "eojDateTime": "2020-08-07T14:43:43.000+07"
        }
    ],
    "cashList": [
        {
            "currencyId": 1,
            "currencyDenomId": 5,
            "denomValue": 5,
            "number": 400,
            "total": 2000
        },
        {
            "currencyId": 1,
            "currencyDenomId": 6,
            "denomValue": 10,
            "number": 500,
            "total": 5000
        }
    ],
    "couponList": [
        {
            "couponId": 4,
            "couponValue": 80,
            "number": 2,
            "total": 160
        }
    ],
    "couponBookList": [
        {
            "couponBookId": 1,
            "couponBookValue": 35,
            "number": 2,
            "total": 70
        }
    ],
    "cardAllowList": [],
    "qrcodeList": [],
    "emvList": []
}

*/