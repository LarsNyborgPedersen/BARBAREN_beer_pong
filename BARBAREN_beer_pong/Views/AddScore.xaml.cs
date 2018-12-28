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
    public partial class AddScore : UserControl
    {
        private static String teamName;

        public AddScore()
        {
            InitializeComponent();
            teamName = AddScoreWhichTeam.Get_Team_Name();
            this.teamNameSentence.Text = "Ændre score på team: " + teamName;
            gamesWon.Text = GameController.GetInstance().GetWins(teamName).ToString();
            gamesLost.Text = GameController.GetInstance().GetLost(teamName).ToString();

        }

        private void WonDecrement_Click(object sender, RoutedEventArgs e)
        {
            GameController.GetInstance().DecreseWins(teamName);
            gamesWon.Text = GameController.GetInstance().GetWins(teamName).ToString();
        }
        
        private void WonIncrement_Click(object sender, RoutedEventArgs e)
        {
            GameController.GetInstance().IncreseWins(teamName);
            gamesWon.Text = GameController.GetInstance().GetWins(teamName).ToString();
        }

        private void LostDecrement_Click(object sender, RoutedEventArgs e)
        {
            GameController.GetInstance().DecreseLost(teamName);
            gamesLost.Text = GameController.GetInstance().GetLost(teamName).ToString();
        }

        private void LostIncrement_Click(object sender, RoutedEventArgs e)
        {
            GameController.GetInstance().IncreseLost(teamName);
            gamesLost.Text = GameController.GetInstance().GetLost(teamName).ToString();
        }

        private void GamesWon_TextChanged(object sender, TextChangedEventArgs e)
        {
            int score;
            if (Int32.TryParse(((TextBox)sender).Text, out score))
            {
                GameController.GetInstance().SetWins(teamName, score);
            }
        }

        private void GamesLost_TextChanged(object sender, TextChangedEventArgs e)
        {
            int score;
            if (Int32.TryParse(((TextBox)sender).Text, out score))
            {
                GameController.GetInstance().SetLost(teamName, score);
            }
        }
    }
}
