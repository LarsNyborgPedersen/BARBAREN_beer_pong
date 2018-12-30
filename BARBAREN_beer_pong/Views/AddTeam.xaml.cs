using BARBAREN_beer_pong_lib;
using Microsoft.Win32;
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
    /// Interaction logic for AddTeam.xaml
    /// </summary>
    public partial class AddTeam : UserControl
    {
        private static String picturePath;

        public AddTeam()
        {
            InitializeComponent();
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            String teamName = this.teamName.Text;
            String member1 = this.member1.Text;
            String member2 = this.member2.Text;
            String member3 = this.member3.Text;

            GameController.GetInstance().AddTeam(teamName);
            GameController.GetInstance().AddTeamMember(teamName, member1);
            GameController.GetInstance().AddTeamMember(teamName, member2);
            GameController.GetInstance().AddTeamMember(teamName, member3);
            if (picturePath != null)
            {
                GameController.GetInstance().SetTeamIcon(teamName, picturePath);
                picturePath = null;
            }

            MainWindow.SetScreen("MainMenu");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.teamName.Focus();
        }

        private void AddPicture_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.Filter = "png|*.png|jpg|*.jpg*";
            fileDialog.DefaultExt = ".jpg";
            bool dialogStarted = (bool) fileDialog.ShowDialog();

            if(dialogStarted)
            {
                picturePath = fileDialog.FileName;
            }
        }
    }
}
