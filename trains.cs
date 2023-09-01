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
    public partial class trains : Form
    {
        Database db = new Database();
        string myConnectionString = "Database=railway_station;Data Source=127.0.0.1;User Id=root;Password=1337";
        public static int id_train;
        String m; int selectedRow;
        public trains()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void trains_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid_trains(dataGridView1);
            dataGridView1.ClearSelection();
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            trains_add frm1 = new trains_add();
            this.Hide();
            frm1.ShowDialog();
            RefreshDataGrid_trains(dataGridView1);
            dataGridView1.ClearSelection();
            m = null;
            this.Show();
        }

        private void button_change_Click(object sender, EventArgs e)
        {

            if (m != null)
            {
                trains_change frm1 = new trains_change();
                this.Hide();
                frm1.ShowDialog();

                RefreshDataGrid_trains(dataGridView1);
                dataGridView1.ClearSelection();

                this.Show();
                m = null;
            }
            else
                MessageBox.Show("Ни одна запись не выбрана.");
        }


        private void CreateColumns()
        {
            dataGridView1.Columns.Add("idtrains", "id");
            dataGridView1.Columns.Add("name", "Поезд");
            dataGridView1.Columns.Add("id_city", "Город");
            dataGridView1.Columns.Add("id_station", "Станция");
            dataGridView1.Columns.Add("description", "Описание");
            dataGridView1.Columns.Add("departure_time", "Время отбытия");
            dataGridView1.Columns[1].Width = 320;
            dataGridView1.Columns[2].Width = 180;
            dataGridView1.Columns[3].Width = 225;
            dataGridView1.Columns[4].Width = 210;
            dataGridView1.Columns[5].Width = 190;
        }

        private void ReadSingleRow_trains(DataGridView dwg, IDataRecord record)
        {
            dwg.Rows.Add(record.GetString(0), record.GetString(1), record.GetString(2), record.GetString(3), record.GetString(4), record.GetString(5));

        }

        private void RefreshDataGrid_trains(DataGridView dwg)
        {
            dwg.Rows.Clear();
            MySqlCommand command = new MySqlCommand("SELECT idtrains, trains.name as 'Поезд', cities.name as 'Город', stations.name as 'Станция', trains.description 'Описание', trains.departure_time as 'Время отбытия'\r\nFROM trains\r\nJOIN stations ON trains.id_station = stations.idstations\r\nJOIN cities on trains.id_city = cities.idcities;", db.getConnection());
            db.openConnection();

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow_trains(dwg, reader);
            }
            reader.Close();
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
                id_train = Convert.ToInt32(i);
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (m != null)
            {
                db.openConnection();
                MySqlCommand command = new MySqlCommand("delete from trains where idtrains = @id_train;", db.getConnection());
                command.Parameters.Add("@id_train", MySqlDbType.VarChar).Value = id_train;
                command.ExecuteNonQuery();

                MessageBox.Show("Запись успешно удалена.", "Удаление записи...");

                m = null;

                RefreshDataGrid_trains(dataGridView1);

                db.closeConnection();
                dataGridView1.ClearSelection();
            }
            else
                MessageBox.Show("Ни одна запись не выбрана.");
        }

        private void label1_Click(object sender, EventArgs e)
        {
            trains_to_routes frm1 = new trains_to_routes();
            this.Hide();
            frm1.ShowDialog();
            this.Show();
        }
    }
}
