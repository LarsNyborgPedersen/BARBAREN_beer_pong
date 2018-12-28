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
        private static MainWindow wn;
        private static String currentWindow;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ChooseSemesterModel();
            wn = this;
        }

        private void Back_Button_Clicked(object sender, RoutedEventArgs e)
        {
            if (currentWindow == "AddScoreWhichTeam" || currentWindow == "AddTeam")
            {
                SetScreen("MainMenu");
            }
            else if(currentWindow == "AddScore")
            {
                SetScreen("AddScoreWhichTeam");
            }
            else if (currentWindow == "MainMenu" || currentWindow == "CreateSemester")
            {
                SetScreen("ChooseSemester");
            }
        }
        
        public static void SetScreen(String screenName)
        {
            if (screenName == "AddScore")
            {
                currentWindow = screenName;
                wn.DataContext = new AddScoreModel();
            }
            if (screenName == "AddScoreWhichTeam")
            {
                currentWindow = screenName;
                wn.DataContext = new AddScoreWhichTeamModel();
            }
            else if (screenName == "AddTeam")
            {
                currentWindow = screenName;
                wn.DataContext = new AddTeamModel();
            }
            else if (screenName == "ChooseSemester")
            {
                currentWindow = screenName;
                wn.DataContext = new ChooseSemesterModel();
            }
            else if (screenName == "CreateSemester")
            {
                currentWindow = screenName;
                wn.DataContext = new CreateSemesterModel();
            }
            else if (screenName == "MainMenu")
            {
                currentWindow = screenName;
                wn.DataContext = new MainMenuModel();
            }
        }
    }
}
