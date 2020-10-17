#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NLib;
using NLib.Design;
using NLib.Reflection;

#endregion

namespace DMT.Models
{
	public class SCWCardAllow
	{
		[PropertyMapName("cardAllowId")]
		public int cardAllowId { get; set; }

		[PropertyMapName("abbreviation")]
		public string abbreviation { get; set; }

		[PropertyMapName("description")]
		public string description { get; set; }
	}

	public class SCWCardAllowList
	{
		public List<SCWCardAllow> list { get; set; }
		public SCWStatus status { get; set; }
	}
}

/*

{
	"list": [
		{
			"cardAllowId": 1,
			"abbreviation": "Card DMT P1",
			"description": "บัตร DMT (ป 1)"
		},
		{
			"cardAllowId": 2,
			"abbreviation": "Card DMT P2",
			"description": "บัตร DMT (ป 2)"
		}
	],
	"status": {
		"code": "S200",
		"message": "Success"
	}
}

*/