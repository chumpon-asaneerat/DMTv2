#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace DMT.Views
{
    public class SqlScriptManager
    {
        private static Assembly Current { get { return typeof(SqlScriptManager).Assembly; } }

        public static string GetScript(string resourceName)
        {
            string ret = string.Empty;
            if (!string.IsNullOrWhiteSpace(resourceName))
            {
                try
                {
                    using (Stream stream = Current.GetManifestResourceStream(resourceName))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            ret = reader.ReadToEnd();
                        }
                    }
                }
                catch (Exception)
                {
                    ret = string.Empty;
                }
            }
            return ret;
        }
    }
}
