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
    /// Logika interakcji dla klasy ChangeSurname.xaml
    /// </summary>
    public partial class ChangeSurname : Window
    {
        public ChangeSurname()
        {
            InitializeComponent();
        }

        private void Save()
        {
            if (lb_surname.Text == "") MessageBox.Show("Wprowadź nazwisko!");

            else
            {
                Properties.Settings.Default.Surname = lb_surname.Text;
                Properties.Settings.Default.Save();
                this.Hide();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void lb_surname_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Return)
            {
                Save();
            }
        }
    }
}
