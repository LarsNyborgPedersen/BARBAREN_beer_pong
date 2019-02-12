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
    /// Interaction logic for FAQ.xaml
    /// </summary>
    public partial class FAQ : Window
    {
        public FAQ()
        {
            InitializeComponent();
            this.FAQText.Text = GameController.GetInstance().GetFAQ();

        }
    }
}
