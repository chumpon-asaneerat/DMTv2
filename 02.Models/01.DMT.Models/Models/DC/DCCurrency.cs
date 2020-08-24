﻿#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace DMT.Models
{
    public class DCCurrency
    {
        public int currencyId { get; set; }
        public int currencyDenomId { get; set; }
        public string abbreviation { get; set; }
        public string description { get; set; }
        public decimal denomValue { get; set; }
        public int denomTypeId { get; set; }
    }

    public class DCCurrencyList
    {
        public List<DCCurrency> list { get; set; }
        public DCStatus status { get; set; }
    }
}

/*

{
    "list": [
        {
            "currencyId": 1,
            "currencyDenomId": 1,
            "abbreviation": "Satang25",
            "description": "25 Satang",
            "denomValue": 0.25,
            "denomTypeId": 2
        },
        {
            "currencyId": 1,
            "currencyDenomId": 2,
            "abbreviation": "Satang50",
            "description": "50 Satang",
            "denomValue": 0.5,
            "denomTypeId": 2
        },
        {
            "currencyId": 1,
            "currencyDenomId": 3,
            "abbreviation": "Baht1",
            "description": "1 Baht",
            "denomValue": 1.0,
            "denomTypeId": 2
        },
        {
            "currencyId": 1,
            "currencyDenomId": 4,
            "abbreviation": "Baht2",
            "description": "2 Baht",
            "denomValue": 2.0,
            "denomTypeId": 2
        },
        {
            "currencyId": 1,
            "currencyDenomId": 5,
            "abbreviation": "Baht5",
            "description": "5 Baht",
            "denomValue": 5.0,
            "denomTypeId": 2
        },
        {
            "currencyId": 1,
            "currencyDenomId": 6,
            "abbreviation": "CBaht10",
            "description": "10 Baht",
            "denomValue": 10.0,
            "denomTypeId": 2
        },
        {
            "currencyId": 1,
            "currencyDenomId": 7,
            "abbreviation": "NBaht10",
            "description": "10 Baht",
            "denomValue": 10.0,
            "denomTypeId": 1
        },
        {
            "currencyId": 1,
            "currencyDenomId": 8,
            "abbreviation": "NBaht20",
            "description": "20 Baht",
            "denomValue": 20.0,
            "denomTypeId": 1
        },
        {
            "currencyId": 1,
            "currencyDenomId": 9,
            "abbreviation": "NBaht50",
            "description": "50 Baht",
            "denomValue": 50.0,
            "denomTypeId": 1
        },
        {
            "currencyId": 1,
            "currencyDenomId": 10,
            "abbreviation": "NBaht100",
            "description": "100 Baht",
            "denomValue": 100.0,
            "denomTypeId": 1
        },
        {
            "currencyId": 1,
            "currencyDenomId": 11,
            "abbreviation": "NBaht500",
            "description": "500 Baht",
            "denomValue": 500.0,
            "denomTypeId": 1
        },
        {
            "currencyId": 1,
            "currencyDenomId": 12,
            "abbreviation": "NBaht1000",
            "description": "1000 Baht",
            "denomValue": 1000.0,
            "denomTypeId": 1
        }
    ],
    "status": {
        "code": "S200",
        "message": "Success"
    }
}

*/