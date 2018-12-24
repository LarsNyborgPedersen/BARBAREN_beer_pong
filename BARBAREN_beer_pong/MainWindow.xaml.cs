using BARBAREN_beer_pong.ViewModels;
using BARBAREN_beer_pong.Views;
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

namespace BARBAREN_beer_pong
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow wn;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ChooseSemesterModel();
            wn = this;
        }

        private void BlueButton_Clicked(object sender, RoutedEventArgs e)
        {
            SetScreen("ChooseSemester");
        }

        private void RedButton_Clicked(object sender, RoutedEventArgs e)
        {
            SetScreen("RedView");
        }
        
        public static void SetScreen(String screenName)
        {
            if (screenName == "AddScore")
            {
                wn.DataContext = new AddScoreModel();
            }
            if (screenName == "AddTeam")
            {
                wn.DataContext = new AddTeamModel();
            }
            if (screenName == "ChooseSemester")
            {
                wn.DataContext = new ChooseSemesterModel();
            }
            if (screenName == "CreateSemester")
            {
                wn.DataContext = new CreateSemesterModel();
            }
            if (screenName == "MainMenuModel")
            {
                wn.DataContext = new MainMenuModel();
            }
            if (screenName == "Rank")
            {
                wn.DataContext = new RankModel();
            }
            if (screenName == "RedView")
            {
                wn.DataContext = new RedViewModel();
            }
        }
    }
}
