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
    /// Interaction logic for AddScore.xaml
    /// </summary>
    public partial class AddScoreWhichTeam : UserControl
    {
        private static String choosenTeam;

        public AddScoreWhichTeam()
        {
            InitializeComponent();
            DataContext = GameController.GetInstance().GetTeamNames();
        }

        private void Choose_Team_Click(object sender, RoutedEventArgs e)
        {
            choosenTeam = (String)((Button)sender).Tag;
            MainWindow.SetScreen("AddScore");
        }
        public static String Get_Team_Name()
        {
            return choosenTeam;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.searchTerm.Focus();
        }

        private void SearchTerm_TextChanged(object sender, TextChangedEventArgs e)
        {
            String searchText = ((TextBox)sender).Text;
            DataContext = GameController.GetInstance().search(searchText);
        }
    }
}
