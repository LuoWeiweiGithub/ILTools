using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Mono.CSharp;

namespace Animaonline.ILTools.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            SetDescription("Press F5 to compile");
            sourceEditor.Text = "System.Console.WriteLine(\"Hello, World\");";
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                if (_compilationTask != null && _compilationTask.Status == TaskStatus.Running)
                    return;

                if (!string.IsNullOrEmpty(sourceEditor.Text))
                    CompileAndGetIL(sourceEditor.Text);
            }
        }

        private Evaluator _compiler;

        private Evaluator Compiler
        {
            get
            {
                if (_compiler == null)
                {
                    var compilerSettings = new CompilerSettings();
                    var compilerContext = new CompilerContext(compilerSettings, new ConsoleReportPrinter());

                    _compiler = new Evaluator(compilerContext);
                }

                return _compiler;
            }
        }

        private Task _compilationTask;

        private void CompileAndGetIL(string sourceCode)
        {
            SetDescription("Compiling code...");

            _compilationTask = Task.Factory.StartNew(() =>
            {
                try
                {
                    var compiledMethod = Compiler.Compile(sourceCode);

                    var methodIL = ILTools.GetMethodIL(compiledMethod.Method);

                    ilList.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        ilList.Items.Clear();
                        foreach (var ilInstruction in methodIL.Instructions)
                            ilList.Items.Add(ilInstruction);

                        SetDescription("Press F5 to compile");
                    }), null);
                }
                catch (Exception ex)
                {
                    SetDescription("Press F5 to compile");
                    MessageBox.Show("Could not compile.\r\n" + ex, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }

        private void SetDescription(string description)
        {
            descriptionLabel.Dispatcher.BeginInvoke(new Action(() =>
            {
                descriptionLabel.Content = description;
            }), null);
        }

        private void ilList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ilList.SelectedItem is ILInstruction)
            {
                var selectedInstruction = (ILInstruction)ilList.SelectedItem;

                this.descriptionLabel.Content = OpCodeDescriber.Describe(selectedInstruction.OpCode);
            }
        }
    }
}
