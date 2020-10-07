using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMT.Models
{
    public class SCWChiefOfDuty
    {
        public int? networkId { get; set; }
        public int? plazaId { get; set; }
        public string staffId { get; set; }
        // ประเภทพนักงาน 1=chief, 2=sup
        public int? staffTypeId { get; set; } // always 1
        public DateTime? beginDateTime { get; set; }
    }

    public class SCWChiefOfDutyResult
    {
        public SCWStatus status { get; set; }
    }
}

/*
{
    "networkId": 31,
    "plazaId": 1,
    "staffId": "00333",
    "staffTypeId": 1, // ประเภทพนักงาน 1=chief, 2=sup
    "beginDateTime": "2020-08-06T00:00:00.000+07"
} 
*/
/*
{
    "status": {
        "code": "S200",
        "message": "Success"
    }
}

*/