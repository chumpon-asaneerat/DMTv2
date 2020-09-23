﻿#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace DMT.Models
{
    public class SCWQRCode
    {
        public DateTime? trxDateTime { get; set; }
        public decimal? amount { get; set; }
        public string approvCode { get; set; }
        public string refNo { get; set; }
    }

    public class SCWQRCodeResult
    {
        public List<SCWQRCode> list { get; set; }
        public SCWStatus status { get; set; }
    }
}
/*
{
    "networkId": 31,
    "plazaId": 1,
    "staffId": "41023",
    "startDateTime": "2020-01-03T00:00:00.000+07",
    "endDateTime": "2020-09-03T23:59:59.999+07"
}
*/
/*
{
    "list": [
        {
            "trxDateTime": "2020-06-29T17:48:40.515+07",
            "amount": 40.0,
            "approvCode": "approveCode",
            "refNo": "refNo"
        }
    ],
    "status": {
        "code": "S200",
        "message": "Success"
    }
}
*/