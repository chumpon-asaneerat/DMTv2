#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using NLib;
using NLib.Design;
using NLib.Reflection;

using SQLite;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensions.Extensions;
// required for JsonIgnore attribute.
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

#endregion

namespace DMT.Models
{
    #region Enun

    public enum CouponType : int
    {
        BHT35 = 35,
        BHT80 = 80
    }

    #endregion

    #region CouponFactor

    /// <summary>
    /// The CouponFactor Data Model class.
    /// </summary>
    [TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
    [Serializable]
    [JsonObject(MemberSerialization.OptOut)]
    //[Table("CouponFactor")]
    public class CouponFactor : NTable<CouponFactor>
    {
        #region Internal Variable

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CouponFactor() : base() { }

        #endregion

        #region Public Properties

        #endregion

        #region Static Methods

        #endregion
    }

    #endregion
}


namespace DMT
{
    using System.Text.RegularExpressions;

    public static class RangeParser
    {
        /// <summary>
        /// Parses a string representing a range of values into a sequence of integers.
        /// </summary>
        /// <param name="s">String to parse</param>
        /// <param name="minValue">Minimum value for open range specifier</param>
        /// <param name="maxValue">Maximum value for open range specifier</param>
        /// <returns>An enumerable sequence of integers</returns>
        /// <remarks>
        /// The range is specified as a string in the following forms or combination thereof:
        /// 5           single value
        /// 1,2,3,4,5   sequence of values
        /// 1-5         closed range
        /// -5          open range (converted to a sequence from minValue to 5)
        /// 1-          open range (converted to a sequence from 1 to maxValue)
        /// 
        /// The value delimiter can be either ',' or ';' and the range separator can be
        /// either '-' or ':'. Whitespace is permitted at any point in the input.
        /// 
        /// Any elements of the sequence that contain non-digit, non-whitespace, or non-separator
        /// characters or that are empty are ignored and not returned in the output sequence.
        /// </remarks>
        public static IEnumerable<int> ParseRange2(this string s, int minValue, int maxValue)
        {
            const string pattern = @"(?:^|(?<=[,;]))                  # match must begin with start of string or delim, where delim is , or ;
                                 \s*(                                 # leading whitespace
                                 (?<from>\d*)\s*(?:-|:)\s*(?<to>\d+)  # capture 'from <sep> to' or '<sep> to', where <sep> is - or :
                                 |                                    # or
                                 (?<from>\d+)\s*(?:-|:)\s*(?<to>\d*)  # capture 'from <sep> to' or 'from <sep>', where <sep> is - or :
                                 |                                    # or
                                 (?<num>\d+)                          # capture lone number
                                 )\s*                                 # trailing whitespace
                                 (?:(?=[,;\b])|$)                     # match must end with end of string or delim, where delim is , or ;";

            Regex regx = new Regex(pattern, RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

            foreach (Match m in regx.Matches(s))
            {
                Group gpNum = m.Groups["num"];
                if (gpNum.Success)
                {
                    yield return int.Parse(gpNum.Value);

                }
                else
                {
                    Group gpFrom = m.Groups["from"];
                    Group gpTo = m.Groups["to"];
                    if (gpFrom.Success || gpTo.Success)
                    {
                        int from = (gpFrom.Success && gpFrom.Value.Length > 0 ? int.Parse(gpFrom.Value) : minValue);
                        int to = (gpTo.Success && gpTo.Value.Length > 0 ? int.Parse(gpTo.Value) : maxValue);

                        for (int i = from; i <= to; i++)
                        {
                            yield return i;
                        }
                    }
                }
            }
        }
    }
}