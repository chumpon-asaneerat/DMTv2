using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMT.Models.SCW
{
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