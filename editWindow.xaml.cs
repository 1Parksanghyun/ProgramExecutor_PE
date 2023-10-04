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

namespace editWindow
{
    public partial class EditWindow : Window
    {
        // 메인메뉴 Class
        mainWindow.MainWindow MW1;

        public EditWindow(mainWindow.MainWindow MW)
        {
            InitializeComponent();

            MW1 = MW;
            InputName.Text = "(이름없음)";
            InputPath.Text = "(경로없음)";
        }

        // 파일 찾기 버튼
        private void FindFileClick(object sender, RoutedEventArgs e)
        {
            using (OpenFileDialog NewFile = new OpenFileDialog())
            {
                NewFile.ShowDialog();
                InputPath.Text = NewFile.FileName.ToString();
            }
        }

        // 확인 버튼
        private void CheckButtonClick(object sender, RoutedEventArgs e)
        {
            MW1.NamePlace.Items.Add(InputName.Text);
            MW1.PathPlace.Items.Add(InputPath.Text);
            this.Close();
        }

        // 취소 버튼
        private void CancleButtonClick(object sender, RoutedEventArgs e)

        {
            this.Close();
        }

    }
}
