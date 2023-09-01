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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace railway_station
{
    public partial class routes : Form
    {
        Database db = new Database();
        string myConnectionString = "Database=railway_station;Data Source=127.0.0.1;User Id=root;Password=1337";
        public static int id_route;
        String m; int selectedRow;
        public routes()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void routes_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid_routes(dataGridView1);
            dataGridView1.ClearSelection();
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            routes_add frm1 = new routes_add();
            this.Hide();
            frm1.ShowDialog();
            RefreshDataGrid_routes(dataGridView1);
            dataGridView1.ClearSelection();
            m = null;
            this.Show();
        }

        private void button_change_Click(object sender, EventArgs e)
        {
            if (m != null)
            {
                routes_change frm1 = new routes_change();
                this.Hide();
                frm1.ShowDialog();

                RefreshDataGrid_routes(dataGridView1);
                dataGridView1.ClearSelection();

                this.Show();
                m = null;
            }
            else
                MessageBox.Show("Ни одна запись не выбрана.");
        }


        private void CreateColumns()
        {
            dataGridView1.Columns.Add("idroutes", "id");
            dataGridView1.Columns.Add("route", "Маршрут");
            dataGridView1.Columns[0].Width = 210;
            dataGridView1.Columns[1].Width = 485;

        }

        private void ReadSingleRow_routes(DataGridView dwg, IDataRecord record)
        {
            dwg.Rows.Add(record.GetString(0), record.GetString(1));

        }

        private void RefreshDataGrid_routes(DataGridView dwg)
        {
            dwg.Rows.Clear();
            MySqlCommand command = new MySqlCommand("select idroutes, concat(ct1.name,\" - \",ct2.name) as 'Маршрут' from routes\r\njoin cities ct1 on routes.id_city_from = ct1.idcities\r\njoin cities ct2 on routes.id_city_to = ct2.idcities ;", db.getConnection());
            db.openConnection();

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow_routes(dwg, reader);
            }
            reader.Close();
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (m != null)
            {
                db.openConnection();
                MySqlCommand command = new MySqlCommand("delete from routes where idroutes = @id_route;", db.getConnection());
                command.Parameters.Add("@id_route", MySqlDbType.VarChar).Value = id_route;
                command.ExecuteNonQuery();

                MessageBox.Show("Запись успешно удалена.", "Удаление записи...");

                m = null;

                RefreshDataGrid_routes(dataGridView1);

                db.closeConnection();
                dataGridView1.ClearSelection();
            }
            else
                MessageBox.Show("Ни одна запись не выбрана.");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
            String i;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];

                i = row.Cells[0].Value.ToString();
                m = i;
                id_route = Convert.ToInt32(i);
            }
        }
    }
}
