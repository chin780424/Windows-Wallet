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
using System.Data.SQLite;
using System.Data;

namespace MoneyAnalizer.History
{
    /// <summary>
    /// Logika interakcji dla klasy ExpenseHistory.xaml
    /// </summary>
    public partial class ExpenseHistory : Window
    {
        public void loadTable()
        {
            SQLiteConnection loadConnection = new SQLiteConnection("Data Source=moneyChanged.sqlite;Version=3;");
            try
            {
                loadConnection.Open();
                string query = "select * from Expense";

                SQLiteCommand loadCommand = new SQLiteCommand(query, loadConnection);
                loadCommand.ExecuteNonQuery();

                SQLiteDataAdapter dataAdp = new SQLiteDataAdapter(loadCommand);
                DataTable dt = new DataTable("Expense");
                dataAdp.Fill(dt);
                dg_expense.ItemsSource = dt.DefaultView;
                dataAdp.Update(dt);


                loadConnection.Close();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public ExpenseHistory()
        {
            InitializeComponent();
            loadTable();
        }
    }
}
