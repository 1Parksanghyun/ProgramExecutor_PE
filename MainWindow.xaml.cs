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
        public class Data
        {
            public string name { get; set; }
            public string path { get; set; }
        }
        private string DataPath = @"C:\Users\User\test_c_c++\NETprogram\ProjectFile/ProgramExecutor_PE\Data";
        public MainWindow()
        {
            List<Data> listdata = new List<Data>();
            InitializeComponent();
            //데이터 불러오기
            try
            {
                /*using (var sr = new System.IO.StreamReader(DataPath + @"\NameData.txt"))
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
                }*/

                var NameSR = new System.IO.StreamReader(DataPath + @"\NameData.txt");
                var PathSR = new System.IO.StreamReader(DataPath + @"\PathData.txt");

                while (NameSR.Peek() != -1)
                {
                    listdata.Add(new Data() { name = NameSR.ReadLine(), path = PathSR.ReadLine() });
                }

                NameSR.Close();
                PathSR.Close();

                DataList.ItemsSource = listdata;
            }
            catch (System.Exception err)
            {
                System.Windows.MessageBox.Show(err.ToString());
                throw;
            }
        }

        // 실행버튼
        private void ExeButton(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.ProcessStartInfo op = new System.Diagnostics.ProcessStartInfo();
            System.Diagnostics.Process ep = new System.Diagnostics.Process();

            // 실행 설정
            op.FileName = "cmd.exe";
            op.CreateNoWindow = true;
            op.UseShellExecute = false;
            op.RedirectStandardInput = true;
            op.RedirectStandardOutput = true;
            op.RedirectStandardError = true;

            ep.StartInfo = op;
            ep.Start();

            try
            {
                // 실행
                int INdex = NamePlace.SelectedIndex;
                ep.StandardInput.WriteLine(@"explorer " + PathPlace.Items[INdex].ToString());
                ep.WaitForExit(1500);

            }
            catch (System.ArgumentOutOfRangeException) // 선택된 파일이 없을경우
            {
                System.Windows.MessageBox.Show("파일을 선택해주세요");
            }
            catch (System.Exception err)
            {
                System.Windows.MessageBox.Show(err.ToString());
                throw;
            }
            ep.Kill();
        }
        // 저장 버튼
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

        // 추가 버튼
        private void AddButton(object sender, RoutedEventArgs e)
        {
            editWindow.EditWindow EW = new editWindow.EditWindow(this);
            EW.ShowDialog();
        }

        // 삭제 버튼
        private void DelButton(object sender, RoutedEventArgs e)
        {
            int NowIndex = NamePlace.SelectedIndex;
            NamePlace.Items.RemoveAt(NowIndex);
            PathPlace.Items.RemoveAt(NowIndex);
        }
    }
}
