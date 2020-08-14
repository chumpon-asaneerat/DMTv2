#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using cypt = System.Security.Cryptography;

#endregion

namespace MD5Sample
{
    public partial class Form1 : Form
    {
        #region Constructor

        public Form1()
        {
            InitializeComponent();
        }

        #endregion

        #region Button Handlers

        private void cmdEncrypt_Click(object sender, EventArgs e)
        {
            string ori = txtOri.Text.Trim();
            if (string.IsNullOrEmpty(ori))
            {
                txtOri.Focus();
            }

            if (rbASCII.Checked)
            {
                txtOut1.Text = Utils.MD5.ASCII.Encrypt(ori);
            }
            else if (rbUTF8v1.Checked)
            {
                txtOut1.Text = Utils.MD5.UTF8.Encrypt(ori);
            }
            else if (rbUTF8v2.Checked)
            {
                txtOut1.Text = Utils.MD5.UTF8v2.Encrypt(ori);
            }
        }

        #endregion

        #region TextBox Handlers

        private void txtOri_Enter(object sender, EventArgs e)
        {
            txtOri.SelectAll();
        }

        #endregion
    }

    /// <summary>
    /// The DMT Utils class.
    /// </summary>
    public static class Utils
    {
        #region MD5

        /// <summary>
        /// The MD5 Utils class.
        /// </summary>
        public static class MD5
        {
            #region ASCII

            /// <summary>
            /// The MD5 ASCII Encoding Utils class.
            /// </summary>
            public static class ASCII
            {
                /// <summary>
                /// Encrypt original string to MD5 string.
                /// </summary>
                /// <param name="value">The original string.</param>
                /// <returns>Returns encrypt string.</returns>
                public static string Encrypt(string value)
                {
                    string ret = string.Empty;

                    byte[] asciiBytes = ASCIIEncoding.ASCII.GetBytes(value);
                    byte[] hashedBytes = cypt.MD5CryptoServiceProvider.Create().ComputeHash(asciiBytes);
                    ret = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                    /*
                    // Step 1, calculate MD5 hash from input
                    var md5 = cypt.MD5.Create();
                    byte[] inputBytes = Encoding.ASCII.GetBytes(value);
                    byte[] hashBytes = md5.ComputeHash(inputBytes);
                    // Step 2, convert byte array to hex string
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < hashBytes.Length; i++)
                    {
                        sb.Append(hashBytes[i].ToString("x2"));
                    }
                    ret = sb.ToString();
                    */

                    return ret;
                }
            }

            #endregion

            #region UTF8

            /// <summary>
            /// The MD5 UTF8 Encoding Utils class.
            /// </summary>
            public static class UTF8
            {
                /// <summary>
                /// Encrypt original string to MD5 string.
                /// </summary>
                /// <param name="value">The original string.</param>
                /// <returns>Returns encrypt string.</returns>
                public static string Encrypt(string value)
                {
                    string ret = string.Empty;

                    // byte array representation of that string
                    byte[] encodedPassword = new UTF8Encoding().GetBytes(value);
                    // need MD5 to calculate the hash
                    cypt.HashAlgorithm algo = (cypt.HashAlgorithm)cypt.CryptoConfig.CreateFromName("MD5");
                    byte[] hash = algo.ComputeHash(encodedPassword);
                    // string representation (similar to UNIX format)
                    ret = BitConverter.ToString(hash)
                       // without dashes
                       .Replace("-", string.Empty)
                       // make lowercase
                       .ToLower();

                    return ret;
                }
            }

            #endregion

            #region UTF8v2

            /// <summary>
            /// The MD5 UTF8v2 Encoding Utils class.
            /// </summary>
            public static class UTF8v2
            {
                /// <summary>
                /// Encrypt original string to MD5 string.
                /// </summary>
                /// <param name="value">The original string.</param>
                /// <returns>Returns encrypt string.</returns>
                public static string Encrypt(string value)
                {
                    string ret = string.Empty;
                    using (var md5 = cypt.MD5.Create())
                    {
                        byte[] inputBytes = Encoding.UTF8.GetBytes(value);
                        byte[] hashBytes = md5.ComputeHash(inputBytes);
                        ret = BitConverter.ToString(hashBytes)
                            .Replace("-", string.Empty).ToLower();
                    }
                    return ret;
                }
            }

            #endregion
        }

        #endregion
    }
}
