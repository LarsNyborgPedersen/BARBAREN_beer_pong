using BARBAREN_beer_pong_lib;
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

namespace BARBAREN_beer_pong.Views
{
    /// <summary>
    /// Interaction logic for CreateSemester.xaml
    /// </summary>
    public partial class CreateSemester : UserControl
    {
        public CreateSemester()
        {
            InitializeComponent();
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String semesterName = this.semesterName.Text;
                if (GameController.GetInstance().PeriodExists(semesterName))
                {
                    MessageBox.Show("Period already exist, so no period was added");
                }
                GameController.GetInstance().AddPeriod(semesterName);
                
                MainWindow.SetScreen("ChooseSemester");
            }
            catch (System.ArgumentException)
            {
                MessageBox.Show("Please don't use any special characters!");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.semesterName.Focus();
        }
    }
}
