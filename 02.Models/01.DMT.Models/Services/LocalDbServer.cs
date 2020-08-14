#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;
using System.Reflection;

// SQLite
using SQLite;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensions.Extensions;

using NLib;
using NLib.IO;

using DMT.Models;
using DMT.Views;

#endregion

namespace DMT.Services
{
	#region Configs(key) constants

	/// <summary>
	/// The Config key constants.
	/// </summary>
	public static class Configs
	{
		/// <summary>
		/// DC Config key constants.
		/// </summary>
		public static class DC
		{
			// for data center
			public static string network = "network";
			public static string tsb = "tsb";
			public static string terminal = "terminal";
		}
		/// <summary>
		/// Application Config key constants.
		/// </summary>
		public static class App
		{
			// For app.
			public static string TSBId = "app_tsb_id";
			public static string PlazaId = "app_plaza_id";
			public static string SupervisorId = "app_sup_id";
			public static string ShiftId = "app_shift_id";
		}
	}

	#endregion

	#region LobalDbServer

	/// <summary>
	/// Local Database Server.
	/// </summary>
	public class LocalDbServer
	{
		#region Singelton

		private static LocalDbServer _instance = null;
		/// <summary>
		/// Singelton Access.
		/// </summary>
		public static LocalDbServer Instance
		{
			get
			{
				if (null == _instance)
				{
					lock (typeof(LocalDbServer))
					{
						_instance = new LocalDbServer();
					}
				}
				return _instance;
			}
		}

		#endregion

		#region Internal Variables

		private int HistoryVersion = 1;

		#endregion

		#region Constructor and Destructor

