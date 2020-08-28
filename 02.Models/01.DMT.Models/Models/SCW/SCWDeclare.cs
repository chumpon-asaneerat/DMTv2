#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace DMT.Models
{
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

        public List<SCWJobList> jobList { get; set; }

        //TODO: Create Type for each list.
        public List<object> cashList { get; set; }
        public List<object> couponList { get; set; }
        public List<object> couponBookList { get; set; }
        public List<object> cardAllowList { get; set; }
        public List<object> qrcodeList { get; set; }
        public List<object> emvList { get; set; }
    }
}

/*

{
    "networkId": 31,
    "plazaId": 3101,
    "staffId": "14124",
    "bagNumber": "12345",
    "safetyBeltNumber": "A12345",
    "cashTotalAmount": 7000,
    "couponTotalAmount": 160,
    "couponBookTotalAmount": 1330,
    "cardAllowTotalAmount": 0,
    "qrcodeTotalAmount": 0,
    "emvTotalAmount": 0,
    "otherTotalAmount": 5000,
    "cashRemark": "หมายเหตุ เงินสด 7000",
    "otherRemark": "หมายเหตุ เงินอื่นๆ 0 บาท",
    "jobList": [
        {
            "networkId": 31,
            "plazaId": 3101,
            "laneId": 1,
            "jobNo": 21,
            "staffId": "14124",
            "bojDateTime": "2020-08-15T01:13:09.000Z",
            "eojDateTime": "2020-08-15T01:13:12.000Z"
        },
        {
            "networkId": 31,
            "plazaId": 3101,
            "laneId": 1,
            "jobNo": 22,
            "staffId": "14124",
            "bojDateTime": "2020-08-15T01:16:44.000Z",
            "eojDateTime": "2020-08-15T01:23:44.000Z"
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
            "couponBookValue": 665,
            "number": 2,
            "total": 1330
        }
    ],
    "cardAllowList": [],
    "qrcodeList": [],
    "emvList": []
}

*/