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

namespace MoneyAnalizer
{
    /// <summary>
    /// Logika interakcji dla klasy ChangeFirstName.xaml
    /// </summary>
    public partial class ChangeFirstName : Window
    {
        public ChangeFirstName()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.FirstName = lb_firstname.Text;
            Properties.Settings.Default.Save();
            this.Hide();
        }
    }
}
