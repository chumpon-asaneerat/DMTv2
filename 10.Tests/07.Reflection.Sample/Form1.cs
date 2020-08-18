﻿#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Reflection;

using NLib;
using NLib.Reflection;

#endregion

namespace Reflection.Sample
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

        private void button1_Click(object sender, EventArgs e)
        {
            // Check Is Subclass
            pgGeneral.SelectedObject = null;

            pgGeneral.SelectedObject = OutputResult.Create("Success");
        }

        #endregion
    }

    public class OutputResult
    {
        public string Message { get; set; }

        public static OutputResult Create(string msg)
        {
            var inst = new OutputResult();
            inst.Message = msg;
            return inst;
        }
    }

    public class ClassA
    {
        [PeropertyMapName("ID")]
        public int ID { get; set; }
    }

    public class ClassB
    {
        [PeropertyMapName("ID")]
        public virtual int ID { get; set; }
    }

    public class SubClassA : ClassA
    {

    }

    public class SubClassB : ClassB
    {
        [PeropertyMapName("ID")]
        public override int ID 
        {
            get { return base.ID;  }
            set { base.ID = value; }
        }
    }
}
