using BARBAREN_beer_pong.ViewModels;
using BARBAREN_beer_pong.Views;
using BARBAREN_beer_pong_lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
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
        public static String errorMessageString;
        public static String stacktrace;
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlAppDomain)]


        public MainWindow()
        {
            ExceptionHandling();
            InitializeComponent();
            DataContext = new ChooseSemesterModel();
            wn = this;
            setImages();
            //ErrorMessageTest();
            
        }

        private void setImages()
        {
            this.backButton.Source = GameController.GetInstance().GetCoreImage("backButton");
            this.logo.Source = GameController.GetInstance().GetCoreImage("logo");
            this.FAQButton.Source = GameController.GetInstance().GetCoreImage("FAQ");
        }

        private void ErrorMessageTest()
        {
            
            errorMessageString = "Hey baby";
            stacktrace = "Hey trace";
            ErrorMessage errorMessage = new ErrorMessage();
            errorMessage.Show();
        }

        private void ExceptionHandling()
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(MyHandler);
        }

        static void MyHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;

            
            errorMessageString = e.Message;
            stacktrace = e.StackTrace;
            ErrorMessage errorMessage = new ErrorMessage();
            errorMessage.Show();

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

        

        private void FAQButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FAQ faqScreen = new FAQ();
            faqScreen.Show();
        }

        private void BackButton_MouseEnter(object sender, MouseEventArgs e)
        {
            this.backButton.Source = GameController.GetInstance().GetCoreImage("backButton2");
        }

        private void BackButton_MouseLeave(object sender, MouseEventArgs e)
        {
            this.backButton.Source = GameController.GetInstance().GetCoreImage("backButton");
        }

        private void FAQButton_MouseEnter(object sender, MouseEventArgs e)
        {
            this.FAQButton.Source = GameController.GetInstance().GetCoreImage("FAQ2");
        }

        private void FAQButton_MouseLeave(object sender, MouseEventArgs e)
        {
            this.FAQButton.Source = GameController.GetInstance().GetCoreImage("FAQ");
        }
    }
}
