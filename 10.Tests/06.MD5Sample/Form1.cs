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

        private void cmdDecrypt_Click(object sender, EventArgs e)
        {
            string md5 = txtMD5.Text.Trim();
            if (string.IsNullOrEmpty(md5))
            {
                txtMD5.Focus();
            }

            if (rbASCII.Checked)
            {
                txtOut2.Text = Utils.MD5.ASCII.Decrypt(md5);
            }
            else if (rbUTF8v1.Checked)
            {
                txtOut2.Text = Utils.MD5.UTF8.Decrypt(md5);
            }
            else if (rbUTF8v2.Checked)
            {
                txtOut2.Text = Utils.MD5.UTF8v2.Decrypt(md5);
            }
        }

        #endregion

        #region TextBox Handlers

        private void txtOri_Enter(object sender, EventArgs e)
        {
            txtOri.SelectAll();
        }

        private void txtMD5_Enter(object sender, EventArgs e)
        {
            txtMD5.SelectAll();
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

                    return ret;
                }
                /// <summary>
                /// Decrypt MD5 string to original.
                /// </summary>
                /// <param name="value">The encrypt string.</param>
                /// <returns>Returns original string.</returns>
                public static string Decrypt(string value)
                {
                    string ret = string.Empty;

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
                /// <summary>
                /// Decrypt MD5 string to original.
                /// </summary>
                /// <param name="value">The encrypt string.</param>
                /// <returns>Returns original string.</returns>
                public static string Decrypt(string value)
                {
                    string ret = string.Empty;

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
                        /*
                        ret = BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(value)))
                            .Replace("-", string.Empty).ToLower();
                        */
                        ret = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(value)))
                            .Replace("-", string.Empty).ToLower();
                    }

                    return ret;
                }
                /// <summary>
                /// Decrypt MD5 string to original.
                /// </summary>
                /// <param name="value">The encrypt string.</param>
                /// <returns>Returns original string.</returns>
                public static string Decrypt(string value)
                {
                    string ret = string.Empty;

                    return ret;
                }
            }

            #endregion
        }

        #endregion
    }
}
