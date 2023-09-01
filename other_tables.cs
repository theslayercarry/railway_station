using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace railway_station
{
    public partial class other_tables : Form
    {
        Database db = new Database();
        string myConnectionString = "Database=railway_station;Data Source=127.0.0.1;User Id=root;Password=1337";
        public other_tables()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void other_tables_Load(object sender, EventArgs e)
        {

        }

        private void label_routes_Click(object sender, EventArgs e)
        {
            routes frm1 = new routes();
            this.Hide();
            frm1.ShowDialog();
            this.Show();
        }

        private void label_trains_Click(object sender, EventArgs e)
        {
            trains frm1 = new trains();
            this.Hide();
            frm1.ShowDialog();
            this.Show();
        }

        private void label_tickets_Click(object sender, EventArgs e)
        {
            tickets frm1 = new tickets();
            this.Hide();
            frm1.ShowDialog();
            this.Show();
        }
    }
}
