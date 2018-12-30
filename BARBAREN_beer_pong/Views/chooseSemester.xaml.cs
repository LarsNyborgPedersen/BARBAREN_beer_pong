using BARBAREN_beer_pong.ViewModels;
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
    /// Interaction logic for BlueView.xaml
    /// </summary>
    public partial class BlueView : UserControl 
    {
        public BlueView()
        {
            InitializeComponent();

            String[] periodNames = GameController.GetInstance().GetPeriodsByDate();



            this.DataContext = periodNames;
        }

        private void Create_Semester_Click(object sender, RoutedEventArgs e) => MainWindow.SetScreen("CreateSemester");

        private void Choose_Semester_Click(object sender, RoutedEventArgs e)
        {
            // setperiod
            string workingPeriod = (String) ((Button)sender).Tag;
            GameController.GetInstance().SetWorkingPeriod(workingPeriod);
            MainWindow.SetScreen("MainMenu");

        }
        
    }
}
