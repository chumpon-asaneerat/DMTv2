#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace DMT.Models
{
    public class SCWJob
    {
        public int? networkId { get; set; }
        public int? plazaId { get; set; }
        public int? laneId { get; set; }
        public int? jobNo { get; set; }
        public string staffId { get; set; }
        public DateTime? bojDateTime { get; set; }
        public DateTime? eojDateTime { get; set; }
    }

    public class SCWJobList
    {
        public List<SCWJob> list { get; set; }
        public SCWStatus status { get; set; }
    }
}

/*

{
    "list": [
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
    "status": {
        "code": "S200",
        "message": "Success"
    }
}

*/