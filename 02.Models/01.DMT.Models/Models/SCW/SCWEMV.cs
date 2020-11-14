#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

#endregion

namespace DMT.Models
{
    [JsonObject(MemberSerialization.OptOut)]
    public class SCWEMV
    {
        public DateTime? trxDateTime { get; set; }
        public decimal? amount { get; set; }
        public string approvCode { get; set; }
        public string refNo { get; set; }
        public string staffId { get; set; }
        public string staffNameTh { get; set; }
        public string staffNameEn { get; set; }
        public int laneId { get; set; }

        [JsonIgnore]
        public string trxDateTimeString 
        { 
            get 
            {
                if (!trxDateTime.HasValue) return string.Empty;
                return (trxDateTime.Value == DateTime.MinValue) ? string.Empty : trxDateTime.Value.ToThaiDateTimeString("dd/MM/yyyy HH:mm:ss");
            } 
            set { } 
        }

        public override int GetHashCode()
        {
            decimal amt = (amount.HasValue) ? amount.Value : decimal.Zero;
            DateTime dt = (trxDateTime.HasValue) ?
                trxDateTime.Value : DateTime.MinValue;
            string dtStr = dt.ToDateTimeString();
            string value = string.Format("{0}_{1}_{2}_{3}_{4}_{5}_{6}_{7}",
                this.staffId, this.staffNameEn, this.staffNameTh,
                this.laneId,
                this.refNo, this.approvCode, amt,
                dtStr);
            return value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (null == obj) return false;
            return obj.GetHashCode() == this.GetHashCode();
        }
    }

    public class SCWEMVResult
    {
        public List<SCWEMV> list { get; set; }
        public SCWStatus status { get; set; }
    }
}


/*
{
    "networkId": 31,
    "plazaId": 1,
    "staffId": "41023",
    "startDateTime": "2020-06-03T00:00:00.000+07",
    "endDateTime": "2020-09-03T23:59:59.999+07"
}
*/
/*
{
    "list": [
        {
            "trxDateTime": "2020-11-10T13:10:27.000+07",
            "amount": 35,
            "approvCode": "PCE7P2   ",
            "refNo": "10000221201110131009",
            "staffId": "00111",
            "staffNameTh": "นาย.ฟัยศรน์ เฮะดือเระ",
            "staffNameEn": "Mr.Qfree Qfree",
            "laneId": 5
        },
        {
            "trxDateTime": "2020-11-10T13:43:57.000+07",
            "amount": 35,
            "approvCode": "3TSJ9E   ",
            "refNo": "10000221201110134321",
            "staffId": "00111",
            "staffNameTh": "นาย.ฟัยศรน์ เฮะดือเระ",
            "staffNameEn": "Mr.Qfree Qfree",
            "laneId": 5
        },
        {
            "trxDateTime": "2020-11-10T13:44:28.000+07",
            "amount": 35,
            "approvCode": "CQA28X   ",
            "refNo": "10000221201110134408",
            "staffId": "00111",
            "staffNameTh": "นาย.ฟัยศรน์ เฮะดือเระ",
            "staffNameEn": "Mr.Qfree Qfree",
            "laneId": 5
        }
    ],
    "status": {
        "code": "S200",
        "message": "Success"
    }
}
*/