		/// <summary>
		/// Constructor.
		/// </summary>
		private LocalDbServer() : base()
		{
			this.FileName = "TODxTA.db";
		}
		/// <summary>
		/// Destructor.
		/// </summary>
		~LocalDbServer()
		{

		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Gets local json folder path name.
		/// </summary>
		private static string LocalFolder
		{
			get
			{
				string localFilder = Folders.Combine(
					Folders.Assemblies.CurrentExecutingAssembly, "data");
				if (!Folders.Exists(localFilder))
				{
					Folders.Create(localFilder);
				}
				return localFilder;
			}
		}

		private void InitTables()
		{
			if (null == Db) return;
			Db.CreateTable<TSB>();
			Db.CreateTable<PlazaGroup>();
			Db.CreateTable<Plaza>();
			Db.CreateTable<Lane>();

			Db.CreateTable<Shift>();

			Db.CreateTable<Role>();
			Db.CreateTable<User>();

			Db.CreateTable<Payment>();

			Db.CreateTable<Config>();
			Db.CreateTable<ViewHistory>();

			Db.CreateTable<TSBShift>();
			Db.CreateTable<UserShift>();
			Db.CreateTable<UserShiftRevenue>();

			Db.CreateTable<LaneAttendance>();
			Db.CreateTable<LanePayment>();

			Db.CreateTable<RevenueEntry>();

			Db.CreateTable<TSBCreditTransaction>();

			Db.CreateTable<TSBCouponTransaction>();

			Db.CreateTable<UserCreditBalance>();
			Db.CreateTable<UserCreditTransaction>();

			//Db.CreateTable<UserCoupon>();
			Db.CreateTable<UserCouponTransaction>();

			Db.CreateTable<TSBExchangeTransaction>();

			InitDefaults();
		}

		private void InitDefaults()
		{
			InitTSBAndPlazaAndLanes();
			InitShifts();
			InitRoleAndUsers();
			InitPayments();
			InitConfigs();
			InitViews();
		}

		private void InitTSBAndPlazaAndLanes()
		{
			if (null == Db) return;

			if (Db.Table<TSB>().Count() > 0) return; // already exists.

			TSB item;
			PlazaGroup plazaGroup;
			Plaza plaza;
			Lane lane;

			#region DIN DAENG

			#region TSB

			item = new TSB();
			item.NetworkId = "31";
			item.TSBId = "311";
			item.TSBNameEN = "DIN DAENG";
			item.TSBNameTH = "ดินแดง";
			item.Active = true;
			if (!TSB.Exists(item)) TSB.Save(item);

			#endregion

			#region PlazaGroup DIN DAENG

			plazaGroup = new PlazaGroup()
			{
				PlazaGroupId = "DD",
				PlazaGroupNameEN = "DIN DAENG",
				PlazaGroupNameTH = "ดินแดง",
				Direction = "?",
				TSBId = item.TSBId
			};
			if (!PlazaGroup.Exists(plazaGroup)) PlazaGroup.Save(plazaGroup);

			#region Plaza DIN DAENG 1

			plaza = new Plaza()
			{
				PlazaId = "3101",
				PlazaNameEN = "DIN DAENG 1",
				PlazaNameTH = "ดินแดง 1",
				TSBId = item.TSBId,
				PlazaGroupId = plazaGroup.PlazaGroupId
			};
			if (!Plaza.Exists(plaza)) Plaza.Save(plaza);

			#region Lanes

			lane = new Lane()
			{
				LaneNo = 1,
				LaneId = "DD01",
				LaneType = "MTC",
				LaneAbbr = "DD01",
				TSBId = item.TSBId,
				PlazaGroupId = plazaGroup.PlazaGroupId,
				PlazaId = plaza.PlazaId
			};
			if (!Lane.Exists(lane)) Lane.Save(lane);
			lane = new Lane()
			{
				LaneNo = 2,
				LaneId = "DD02",
				LaneType = "MTC",
				LaneAbbr = "DD02",
				TSBId = item.TSBId,
				PlazaGroupId = plazaGroup.PlazaGroupId,
				PlazaId = plaza.PlazaId
			};
			if (!Lane.Exists(lane)) Lane.Save(lane);
			lane = new Lane()
			{
				LaneNo = 3,
				LaneId = "DD03",
				LaneType = "A/M",
				LaneAbbr = "DD03",
				TSBId = item.TSBId,
				PlazaGroupId = plazaGroup.PlazaGroupId,
				PlazaId = plaza.PlazaId
			};
			if (!Lane.Exists(lane)) Lane.Save(lane);
			lane = new Lane()
			{
				LaneNo = 4,
				LaneId = "DD04",
				LaneType = "ETC",
				LaneAbbr = "DD04",
				TSBId = item.TSBId,
				PlazaGroupId = plazaGroup.PlazaGroupId,
				PlazaId = plaza.PlazaId
			};
			if (!Lane.Exists(lane)) Lane.Save(lane);

			#endregion

			#endregion

			#region Plaza DIN DAENG 2

			plaza = new Plaza()
			{
				PlazaId = "3102",
				PlazaNameEN = "DIN DAENG 2",
				PlazaNameTH = "ดินแดง 2",
				TSBId = item.TSBId,
				PlazaGroupId = plazaGroup.PlazaGroupId
			};
			if (!Plaza.Exists(plaza)) Plaza.Save(plaza);

			#region Lanes

			lane = new Lane()
			{
				LaneNo = 11,
				LaneId = "DD11",
				LaneType = "?",
				LaneAbbr = "DD11",
				TSBId = item.TSBId,
				PlazaGroupId = plazaGroup.PlazaGroupId,
				PlazaId = plaza.PlazaId
			};
			if (!Lane.Exists(lane)) Lane.Save(lane);
			lane = new Lane()
			{
				LaneNo = 12,
				LaneId = "DD12",
				LaneType = "?",
				LaneAbbr = "DD12",
				TSBId = item.TSBId,
				PlazaGroupId = plazaGroup.PlazaGroupId,
				PlazaId = plaza.PlazaId
			};
			if (!Lane.Exists(lane)) Lane.Save(lane);
			lane = new Lane()
			{
				LaneNo = 13,
				LaneId = "DD13",
				LaneType = "?",
				LaneAbbr = "DD13",
				TSBId = item.TSBId,
				PlazaGroupId = plazaGroup.PlazaGroupId,
				PlazaId = plaza.PlazaId
			};
			if (!Lane.Exists(lane)) Lane.Save(lane);
			lane = new Lane()
			{
				LaneNo = 14,
				LaneId = "DD14",
				LaneType = "?",
				LaneAbbr = "DD14",
				TSBId = item.TSBId,
				PlazaGroupId = plazaGroup.PlazaGroupId,
				PlazaId = plaza.PlazaId
			};
			if (!Lane.Exists(lane)) Lane.Save(lane);
			lane = new Lane()
			{
				LaneNo = 15,
				LaneId = "DD15",
				LaneType = "?",
				LaneAbbr = "DD15",
				TSBId = item.TSBId,
				PlazaGroupId = plazaGroup.PlazaGroupId,
				PlazaId = plaza.PlazaId
			};
			if (!Lane.Exists(lane)) Lane.Save(lane);
			lane = new Lane()
			{
				LaneNo = 16,
				LaneId = "DD16",
				LaneType = "?",
				LaneAbbr = "DD16",
				TSBId = item.TSBId,
				PlazaGroupId = plazaGroup.PlazaGroupId,
				PlazaId = plaza.PlazaId
			};
			if (!Lane.Exists(lane)) Lane.Save(lane);

			#endregion

			#endregion

			#endregion

			#endregion

			#region SUTHISARN

			#region TSB

			item = new TSB();
			item.NetworkId = "31";
			item.TSBId = "312";
			item.TSBNameEN = "SUTHISARN";
			item.TSBNameTH = "สุทธิสาร";
			item.Active = false;
			if (!TSB.Exists(item)) TSB.Save(item);

			#endregion

			#region PlazaGroup SUTHISARN

			plazaGroup = new PlazaGroup()
			{
				PlazaGroupId = "SS",
				PlazaGroupNameEN = "SUTHISARN",
				PlazaGroupNameTH = "สุทธิสาร",
				Direction = "?",
				TSBId = item.TSBId
			};
			if (!PlazaGroup.Exists(plazaGroup)) PlazaGroup.Save(plazaGroup);

			#region Plaza SUTHISARN

			plaza = new Plaza()
			{
				PlazaId = "3103",
				PlazaNameEN = "SUTHISARN",
				PlazaNameTH = "สุทธิสาร",
				TSBId = item.TSBId,
				PlazaGroupId = plazaGroup.PlazaGroupId
			};
			if (!Plaza.Exists(plaza)) Plaza.Save(plaza);

			#region Lanes

			lane = new Lane()
			{
				LaneNo = 1,
				LaneId = "SS01",
				LaneType = "?",
				LaneAbbr = "SS01",
				TSBId = item.TSBId,
				PlazaGroupId = plazaGroup.PlazaGroupId,
				PlazaId = plaza.PlazaId
			};
			if (!Lane.Exists(lane)) Lane.Save(lane);
			lane = new Lane()
			{
				LaneNo = 2,
				LaneId = "SS02",
				LaneType = "?",
				LaneAbbr = "SS02",
				TSBId = item.TSBId,
				PlazaGroupId = plazaGroup.PlazaGroupId,
				PlazaId = plaza.PlazaId
			};
			if (!Lane.Exists(lane)) Lane.Save(lane);
			lane = new Lane()
			{
				LaneNo = 3,
				LaneId = "SS03",
				LaneType = "?",
				LaneAbbr = "SS03",
				TSBId = item.TSBId,
				PlazaGroupId = plazaGroup.PlazaGroupId,
				PlazaId = plaza.PlazaId
			};
			if (!Lane.Exists(lane)) Lane.Save(lane);


			#endregion

			#endregion

			#endregion

			#endregion

			#region LAD PRAO

			#region TSB

			item = new TSB();
			item.NetworkId = "31";
			item.TSBId = "313";
			item.TSBNameEN = "LAD PRAO";
			item.TSBNameTH = "ลาดพร้าว";
			item.Active = false;
			if (!TSB.Exists(item)) TSB.Save(item);

			#endregion

			#region PlazaGroup LAD PRAO INBOUND

			plazaGroup = new PlazaGroup()
			{
				PlazaGroupId = "LP-IN",
				PlazaGroupNameEN = "LAD PRAO INBOUND",
				PlazaGroupNameTH = "ลาดพร้าว ขาเข้า",
				Direction = "IN",
				TSBId = item.TSBId
			};
			if (!PlazaGroup.Exists(plazaGroup)) PlazaGroup.Save(plazaGroup);

			#region Plaza LAD PRAO INBOUND

			plaza = new Plaza()
			{
				PlazaId = "3104",
				PlazaNameEN = "LAD PRAO INBOUND",
				PlazaNameTH = "ลาดพร้าว ขาเข้า",
				TSBId = item.TSBId,
				PlazaGroupId = plazaGroup.PlazaGroupId
			};
			if (!Plaza.Exists(plaza)) Plaza.Save(plaza);

			#region Lanes

			lane = new Lane()
			{
				LaneNo = 1,
				LaneId = "LP01",
				LaneType = "?",
				LaneAbbr = "LP01",
				TSBId = item.TSBId,
				PlazaGroupId = plazaGroup.PlazaGroupId,
				PlazaId = plaza.PlazaId
			};
			if (!Lane.Exists(lane)) Lane.Save(lane);
			lane = new Lane()
			{
				LaneNo = 2,
				LaneId = "LP02",
				LaneType = "?",
				LaneAbbr = "LP02",
				TSBId = item.TSBId,
				PlazaGroupId = plazaGroup.PlazaGroupId,
				PlazaId = plaza.PlazaId
			};
			if (!Lane.Exists(lane)) Lane.Save(lane);
			lane = new Lane()
			{
				LaneNo = 3,
				LaneId = "LP03",
				LaneType = "?",
				LaneAbbr = "LP03",
				TSBId = item.TSBId,
				PlazaGroupId = plazaGroup.PlazaGroupId,
				PlazaId = plaza.PlazaId
			};
			if (!Lane.Exists(lane)) Lane.Save(lane);
			lane = new Lane()
			{
				LaneNo = 4,
				LaneId = "LP04",
				LaneType = "?",
				LaneAbbr = "LP04",
				TSBId = item.TSBId,
				PlazaGroupId = plazaGroup.PlazaGroupId,
				PlazaId = plaza.PlazaId
			};
			if (!Lane.Exists(lane)) Lane.Save(lane);

			#endregion

			#endregion

			#endregion

			#region PlazaGroup LAD PRAO OUTBOUND

			plazaGroup = new PlazaGroup()
			{
				PlazaGroupId = "LP-OUT",
				PlazaGroupNameEN = "LAD PRAO OUTBOUND",
				PlazaGroupNameTH = "ลาดพร้าว ขาออก",
				Direction = "OUT",
				TSBId = item.TSBId
			};
			if (!PlazaGroup.Exists(plazaGroup)) PlazaGroup.Save(plazaGroup);

			#region Plaza LAD PRAO OUTBOUND

			plaza = new Plaza()
			{
				PlazaId = "3105",
				PlazaNameEN = "LAD PRAO OUTBOUND",
				PlazaNameTH = "ลาดพร้าว ขาออก",
				TSBId = item.TSBId,
				PlazaGroupId = plazaGroup.PlazaGroupId
			};
			if (!Plaza.Exists(plaza)) Plaza.Save(plaza);

			#region Lanes

			lane = new Lane()
			{
				LaneNo = 21,
				LaneId = "LP21",
				LaneType = "?",
				LaneAbbr = "LP21",
				TSBId = item.TSBId,
				PlazaGroupId = plazaGroup.PlazaGroupId,
				PlazaId = plaza.PlazaId
			};
			if (!Lane.Exists(lane)) Lane.Save(lane);

			lane = new Lane()
			{
				LaneNo = 22,
				LaneId = "LP22",
				LaneType = "?",
				LaneAbbr = "LP22",
				TSBId = item.TSBId,
				PlazaGroupId = plazaGroup.PlazaGroupId,
				PlazaId = plaza.PlazaId
			};
			if (!Lane.Exists(lane)) Lane.Save(lane);

			lane = new Lane()
			{
				LaneNo = 23,
				LaneId = "LP23",
				LaneType = "?",
				LaneAbbr = "LP23",
				TSBId = item.TSBId,
				PlazaGroupId = plazaGroup.PlazaGroupId,
				PlazaId = plaza.PlazaId
			};
			if (!Lane.Exists(lane)) Lane.Save(lane);

			#endregion

			#endregion

			#endregion

			#endregion

			#region RATCHADA PHISEK

			#region TSB

			item = new TSB();
			item.NetworkId = "31";
			item.TSBId = "314";
			item.TSBNameEN = "RATCHADA PHISEK";
			item.TSBNameTH = "รัชดาภิเษก";
			item.Active = false;
			if (!TSB.Exists(item)) TSB.Save(item);

			#endregion

			#region PlazaGroup RATCHADA PHISEK

			plazaGroup = new PlazaGroup()
			{
				PlazaGroupId = "RP",
				PlazaGroupNameEN = "RATCHADA PHISEK",
				PlazaGroupNameTH = "รัชดาภิเษก",
				Direction = "?",
				TSBId = item.TSBId
			};
			if (!PlazaGroup.Exists(plazaGroup)) PlazaGroup.Save(plazaGroup);

			#region Plaza RATCHADA PHISEK 1

			plaza = new Plaza()
			{
				PlazaId = "3106",
				PlazaNameEN = "RATCHADA PHISEK 1",
				PlazaNameTH = "รัชดาภิเษก 1",
				TSBId = item.TSBId,
				PlazaGroupId = plazaGroup.PlazaGroupId
			};
			if (!Plaza.Exists(plaza)) Plaza.Save(plaza);

			#region Lanes (no data)

			#endregion

			#endregion

			#region Plaza RATCHADA PHISEK 2

			plaza = new Plaza()
			{
				PlazaId = "3107",
				PlazaNameEN = "RATCHADA PHISEK 2",
				PlazaNameTH = "รัชดาภิเษก 2",
				TSBId = item.TSBId,
				PlazaGroupId = plazaGroup.PlazaGroupId
			};
			if (!Plaza.Exists(plaza)) Plaza.Save(plaza);

			#region Lanes (no data)

			#endregion

			#endregion

			#endregion

			#endregion

			#region BANGKHEN

			#region TSB

			item = new TSB();
			item.NetworkId = "31";
			item.TSBId = "315";
			item.TSBNameEN = "BANGKHEN";
			item.TSBNameTH = "บางเขน";
			item.Active = false;
			if (!TSB.Exists(item)) TSB.Save(item);

			#endregion

			#region PlazaGroup BANGKHEN

			plazaGroup = new PlazaGroup()
			{
				PlazaGroupId = "BK",
				PlazaGroupNameEN = "BANGKHEN",
				PlazaGroupNameTH = "บางเขน",
				Direction = "?",
				TSBId = item.TSBId
			};
			if (!PlazaGroup.Exists(plazaGroup)) PlazaGroup.Save(plazaGroup);

			#region Plaza BANGKHEN

			plaza = new Plaza()
			{
				PlazaId = "3108",
				PlazaNameEN = "BANGKHEN",
				PlazaNameTH = "บางเขน",
				TSBId = item.TSBId,
				PlazaGroupId = plazaGroup.PlazaGroupId
			};
			if (!Plaza.Exists(plaza)) Plaza.Save(plaza);

			#endregion

			#region Lanes (no data)

			#endregion

			#endregion

			#endregion

			#region CHANGEWATTANA

			#region TSB

			item = new TSB();
			item.NetworkId = "31";
			item.TSBId = "316";
			item.TSBNameEN = "CHANGEWATTANA";
			item.TSBNameTH = "แจ้งวัฒนะ";
			item.Active = false;
			if (!TSB.Exists(item)) TSB.Save(item);

			#endregion

			#region PlazaGroup CHANGEWATTANA

			plazaGroup = new PlazaGroup()
			{
				PlazaGroupId = "CW",
				PlazaGroupNameEN = "CHANGEWATTANA",
				PlazaGroupNameTH = "แจ้งวัฒนะ",
				Direction = "?",
				TSBId = item.TSBId
			};
			if (!PlazaGroup.Exists(plazaGroup)) PlazaGroup.Save(plazaGroup);

			#region Plaza CHANGEWATTANA 1

			plaza = new Plaza()
			{
				PlazaId = "3109",
				PlazaNameEN = "CHANGEWATTANA 1",
				PlazaNameTH = "แจ้งวัฒนะ 1",
				TSBId = item.TSBId,
				PlazaGroupId = plazaGroup.PlazaGroupId
			};
			if (!Plaza.Exists(plaza)) Plaza.Save(plaza);

			#region Lanes (no data)

			#endregion

			#endregion

			#region Plaza CHANGEWATTANA 2

			plaza = new Plaza()
			{
				PlazaId = "3110",
				PlazaNameEN = "CHANGEWATTANA 2",
				PlazaNameTH = "แจ้งวัฒนะ 2",
				TSBId = item.TSBId,
				PlazaGroupId = plazaGroup.PlazaGroupId
			};
			if (!Plaza.Exists(plaza)) Plaza.Save(plaza);

			#region Lanes (no data)

			#endregion

			#endregion

			#endregion

			#endregion

			#region LAKSI

			#region TSB

			item = new TSB();
			item.NetworkId = "31";
			item.TSBId = "317";
			item.TSBNameEN = "LAKSI";
			item.TSBNameTH = "หลักสี่";
			item.Active = false;
			if (!TSB.Exists(item)) TSB.Save(item);

			#endregion

			#region PlazaGroup LAKSI INBOUND

			plazaGroup = new PlazaGroup()
			{
				PlazaGroupId = "LS-IN",
				PlazaGroupNameEN = "LAKSI INBOUND",
				PlazaGroupNameTH = "หลักสี่ ขาเข้า",
				Direction = "IN",
				TSBId = item.TSBId
			};
			if (!PlazaGroup.Exists(plazaGroup)) PlazaGroup.Save(plazaGroup);

			#region Plaza LAKSI INBOUND

			plaza = new Plaza()
			{
				PlazaId = "3111",
				PlazaNameEN = "LAKSI INBOUND",
				PlazaNameTH = "หลักสี่ ขาเข้า",
				TSBId = item.TSBId,
				PlazaGroupId = plazaGroup.PlazaGroupId
			};
			if (!Plaza.Exists(plaza)) Plaza.Save(plaza);

			#region Lanes (no data)

			#endregion

			#endregion

			#endregion

			#region PlazaGroup LAKSI OUTBOUND

			plazaGroup = new PlazaGroup()
			{
				PlazaGroupId = "LS-OUT",
				PlazaGroupNameEN = "LAKSI OUTBOUND",
				PlazaGroupNameTH = "หลักสี่ ขาออก",
				Direction = "OUT",
				TSBId = item.TSBId
			};
			if (!PlazaGroup.Exists(plazaGroup)) PlazaGroup.Save(plazaGroup);

			#region Plaza LAKSI OUTBOUND

			plaza = new Plaza()
			{
				PlazaId = "3112",
				PlazaNameEN = "LAKSI OUTBOUND",
				PlazaNameTH = "หลักสี่ ขาออก",
				TSBId = item.TSBId,
				PlazaGroupId = plazaGroup.PlazaGroupId
			};
			if (!Plaza.Exists(plaza)) Plaza.Save(plaza);

			#region Lanes (no data)

			#endregion

			#endregion

			#endregion

			#endregion

			#region DON MUANG

			#region TSB

			item = new TSB();
			item.NetworkId = "31";
			item.TSBId = "318";
			item.TSBNameEN = "DON MUANG";
			item.TSBNameTH = "ดอนเมือง";
			item.Active = false;
			if (!TSB.Exists(item)) TSB.Save(item);

			#endregion

			#region PlazaGroup DON MUANG

			plazaGroup = new PlazaGroup()
			{
				PlazaGroupId = "DM",
				PlazaGroupNameEN = "DON MUANG",
				PlazaGroupNameTH = "ดอนเมือง",
				Direction = "?",
				TSBId = item.TSBId
			};
			if (!PlazaGroup.Exists(plazaGroup)) PlazaGroup.Save(plazaGroup);

			#region Plaza DON MUANG 1

			plaza = new Plaza()
			{
				PlazaId = "3113",
				PlazaNameEN = "DON MUANG 1",
				PlazaNameTH = "ดอนเมือง 1",
				TSBId = item.TSBId,
				PlazaGroupId = plazaGroup.PlazaGroupId
			};
			if (!Plaza.Exists(plaza)) Plaza.Save(plaza);

			#region Lanes (no data)

			#endregion

			#endregion

			#region Plaza DON MUANG 2

			plaza = new Plaza()
			{
				PlazaId = "3114",
				PlazaNameEN = "DON MUANG 2",
				PlazaNameTH = "ดอนเมือง 2",
				TSBId = item.TSBId,
				PlazaGroupId = plazaGroup.PlazaGroupId
			};
			if (!Plaza.Exists(plaza)) Plaza.Save(plaza);

			#region Lanes (no data)

			#endregion

			#endregion

			#endregion

			#endregion

			#region ANUSORN SATHAN

			#region TSB

			item = new TSB();
			item.NetworkId = "31";
			item.TSBId = "319";
			item.TSBNameEN = "ANUSORN SATHAN";
			item.TSBNameTH = "อนุสรน์สถาน";
			item.Active = false;
			if (!TSB.Exists(item)) TSB.Save(item);

			#endregion

			#region PlazaGroup ANUSORN SATHAN

			plazaGroup = new PlazaGroup()
			{
				PlazaGroupId = "AS",
				PlazaGroupNameEN = "ANUSORN SATHAN",
				PlazaGroupNameTH = "อนุสรน์สถาน",
				Direction = "?",
				TSBId = item.TSBId
			};
			if (!PlazaGroup.Exists(plazaGroup)) PlazaGroup.Save(plazaGroup);

			#region Plaza ANUSORN SATHAN 1

			plaza = new Plaza()
			{
				PlazaId = "3115",
				PlazaNameEN = "ANUSORN SATHAN 1",
				PlazaNameTH = "อนุสรน์สถาน 1",
				TSBId = item.TSBId,
				PlazaGroupId = plazaGroup.PlazaGroupId
			};
			if (!Plaza.Exists(plaza)) Plaza.Save(plaza);

			#region Lanes (no data)

			#endregion

			#endregion

			#region Plaza ANUSORN SATHAN 2

			plaza = new Plaza()
			{
				PlazaId = "3116",
				PlazaNameEN = "ANUSORN SATHAN 2",
				PlazaNameTH = "อนุสรน์สถาน 2",
				TSBId = item.TSBId,
				PlazaGroupId = plazaGroup.PlazaGroupId
			};
			if (!Plaza.Exists(plaza)) Plaza.Save(plaza);

			#region Lanes (no data)

			#endregion

			#endregion

			#endregion

			#endregion
		}

		private void InitShifts()
		{
			if (null == Db) return;

			if (Db.Table<Shift>().Count() > 0) return; // already exists.

			Shift item;
			item = new Shift()
			{
				ShiftId = 1,
				ShiftNameEN = "Morning",
				ShiftNameTH = "เช้า"
			};
			if (!Shift.Exists(item)) Shift.Save(item);
			item = new Shift()
			{
				ShiftId = 2,
				ShiftNameEN = "Afternoon",
				ShiftNameTH = "บ่าย"
			};
			if (!Shift.Exists(item)) Shift.Save(item);
			item = new Shift()
			{
				ShiftId = 3,
				ShiftNameEN = "Midnight",
				ShiftNameTH = "ดึก"
			};
			if (!Shift.Exists(item)) Shift.Save(item);
		}

		private void InitRoleAndUsers()
		{
			if (null == Db) return;
			Role item;
			User user;

			#region ADMINS
			
			item = new Role()
			{
				RoleId = "ADMINS",
				RoleNameEN = "Administrator",
				RoleNameTH = "ผู้ดูแลระบบ",
				GroupId = 1
			};
			if (!Role.Exists(item)) Role.Save(item);

			user = new User()
			{
				UserId = "99901",
				FullNameEN = "Admin 1",
				FullNameTH = "Admin 1",
				UserName = "admin1",
				Password = "1234",
				CardId = "",
				Status = 1,
				RoleId = item.RoleId
			};
			if (!User.Exists(user)) User.Save(user);

			#endregion

			#region ACCOUNT

			item = new Role()
			{
				RoleId = "ACCOUNT",
				RoleNameEN = "Account",
				RoleNameTH = "ฝ่ายบัญขี",
				GroupId = 12
			};
			if (!Role.Exists(item)) Role.Save(item);

			user = new User()
			{
				UserId = "85020",
				FullNameEN = "audit1",
				FullNameTH = "audit1",
				UserName = "audit1",
				Password = "1234",
				CardId = "",
				Status = 1,
				RoleId = item.RoleId
			};
			if (!User.Exists(user)) User.Save(user);

			user = new User()
			{
				UserId = "65401",
				FullNameEN = "นาย สมชาย ตุยเอียว",
				FullNameTH = "นาย สมชาย ตุยเอียว",
				UserName = "audit2",
				Password = "1234",
				CardId = "",
				Status = 1,
				RoleId = item.RoleId
			};
			if (!User.Exists(user)) User.Save(user);

			#endregion

			#region CTC

			item = new Role()
			{
				RoleId = "CTC",
				RoleNameEN = "Chief Toll Collector",
				RoleNameTH = "หัวหน้ากะ",
				GroupId = 4
			};
			if (!Role.Exists(item)) Role.Save(item);

			user = new User()
			{
				UserId = "13566",
				FullNameEN = "นาย ผจญ สุดศิริ",
				FullNameTH = "นาย ผจญ สุดศิริ",
				UserName = "sup1",
				Password = "1234",
				CardId = "",
				Status = 1,
				RoleId = item.RoleId
			};
			if (!User.Exists(user)) User.Save(user);

			user = new User()
			{
				UserId = "26855",
				FullNameEN = "นวย วิรชัย ขำหิรัญ",
				FullNameTH = "นวย วิรชัย ขำหิรัญ",
				UserName = "sup2",
				Password = "1234",
				CardId = "",
				Status = 1,
				RoleId = item.RoleId
			};
			if (!User.Exists(user)) User.Save(user);

			user = new User()
			{
				UserId = "30242",
				FullNameEN = "นาย บุญส่ง บุญปลื้ม",
				FullNameTH = "นาย บุญส่ง บุญปลื้ม",
				UserName = "sup3",
				Password = "1234",
				CardId = "",
				Status = 1,
				RoleId = item.RoleId
			};
			if (!User.Exists(user)) User.Save(user);

			user = new User()
			{
				UserId = "76333",
				FullNameEN = "นาย สมบูรณ์ สบายดี",
				FullNameTH = "นาย สมบูรณ์ สบายดี",
				UserName = "sup4",
				Password = "1234",
				CardId = "",
				Status = 1,
				RoleId = item.RoleId
			};
			if (!User.Exists(user)) User.Save(user);

			#endregion

			#region TC

			item = new Role()
			{
				RoleId = "TC",
				RoleNameEN = "Toll Collector",
				RoleNameTH = "พนักงาน",
				GroupId = 2
			};
			if (!Role.Exists(item)) Role.Save(item);

			user = new User()
			{
				UserId = "14211",
				FullNameEN = "นาย ภักดี อมรรุ่งโรจน์",
				FullNameTH = "นาย ภักดี อมรรุ่งโรจน์",
				UserName = "user1",
				Password = "1234",
				CardId = "",
				Status = 1,
				RoleId = item.RoleId
			};
			if (!User.Exists(user)) User.Save(user);

			user = new User()
			{
				UserId = "14124",
				FullNameEN = "นางสาว แก้วใส ฟ้ารุ่งโรจณ์",
				FullNameTH = "นางสาว แก้วใส ฟ้ารุ่งโรจณ์",
				UserName = "user2",
				Password = "1234",
				CardId = "",
				Status = 1,
				RoleId = item.RoleId
			};
			if (!User.Exists(user)) User.Save(user);

			user = new User()
			{
				UserId = "14055",
				FullNameEN = "นางวิภา สวัสดิวัฒน์",
				FullNameTH = "นางวิภา สวัสดิวัฒน์",
				UserName = "user3",
				Password = "1234",
				CardId = "",
				Status = 1,
				RoleId = item.RoleId
			};
			if (!User.Exists(user)) User.Save(user);

			user = new User()
			{
				UserId = "14321",
				FullNameEN = "นาย สุเทพ เหมัน",
				FullNameTH = "นาย สุเทพ เหมัน",
				UserName = "user4",
				Password = "1234",
				CardId = "",
				Status = 1,
				RoleId = item.RoleId
			};
			if (!User.Exists(user)) User.Save(user);

			user = new User()
			{
				UserId = "14477",
				FullNameEN = "นาย ศิริลักษณ์ วงษาหาร",
				FullNameTH = "นาย ศิริลักษณ์ วงษาหาร",
				UserName = "user5",
				Password = "1234",
				CardId = "",
				Status = 1,
				RoleId = item.RoleId
			};
			if (!User.Exists(user)) User.Save(user);

			user = new User()
			{
				UserId = "14566",
				FullNameEN = "นางสาว สุณิสา อีนูน",
				FullNameTH = "นางสาว สุณิสา อีนูน",
				UserName = "user6",
				Password = "1234",
				CardId = "",
				Status = 1,
				RoleId = item.RoleId
			};
			if (!User.Exists(user)) User.Save(user);

			user = new User()
			{
				UserId = "15097",
				FullNameEN = "นาง วาสนา ชาญวิเศษ",
				FullNameTH = "นาง วาสนา ชาญวิเศษ",
				UserName = "user7",
				Password = "1234",
				CardId = "",
				Status = 1,
				RoleId = item.RoleId
			};
			if (!User.Exists(user)) User.Save(user);

			#endregion
		}

		private void InitPayments()
		{
			if (null == Db) return;
			Payment item;
			// for send to Data Center.
			item = new Payment()
			{
				PaymentId = "EMV",
				PaymentNameEN = "EMV",
				PaymentNameTH = "อีเอ็มวี"
			};
			if (!Payment.Exists(item)) Payment.Save(item);
			item = new Payment()
			{
				PaymentId = "QRCODE",
				PaymentNameEN = "QR Code",
				PaymentNameTH = "คิวอาร์ โค้ด"
			};
			if (!Payment.Exists(item)) Payment.Save(item);
		}

		private void InitConfigs()
		{
			if (null == Db) return;
			Config item;
			// for send to Data Center.
			item = new Config() { Key = Configs.DC.network, Value = "4" };
			if (!Config.Exists(item)) Config.Save(item);
			item = new Config() { Key = Configs.DC.tsb, Value = "97" };
			if (!Config.Exists(item)) Config.Save(item);
			item = new Config() { Key = Configs.DC.terminal, Value = "49701" };
			if (!Config.Exists(item)) Config.Save(item);
			// for application
			/*
			item = new Config() { Key = Configs.App.TSBId, Value = "" };
			if (!Config.Exists(item)) Config.Save(item);
			item = new Config() { Key = Configs.App.PlazaId, Value = "" };
			if (!Config.Exists(item)) Config.Save(item);
			item = new Config() { Key = Configs.App.SupervisorId, Value = "" };
			if (!Config.Exists(item)) Config.Save(item);
			item = new Config() { Key = Configs.App.ShiftId, Value = "" };
			if (!Config.Exists(item)) Config.Save(item);
			*/
		}

		private void InitViews()
		{
			if (null == Db) return;

			string prefix;

			// Infrastructures - Embeded resource used . instead / to access sub contents.
			prefix = @"Infrastructures";
			InitView("PlazaGroupView", prefix);
			InitView("PlazaView", prefix);
			InitView("LaneView", prefix);

			// Users - Embeded resource used . instead / to access sub contents.
			prefix = @"Users";
			InitView("UserView", prefix);

			// Shifts - Embeded resource used . instead / to access sub contents.
			prefix = @"Shifts";
			InitView("TSBShiftView", prefix);
			InitView("UserShiftView", prefix);
			InitView("UserShiftRevenueView", prefix);

			// LaneActivities - Embeded resource used . instead / to access sub contents.
			prefix = @"LaneActivities";
			InitView("LaneAttendanceView", prefix);
			InitView("LanePaymentView", prefix);

			// Revenues - Embeded resource used . instead / to access sub contents.
			prefix = @"Revenues";
			InitView("RevenueEntryView", prefix);

			// Credits - Embeded resource used . instead / to access sub contents.
			prefix = @"Credits";
			InitView("TSBCreditSummarryView", prefix);
			InitView("UserCreditBorrowSummaryView", prefix);
			InitView("UserCreditReturnSummaryView", prefix);
			InitView("UserCreditSummaryView", prefix);

			// Coupons - Embeded resource used . instead / to access sub contents.
			prefix = @"Coupons";
			InitView("TSBCouponTransactionView", prefix);
			InitView("TSBCouponBalanceView", prefix);
			InitView("TSBCouponSummarryView", prefix);
			InitView("UserCoupon35SummaryView", prefix);
			InitView("UserCoupon80SummaryView", prefix);
			InitView("UserCouponSummaryView", prefix);

			// Exchanges - Embeded resource used . instead / to access sub contents.
			prefix = @"Exchanges";
			InitView("TSBExchangeTransactionView", prefix);
		}

		class ViewInfo
		{
			public string Name { get; set; }
		}

		private void InitView(string viewName, string resourcePrefix = "")
		{
			if (null == Db) return;

			var histRet = ViewHistory.GetWithChildren(viewName, false);
			var hist = (null != histRet && !histRet.errors.hasError) ? histRet.data : null;

			if (null == hist)
				return;

			var info = Db.GetTableInfo(viewName);

			string checkViewCmd = "SELECT Name FROM sqlite_master WHERE Type = 'view' AND Name = ?";
			var rets = Db.Query<ViewInfo>(checkViewCmd, viewName);
			bool exists = (null != rets && rets.Count > 0);

			//bool exists = (null != info) ? info.Count > 0 : false;

			if (!exists || null == hist || hist.VersionId != HistoryVersion)
			{
				string script = string.Empty;
				MethodBase med = MethodBase.GetCurrentMethod();
				try
				{
					string dropCmd = string.Empty;
					dropCmd += "DROP VIEW IF EXISTS " + viewName;
					Db.BeginTransaction();
					try { Db.Execute(dropCmd); }
					catch (Exception dropEx)
					{
						//Console.WriteLine(dropEx);
						med.Err(dropEx);
						med.Err("Drop Failed:" + Environment.NewLine + viewName);
						Db.Rollback();
					}
					finally { Db.Commit(); }

					// Recheck
					/*
					rets = Db.Query<ViewInfo>(checkViewCmd, viewName);
					exists = (null != rets && rets.Count > 0);
					if (exists)
					{
						Console.WriteLine("Drop View Failed.");
					}
					*/

					string resourceName = viewName + ".sql";
					// Note: 
					// -----------------------------------------------------------
					// Embeded resource used . instead / to access sub contents.
					// -----------------------------------------------------------
					string embededResourceName;
					if (!string.IsNullOrWhiteSpace(resourcePrefix))
					{
						// Has prefix
						if (!resourcePrefix.Trim().EndsWith("."))
						{
							// Not end with . so append . and concat full name.
							embededResourceName = @"DMT.Views.Scripts." + resourcePrefix + "." + resourceName;
						}
						else
						{
							// Already end with . so concat to full name.
							embededResourceName = @"DMT.Views.Scripts." + resourcePrefix + resourceName;
						}
					}
					else
					{
						// No prefix.
						embededResourceName = @"DMT.Views.Scripts." + resourceName;
					}

					script = SqliteScriptManager.GetScript(embededResourceName);

					if (!string.IsNullOrEmpty(script))
					{
						var ret = Db.Execute(script);

						Console.WriteLine("Returns: {0}", ret);

						if (null == hist) hist = new ViewHistory();
						hist.ViewName = viewName;
						hist.VersionId = HistoryVersion;
						ViewHistory.Save(hist);
					}
					else
					{
						Console.WriteLine("{0} Has Empty Scripts.", viewName);
					}
				}
				catch (Exception ex)
				{
					//Console.WriteLine(ex);
					med.Err(ex);
					med.Err("Script:" + Environment.NewLine + script);
					Console.WriteLine(script);
				}
			}
		}

		#endregion

		#region Public Methods (Start/Shutdown)

		/// <summary>
		/// Start.
		/// </summary>
		public void Start()
		{
			MethodBase med = MethodBase.GetCurrentMethod();
			if (null == Db)
			{
				lock (typeof(LocalDbServer))
				{
					try
					{
						// ---------------------------------------------------------------
						// NOTE:
						// ---------------------------------------------------------------
						// If Exception due to version mismatch here
						// Please rebuild only this project and try again
						// VS Should Solve mismatch version properly (maybe)
						// See: https://nickcraver.com/blog/2020/02/11/binding-redirects/
						// for more information.
						// ---------------------------------------------------------------

						string path = Path.Combine(LocalFolder, FileName);
						Db = new SQLiteConnection(path,
							SQLiteOpenFlags.Create |
							SQLiteOpenFlags.SharedCache |
							SQLiteOpenFlags.ReadWrite |
							SQLiteOpenFlags.FullMutex,
							storeDateTimeAsTicks: false);
						Db.BusyTimeout = new TimeSpan(0, 0, 5); // set busy timeout.
					}
					catch (Exception ex)
					{
						med.Err(ex);
						Db = null;

						OnConectError.Call(this, EventArgs.Empty);
					}
					if (null != Db)
					{
						// Set Default connection 
						// (be careful to make sure that we only has single database
						// for all domain otherwise call static method with user connnection
						// in each domain class instead omit connection version).
						NTable.Default = Db;
						NQuery.Default = Db;

						InitTables();

						OnConnected.Call(this, EventArgs.Empty);
					}
				}
			}
		}
		/// <summary>
		/// Shutdown.
		/// </summary>
		public void Shutdown()
		{
			if (null != Db)
			{
				Db.Dispose();
			}
			Db = null;
			OnDisconnected.Call(this, EventArgs.Empty);
		}

		#endregion

		#region Public Methods (Event raiser)

		public void ChangeShift()
		{
			OnChangeShift.Call(this, EventArgs.Empty);

		}

		public void ActiveTSBChanged()
		{
			OnActiveTSBChanged.Call(this, EventArgs.Empty);
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets database file name.
		/// </summary>
		public string FileName { get; set; }
		/// <summary>
		/// Gets SQLite Connection.
		/// </summary>
		public SQLiteConnection Db { get; private set; }

		#endregion

		#region Public Events

		public event System.EventHandler OnConnected;
		public event System.EventHandler OnDisconnected;
		public event System.EventHandler OnConectError;

		public event System.EventHandler OnChangeShift;
		public event System.EventHandler OnActiveTSBChanged;

		#endregion
	}

	#endregion
}
