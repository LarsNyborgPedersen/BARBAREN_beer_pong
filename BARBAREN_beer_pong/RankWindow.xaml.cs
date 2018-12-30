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
using System.Windows.Shapes;

namespace BARBAREN_beer_pong
{
    /// <summary>
    /// Interaction logic for RankWindow.xaml
    /// </summary>
    public partial class RankWindow : Window
    {
        bool fullscreen = false;

        public RankWindow()
        {
            InitializeComponent();
            Set_Teams_Text_And_Image();
            DataContext = GameController.GetInstance().GetScores();
        }

        private void Set_Teams_Text_And_Image()
        {
            BARBAREN_beer_pong_lib.GameController.ScoreProbe[] scoreProbes = GameController.GetInstance().GetScores();
            
            foreach (BARBAREN_beer_pong_lib.GameController.ScoreProbe ScoreProbe in scoreProbes)
            {
                String teamExist = ScoreProbe.Teamname;
                if (GameController.GetInstance().TeamExists(teamExist))
                {
                    Grid grid = new Grid();

                    grid.RowDefinitions.Add(new RowDefinition());
                    grid.RowDefinitions.Add(new RowDefinition());
                    //ColumnDefinition c1 = new ColumnDefinition().Width.IsStar.
                    grid.ColumnDefinitions.Add(new ColumnDefinition());
                    grid.ColumnDefinitions.Add(new ColumnDefinition());
                    grid.ColumnDefinitions.Add(new ColumnDefinition());
                    grid.Width = System.Windows.SystemParameters.PrimaryScreenWidth / 2 - 5;

                    Image rank = new Image();
                    rank.Source = new BitmapImage(new Uri("C:/EksternHarddisk/Programmering/Visual Studio/BARBAREN_beer_pong/BARBAREN_beer_pong/Images/1.png"));
                    Grid.SetRow(rank, 0);
                    Grid.SetColumn(rank, 0);
                    Grid.SetRowSpan(rank, 2);
                    grid.Children.Add(rank);

                    TextBlock teamName = new TextBlock();
                    teamName.Text = ScoreProbe.Teamname;
                    teamName.FontSize = 50;
                    teamName.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                    Grid.SetRow(teamName, 0);
                    Grid.SetColumn(teamName, 1);
                    grid.Children.Add(teamName);

                    TextBlock score = new TextBlock();
                    score.Text = "Score: " + ScoreProbe.Score.ToString();
                    score.FontSize = 50;
                    score.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                    Grid.SetRow(score, 1);
                    Grid.SetColumn(score, 1);
                    grid.Children.Add(score);

                    Image teamImage = new Image();
                    String teamIcon = GameController.GetInstance().GetTeamIcon(ScoreProbe.Teamname);
                    teamImage.Source = new BitmapImage(new Uri(teamIcon));
                    Grid.SetRow(teamImage, 0);
                    Grid.SetColumn(teamImage, 2);
                    Grid.SetRowSpan(teamImage, 2);
                    grid.Children.Add(teamImage);

                    this.wrapPanel.MaxWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
                    this.wrapPanel.Children.Add(grid);
                }
            }



            if (scoreProbes.Length >= 1)
            {
                //this.team1Name.Text = scoreProbes[0].Teamname;
                //this.team1Score.Text = (String)scoreProbes[0].Score.ToString();
                //String picturePath = GameController.GetInstance().GetTeamIcon(scoreProbes[0].Teamname);
                //this.team1Picture.Source = new BitmapImage(new Uri(picturePath));
            } 
            else
            {
                //this.team1Name.Text = "Not enough teams. Sorry...";
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F11 && !fullscreen)
            {
                fullscreen = true;
                WindowStyle = WindowStyle.None;
                WindowState = WindowState.Maximized;
                ResizeMode = ResizeMode.NoResize;
            }
            else if (fullscreen)
            {
                fullscreen = false;
                WindowStyle = WindowStyle.ThreeDBorderWindow;
                WindowState = WindowState.Normal;
                ResizeMode = ResizeMode.CanResize;
            }

        }
    }
}
