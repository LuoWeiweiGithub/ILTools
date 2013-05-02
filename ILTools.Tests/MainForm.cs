using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mono.CSharp;

namespace ILTools.Tests
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void buttonCompile_Click(object sender, EventArgs e)
        {
            var compilerSettings = new CompilerSettings();
            var compilerContext = new CompilerContext(compilerSettings, new ConsoleReportPrinter());

            var ev = new Evaluator(compilerContext);
            var compiledMethod = ev.Compile(textBoxSource.Text);

            var methodIL = Animaonline.ILTools.ILTools.GetMethodIL(compiledMethod.Method);

            var ilStringBuilder = new StringBuilder();
            foreach (var ilInstruction in methodIL)
                ilStringBuilder.AppendLine(ilInstruction.ToString());

            richTextBoxIL.Text = ilStringBuilder.ToString();
        }
    }
}
