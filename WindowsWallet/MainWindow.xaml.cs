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
using System.Windows.Threading;

namespace MoneyAnalizer
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int i = 0;

        DispatcherTimer refresher = new DispatcherTimer();

        private void ChangeAwatar(int j)
        {
            BitmapImage User = new BitmapImage();
            User.BeginInit();
            switch (j)
            {
                case 1:
                    User.UriSource = new Uri(@"icons/user.png", UriKind.Relative);
                    img_avatar.Source = User;
                    break;

                case 2:
                    User.UriSource = new Uri(@"icons/user_women2.png", UriKind.Relative);
                    img_avatar.Source = User;
                    break;

                case 3:
                    User.UriSource = new Uri(@"icons/user_waiter2.png", UriKind.Relative);
                    img_avatar.Source = User;
                    break;

                case 4:
                    User.UriSource = new Uri(@"icons/user_women.png", UriKind.Relative);
                    img_avatar.Source = User;
                    break;

                case 5:
                    User.UriSource = new Uri(@"icons/user_accountant.png", UriKind.Relative);
                    img_avatar.Source = User;
                    break;

                case 6:
                    User.UriSource = new Uri(@"icons/user_cook.png", UriKind.Relative);
                    img_avatar.Source = User;
                    break;

                case 7:
                    User.UriSource = new Uri(@"icons/user_doctor.png", UriKind.Relative);
                    img_avatar.Source = User;
                    break;

                case 8:
                    User.UriSource = new Uri(@"icons/user_girl.png", UriKind.Relative);
                    img_avatar.Source = User;
                    break;

                case 9:
                    User.UriSource = new Uri(@"icons/user_girl2.png", UriKind.Relative);
                    img_avatar.Source = User;
                    break;

                case 10:
                    User.UriSource = new Uri(@"icons/user_grandpa.png", UriKind.Relative);
                    img_avatar.Source = User;
                    break;

                case 11:
                    User.UriSource = new Uri(@"icons/user_solider.png", UriKind.Relative);
                    img_avatar.Source = User;
                    break;

                case 12:
                    User.UriSource = new Uri(@"icons/user_thief.png", UriKind.Relative);
                    img_avatar.Source = User;
                    break;

                case 13:
                    User.UriSource = new Uri(@"icons/user_waiter.png", UriKind.Relative);
                    img_avatar.Source = User;
                    break;
            }
            User.EndInit();
        }

        public MainWindow()
        {
            InitializeComponent();
            i = Properties.Settings.Default.AwatarID;
            ChangeAwatar(i);
            lb_FirstName.Content = Properties.Settings.Default.FirstName;
            lb_Surname.Content = Properties.Settings.Default.Surname;

            refresher.Interval = new TimeSpan(0, 0, 1);
            refresher.Tick += new EventHandler(Timer_Tick);
            refresher.Start();

            /*
            Properties.Settings.Default.saldo = 0;
            Properties.Settings.Default.lastExpense = 0;
            Properties.Settings.Default.sumExpense = 0;
            Properties.Settings.Default.cashFlow = 0;
            Properties.Settings.Default.maxSaldo = 0;
            Properties.Settings.Default.lastRevenue = 0;
            Properties.Settings.Default.sumRevenue = 0;

            Properties.Settings.Default.Save();
            */

        }

        int k = 0;
        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            k++;
            ChangeAwatar(k);
            Properties.Settings.Default.AwatarID = k;
            Properties.Settings.Default.Save();
            if (k >= 13) k = 0;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            i = Properties.Settings.Default.AwatarID;
            ChangeAwatar(i);
            lb_FirstName.Content = Properties.Settings.Default.FirstName;
            lb_Surname.Content = Properties.Settings.Default.Surname;
            lb_saldo.Content = "Saldo: " + Properties.Settings.Default.saldo + "zł";
            lb_lastRevenue.Content = "Ostatni wydatek: " + Properties.Settings.Default.lastRevenue + "zł";
            lb_sumRevenue.Content = "Łączne wydatki: " + Properties.Settings.Default.sumRevenue + "zł";

            //STATY

            lb_stats_cashFlow.Content = Properties.Settings.Default.cashFlow + "zł";
            lb_stats_lastExpense.Content = Properties.Settings.Default.lastExpense + "zł";
            lb_stats_lastRevenue.Content = Properties.Settings.Default.lastRevenue + "zł";
            lb_stats_maxSaldo.Content = Properties.Settings.Default.maxSaldo + "zł";
            lb_stats_saldo.Content = Properties.Settings.Default.saldo + "zł";
            lb_stats_sumExpense.Content = Properties.Settings.Default.sumExpense + "zł";
            lb_sum_sumRevenue.Content = Properties.Settings.Default.sumRevenue + "zł";




        }

        private void Buton_FirstNameClick(object sender, MouseButtonEventArgs e)
        {
            ChangeFirstName window = new ChangeFirstName();
            window.Show();
        }

        private void Button_SurnameClick(object sender, MouseButtonEventArgs e)
        {
            ChangeSurname window = new ChangeSurname();
            window.Show();
        }


        //Nawigacja
        private void bt_addExpense_click(object sender, RoutedEventArgs e)
        {
            Page_index.Visibility = Visibility.Hidden;
            Page_AddExpense.Visibility = Visibility.Visible;
        }

        private void bt_addRevenue_click(object sender, RoutedEventArgs e)
        {
            Page_index.Visibility = Visibility.Hidden;
            Page_AddRevenue.Visibility = Visibility.Visible;
        }

        private void bt_stats_click(object sender, RoutedEventArgs e)
        {
            Page_index.Visibility = Visibility.Hidden;
            Page_Stats.Visibility = Visibility.Visible;
        }

        private void bt_backToIndex_click(object sender, RoutedEventArgs e)
        {
            Page_AddExpense.Visibility = Visibility.Hidden;
            Page_AddRevenue.Visibility = Visibility.Hidden;
            Page_Stats.Visibility = Visibility.Hidden;
            Page_index.Visibility = Visibility.Visible;
        }

        //Dodawanie i inne operacje na pieniążkach

        private void bt_addExpense(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.saldo += Convert.ToInt32(lb_ExpensePrice.Text);
            Properties.Settings.Default.lastExpense = Convert.ToInt32(lb_ExpensePrice.Text);
            Properties.Settings.Default.sumExpense += Convert.ToInt32(lb_ExpensePrice.Text);
            Properties.Settings.Default.cashFlow += Convert.ToInt32(lb_ExpensePrice.Text);

            if (Properties.Settings.Default.saldo > Properties.Settings.Default.maxSaldo)
                Properties.Settings.Default.maxSaldo = Properties.Settings.Default.saldo;

            Properties.Settings.Default.Save();

            Page_AddExpense.Visibility = Visibility.Hidden;
            Page_index.Visibility = Visibility.Visible;
        }

        private void bt_addRevenue(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.saldo -= Convert.ToInt32(lb_RevenuePrice.Text);
            Properties.Settings.Default.lastRevenue = Convert.ToInt32(lb_RevenuePrice.Text);
            Properties.Settings.Default.sumRevenue += Convert.ToInt32(lb_RevenuePrice.Text);
            Properties.Settings.Default.cashFlow += Convert.ToInt32(lb_RevenuePrice.Text);

            Properties.Settings.Default.Save();

            Page_AddRevenue.Visibility = Visibility.Hidden;
            Page_index.Visibility = Visibility.Visible;
        }
    }
}
