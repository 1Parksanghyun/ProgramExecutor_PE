using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace mainWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {

        private string DataPath = @"C:\Users\User\test_c_c++\NETprogram\ProjectFile\Data";
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                using (var sr = new System.IO.StreamReader(DataPath + @"\NameData.txt"))
                {
                    while (sr.Peek() != -1)
                    {
                        NamePlace.Items.Add(sr.ReadLine());
                    }
                }

                using (var sr = new System.IO.StreamReader(DataPath + @"\PathData.txt"))
                {
                    while (sr.Peek() != -1)
                    {
                        PathPlace.Items.Add(sr.ReadLine());
                    }
                }
            }
            catch (System.Exception err)
            {
                System.Windows.MessageBox.Show(err.ToString());
                throw;
            }
        }


        private void ExeButton(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.ProcessStartInfo op = new System.Diagnostics.ProcessStartInfo();
            System.Diagnostics.Process ep = new System.Diagnostics.Process();

            op.FileName = "cmd.exe";
            op.CreateNoWindow = false;
            op.UseShellExecute = false;
            op.RedirectStandardInput = true;
            op.RedirectStandardOutput = true;
            op.RedirectStandardError = true;

            ep.StartInfo = op;
            ep.Start();

            try
            {
                int INdex = NamePlace.SelectedIndex;
                ep.StandardInput.WriteLine(@"explorer " + PathPlace.Items[INdex].ToString());
                ep.WaitForExit(1500);

            }
            catch (System.Exception)
            {

                throw;
            }
            ep.Kill();
        }
        private void SaveButton(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var sr = new System.IO.StreamWriter(DataPath + @"\NameData.txt"))
                {
                    for (int x = 0; x < NamePlace.Items.Count; x++)
                    {
                        sr.WriteLine(NamePlace.Items[x].ToString());
                    }
                }

                using (var sr = new System.IO.StreamWriter(DataPath + @"\PathData.txt"))
                {
                    for (int x = 0; x < PathPlace.Items.Count; x++)
                    {
                        sr.WriteLine(PathPlace.Items[x].ToString());
                    }
                }
                System.Windows.MessageBox.Show("저장 성공!");
            }
            catch (System.Exception err)
            {
                System.Windows.MessageBox.Show(err.ToString());
                throw;
            }
        }

        private void AddButton(object sender, RoutedEventArgs e)
        {
            editWindow.EditWindow EW = new editWindow.EditWindow(this);
            EW.ShowDialog();
        }
        private void DelButton(object sender, RoutedEventArgs e)
        {
            int NowIndex = NamePlace.SelectedIndex;
            NamePlace.Items.RemoveAt(NowIndex);
            PathPlace.Items.RemoveAt(NowIndex);
        }
    }
}
