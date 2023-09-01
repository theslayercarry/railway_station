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
    public partial class tickets : Form
    {
        Database db = new Database();
        string myConnectionString = "Database=railway_station;Data Source=127.0.0.1;User Id=root;Password=1337";
        public static int id_ticket;
        String m; int selectedRow;
        public tickets()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void tickets_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid_tickets(dataGridView1);
            dataGridView1.ClearSelection();
        }

        private void button_change_Click(object sender, EventArgs e)
        {

            if (m != null)
            {
                tickets_change frm1 = new tickets_change();
                this.Hide();
                frm1.ShowDialog();

                RefreshDataGrid_tickets(dataGridView1);
                dataGridView1.ClearSelection();

                this.Show();
                m = null;
            }
            else
                MessageBox.Show("Ни одна запись не выбрана.");
        }


        private void CreateColumns()
        {
            dataGridView1.Columns.Add("idtickets", "id");
            dataGridView1.Columns.Add("passenger", "Пассажир");
            dataGridView1.Columns.Add("train", "Поезд");
            dataGridView1.Columns.Add("route", "Маршрут");
            dataGridView1.Columns.Add("wagon", "Вагон");
            dataGridView1.Columns.Add("departure_time", "Время отправления");
            dataGridView1.Columns.Add("arrival_time", "Время прибытия");
            dataGridView1.Columns[1].Width = 320;
            dataGridView1.Columns[2].Width = 180;
            dataGridView1.Columns[3].Width = 225;
            dataGridView1.Columns[4].Width = 210;
            dataGridView1.Columns[5].Width = 220;
            dataGridView1.Columns[6].Width = 220;
        }

        private void ReadSingleRow_tickets(DataGridView dwg, IDataRecord record)
        {
            dwg.Rows.Add(record.GetString(0), record.GetString(1), record.GetString(2), record.GetString(3), record.GetString(4), record.GetDateTime(5), record.GetDateTime(6));

        }

        private void RefreshDataGrid_tickets(DataGridView dwg)
        {
            dwg.Rows.Clear();
            MySqlCommand command = new MySqlCommand("SELECT idtickets,concat(passengers.surname,\" \",passengers.name,\" \", passengers.patronymic) as 'Пассажир', trains.name as 'Поезд', concat(ct1.name,\" - \", ct2.name) as 'Маршрут', wagons.name as 'Вагон', tickets.departure_time as 'Время отправления', tickets.arrival_time as 'Время прибытия'\r\nFROM tickets\r\nJOIN passengers ON tickets.id_passenger = passengers.idpassengers\r\nJOIN trains on tickets.id_train = trains.idtrains\r\nJOIN routes on tickets.id_route = routes.idroutes\r\nJOIN cities ct1 on routes.id_city_from = ct1.idcities\r\nJOIN cities ct2 on routes.id_city_to = ct2.idcities\r\nJOIN wagons on tickets.id_wagon = wagons.idwagons;", db.getConnection());
            db.openConnection();

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow_tickets(dwg, reader);
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
                id_ticket = Convert.ToInt32(i);
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (m != null)
            {
                db.openConnection();
                MySqlCommand command = new MySqlCommand("delete from tickets where idtickets = @id_ticket;", db.getConnection());
                command.Parameters.Add("@id_ticket", MySqlDbType.VarChar).Value = id_ticket;
                command.ExecuteNonQuery();

                MessageBox.Show("Запись успешно удалена.", "Удаление записи...");

                m = null;

                RefreshDataGrid_tickets(dataGridView1);

                db.closeConnection();
                dataGridView1.ClearSelection();
            }
            else
                MessageBox.Show("Ни одна запись не выбрана.");
        }
    }
}
