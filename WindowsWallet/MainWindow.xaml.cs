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
using System.Data.SQLite;
using System.IO;

namespace MoneyAnalizer
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int i = 1; string query;

        DispatcherTimer refresher = new DispatcherTimer();

        private void ChangeAwatar(int j)
        {
            BitmapImage User = new BitmapImage();
            User.BeginInit();
            User.UriSource = new Uri(@"icons/user" + j + ".png", UriKind.Relative);
            img_avatar.Source = User;
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


            //BAZA DANYCH
            if (!File.Exists("moneyChanged.sqlite"))
            {
                SQLiteConnection.CreateFile("moneyChanged.sqlite");

                SQLiteConnection sqlLiteDataBaseConnection;
                sqlLiteDataBaseConnection = new SQLiteConnection("Data Source=moneyChanged.sqlite;Version=3;");
                sqlLiteDataBaseConnection.Open();

                query = "CREATE TABLE Revenue (Tytul VARCHAR(30), Kwota INT)";
                SQLiteCommand command = new SQLiteCommand(query, sqlLiteDataBaseConnection);
                command.ExecuteNonQuery();

                query = "CREATE TABLE Expense (Tytul VARCHAR(30), Kwota INT)";
                command = new SQLiteCommand(query, sqlLiteDataBaseConnection);
                command.ExecuteNonQuery();

                sqlLiteDataBaseConnection.Close();
            }


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

        private void bt_revenueHistory_click(object sender, RoutedEventArgs e)
        {
            History.RevenueHistory window = new History.RevenueHistory();
            window.Show();
        }

        private void bt_expenseHistory_click(object sender, RoutedEventArgs e)
        {
            History.ExpenseHistory window = new History.ExpenseHistory();
            window.Show();
        }

        //Dodawanie i inne operacje na pieniążkach

        private void bt_addExpense(object sender, RoutedEventArgs e)
        {
            if (lb_ExpensePrice.Text != "")
            {
                Properties.Settings.Default.saldo += Convert.ToInt32(lb_ExpensePrice.Text);
                Properties.Settings.Default.lastExpense = Convert.ToInt32(lb_ExpensePrice.Text);
                Properties.Settings.Default.sumExpense += Convert.ToInt32(lb_ExpensePrice.Text);
                Properties.Settings.Default.cashFlow += Convert.ToInt32(lb_ExpensePrice.Text);

                if (Properties.Settings.Default.saldo > Properties.Settings.Default.maxSaldo)
                    Properties.Settings.Default.maxSaldo = Properties.Settings.Default.saldo;

                saveExpense();
            }
            else MessageBox.Show("Wprowadź ilość zarobionych pieniędzy!", "Windows Wallet Error", MessageBoxButton.OK, MessageBoxImage.Error);

            Properties.Settings.Default.Save();

            Page_AddExpense.Visibility = Visibility.Hidden;
            Page_index.Visibility = Visibility.Visible;
        }

        private void bt_addRevenue(object sender, RoutedEventArgs e)
        {
            if (lb_RevenuePrice.Text != "")
            {
                Properties.Settings.Default.saldo -= Convert.ToInt32(lb_RevenuePrice.Text);
                Properties.Settings.Default.lastRevenue = Convert.ToInt32(lb_RevenuePrice.Text);
                Properties.Settings.Default.sumRevenue += Convert.ToInt32(lb_RevenuePrice.Text);
                Properties.Settings.Default.cashFlow += Convert.ToInt32(lb_RevenuePrice.Text);

                saveRevenue();
            }
            else MessageBox.Show("Wprowadź ilość wydanych pieniędzy!", "Windows Wallet Error", MessageBoxButton.OK, MessageBoxImage.Error);

            Properties.Settings.Default.Save();

            Page_AddRevenue.Visibility = Visibility.Hidden;
            Page_index.Visibility = Visibility.Visible;
        }

        //Zapisywanie logów do SQLite

        private void saveExpense()
        {
            if (lb_ExpenseTitle.Text == "") lb_ExpenseTitle.Text = "-";

            query = "insert into Expense (Tytul, Kwota) values ('" + lb_ExpenseTitle.Text + "', " + lb_ExpensePrice.Text + ")";

            SQLiteConnection SaveConnection;
            SaveConnection = new SQLiteConnection("Data Source = moneyChanged.sqlite; Version = 3;");
            SaveConnection.Open();

            SQLiteCommand save = new SQLiteCommand(query, SaveConnection);
            save.ExecuteNonQuery();

            SaveConnection.Close();

        }

        private void saveRevenue()
        {
            if (lb_RevenueTitle.Text == "") lb_RevenueTitle.Text = "-";

            query = "insert into Revenue (Tytul, Kwota) values ('" + lb_RevenueTitle.Text + "', " + lb_RevenuePrice.Text + ")";

            SQLiteConnection SaveConnection;
            SaveConnection = new SQLiteConnection("Data Source = moneyChanged.sqlite; Version = 3;");
            SaveConnection.Open();

            SQLiteCommand save = new SQLiteCommand(query, SaveConnection);
            save.ExecuteNonQuery();

            SaveConnection.Close();
        }

        private void bt_expense(object sender, RoutedEventArgs e)
        {

        }

        
    }
}
