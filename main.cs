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
    public partial class main : Form
    {
        Database db = new Database();
        string myConnectionString = "Database=railway_station;Data Source=127.0.0.1;User Id=root;Password=1337";

        public static int id_city_from, id_city_to, id_route, id_train, id_wagon, seat_number, id_passenger;
        public static String date_string, time, ticket_cost, train_name, station_name, departure_time, arrival_time;

        DateTime date;
        int value, i, selectedRow;
        public main()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            DataTable city_from = new DataTable();
            MySqlConnection connection_city_from = new MySqlConnection(myConnectionString);
            {
                MySqlCommand command = new MySqlCommand("select idcities, name from cities", connection_city_from);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(city_from);
            }
            comboBox_сity_from.DataSource = city_from;
            comboBox_сity_from.DisplayMember = "name";
            comboBox_сity_from.ValueMember = "idcities";
            ///////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////
            DataTable city_to = new DataTable();
            MySqlConnection connection_city_to = new MySqlConnection(myConnectionString);
            {
                MySqlCommand command = new MySqlCommand("select idcities, name from cities", connection_city_to);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(city_to);
            }
            comboBox_city_to.DataSource = city_to;
            comboBox_city_to.DisplayMember = "name";
            comboBox_city_to.ValueMember = "idcities";


            comboBox_сity_from.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_city_to.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            other_tables frm1 = new other_tables();
            this.Hide();
            frm1.ShowDialog();
            this.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            value = (int)comboBox_сity_from.SelectedValue;
            comboBox_сity_from.SelectedValue = comboBox_city_to.SelectedValue;
            comboBox_city_to.SelectedValue = value;
        }

        private void main_Load(object sender, EventArgs e)
        {
            date = dateTimePicker1.Value;
            date.GetDateTimeFormats('u');
            date_string = date.ToString("yyyy-MM-dd");

        }

        private void CreateColumns()
        {
            dataGridView1.Columns.Add("trains.idtrains", "id");
            dataGridView1.Columns.Add("trains.name", "Поезд");
            dataGridView1.Columns.Add("cities.name", "Станция отправления");
            dataGridView1.Columns.Add("trains.description", "Услуги и сервисы");
            dataGridView1.Columns.Add("departure_time", "Время отправления");
            dataGridView1.Columns.Add("travel_time", "Время в пути");
            dataGridView1.Columns[1].Width = 185;
            dataGridView1.Columns[2].Width = 140;
            dataGridView1.Columns[3].Width = 240;
            dataGridView1.Columns[4].Width = 130;
            dataGridView1.Columns[0].Visible = false;

        }
        private void ReadSingleRow_trains(DataGridView dwg, IDataRecord record)
        {
            dwg.Rows.Add(record.GetString(0), record.GetString(1), record.GetString(2), record.GetString(3), record.GetString(4), record.GetString(5));

        }

        private void comboBox_сity_from_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (id_city_from == id_city_to)
            {
                MessageBox.Show("Произошла ошибка при загрузке мест.");
            }
            else if (dataGridView1.RowCount == 0)
            {
                MessageBox.Show("Не найдено ни одного билета.");
            }
            else if (dataGridView1.RowCount > 0 && label1.Text == "")
            {
                MessageBox.Show("Выберите поезд.");
            }
            else
            {
                id_city_from = (int)comboBox_сity_from.SelectedValue;
                id_city_to = (int)comboBox_city_to.SelectedValue;
                id_wagon = 0;
                date = dateTimePicker1.Value;
                date.GetDateTimeFormats('u');
                date_string = date.ToString("yyyy-MM-dd");

                choosing_a_seat frm1 = new choosing_a_seat();
                this.Hide();
                frm1.ShowDialog();
                this.Show();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (id_city_from == id_city_to)
            {
                MessageBox.Show("Произошла ошибка при загрузке мест.");
            }
            else if (dataGridView1.RowCount == 0)
            {
                MessageBox.Show("Не найдено ни одного билета.");
            }
            else if (dataGridView1.RowCount > 0 && label1.Text == "")
            {
                MessageBox.Show("Выберите поезд.");
            }
            else
            {
                id_city_from = (int)comboBox_сity_from.SelectedValue;
                id_city_to = (int)comboBox_city_to.SelectedValue;
                id_wagon = 2;
                date = dateTimePicker1.Value;
                date.GetDateTimeFormats('u');
                date_string = date.ToString("yyyy-MM-dd");

                choosing_a_seat frm1 = new choosing_a_seat();
                this.Hide();
                frm1.ShowDialog();
                this.Show();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            date = dateTimePicker1.Value;
            date.GetDateTimeFormats('u');
            date_string = date.ToString("yyyy-MM-dd");
            if (time != "false")
                label3.Text = date_string + "    " + time;
            else
                label3.Text = date_string;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
            String i;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];

                i = row.Cells[0].Value.ToString();
                id_train = Convert.ToInt32(i);

                train_name = row.Cells[1].Value.ToString();
                station_name = row.Cells[2].Value.ToString();
                time = row.Cells[4].Value.ToString();

                date = dateTimePicker1.Value;
                date.GetDateTimeFormats('u');
                date_string = date.ToString("yyyy-MM-dd");

                label1.Text = train_name;
                label3.Text = date_string + "    " + time;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (id_city_from == id_city_to)
            {
                MessageBox.Show("Произошла ошибка при загрузке мест.");
            }
            else if (dataGridView1.RowCount == 0)
            {
                MessageBox.Show("Не найдено ни одного билета.");
            }
            else if (dataGridView1.RowCount > 0 && label1.Text == "")
            {
                MessageBox.Show("Выберите поезд.");
            }
            else
            {
                id_city_from = (int)comboBox_сity_from.SelectedValue;
                id_city_to = (int)comboBox_city_to.SelectedValue;
                id_wagon = 1;
                date = dateTimePicker1.Value;
                date.GetDateTimeFormats('u');
                date_string = date.ToString("yyyy-MM-dd");

                choosing_a_seat frm1 = new choosing_a_seat();
                this.Hide();
                frm1.ShowDialog();
                this.Show();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (id_city_from == id_city_to)
            {
                MessageBox.Show("Произошла ошибка при загрузке мест.");
            }
            else if (dataGridView1.RowCount == 0)
            {
                MessageBox.Show("Не найдено ни одного билета.");
            }
            else if (dataGridView1.RowCount > 0 && label1.Text == "")
            {
                MessageBox.Show("Выберите поезд.");
            }
            else
            {
                id_city_from = (int)comboBox_сity_from.SelectedValue;
                id_city_to = (int)comboBox_city_to.SelectedValue;
                id_wagon = 3;
                date = dateTimePicker1.Value;
                date.GetDateTimeFormats('u');
                date_string = date.ToString("yyyy-MM-dd");
                
                choosing_a_seat frm1 = new choosing_a_seat();
                this.Hide();
                frm1.ShowDialog();
                this.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            id_city_from = (int)comboBox_сity_from.SelectedValue;
            id_city_to = (int)comboBox_city_to.SelectedValue;
            time = "false";

            if (id_city_from == id_city_to)
            {
                MessageBox.Show("Произошла ошибка при загрузке предложений.");
            }
            else
            {
                if (i++ == 0)
                {
                    CreateColumns();
                }
                RefreshDataGrid_trains(dataGridView1);
                dataGridView1.ClearSelection();
                label1.Text = "";

                date = dateTimePicker1.Value;
                date.GetDateTimeFormats('u');
                date_string = date.ToString("yyyy-MM-dd");

                label3.Text = date_string;
            }

        }

        private void RefreshDataGrid_trains(DataGridView dwg)
        {
            dwg.Rows.Clear();
            MySqlCommand command = new MySqlCommand("select trains.idtrains, trains.name, stations.name, trains.description,trains.departure_time, concat(trains_to_routes.travel_time_hours,' ч ',trains_to_routes.travel_time_minutes, ' м') as travel_time from trains\r\njoin stations on trains.id_station = stations.idstations\r\njoin trains_to_routes on trains.idtrains = trains_to_routes.id_train\r\njoin routes on trains_to_routes.id_route = routes.idroutes\r\nwhere routes.id_city_from = @id_city_from and routes.id_city_to = @id_city_to", db.getConnection());
            command.Parameters.Add("@id_city_from", MySqlDbType.VarChar).Value = id_city_from;
            command.Parameters.Add("@id_city_to", MySqlDbType.VarChar).Value = id_city_to;
            db.openConnection();

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow_trains(dwg, reader);
            }
            reader.Close();
        }
    }
}
