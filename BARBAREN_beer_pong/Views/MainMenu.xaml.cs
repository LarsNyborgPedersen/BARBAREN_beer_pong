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
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        

        private void AddTeam_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SetScreen("AddTeam");
        }

        private void AddScore_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SetScreen("AddScoreWhichTeam");
        }

        private void Rank_Click(object sender, RoutedEventArgs e)
        {
            RankWindow rankWindow = new RankWindow();
            rankWindow.Show();
        }
    }
}
