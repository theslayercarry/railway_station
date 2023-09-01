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
    public partial class trains_change : Form
    {
        Database db = new Database();
        string myConnectionString = "Database=railway_station;Data Source=127.0.0.1;User Id=root;Password=1337";
        String help, train, description, departure_time;
        int id_city, id_station;

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox_name.Text == "" || textBox_description.Text == "" || textBox_departure_time.Text == "")
            {
                MessageBox.Show("1.Введите название поезда.\r\n" +
                        "2.Введите описание поезда.\r\n" +
                        "3.Введите время отбытия поезда.\r\n", "Несоответствие форме изменения записи");
            }
            else
            {
                MySqlCommand cmd = new MySqlCommand("update trains set name = @name, id_city = @id_city, id_station = @id_station, description = @description, departure_time = @departure_time where idtrains = @id_train;", db.getConnection());
                cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = textBox_name.Text;
                cmd.Parameters.Add("@id_city", MySqlDbType.VarChar).Value = comboBox_city.SelectedValue;
                cmd.Parameters.Add("@id_station", MySqlDbType.VarChar).Value = comboBox_station.SelectedValue;
                cmd.Parameters.Add("@description", MySqlDbType.VarChar).Value = textBox_description.Text;
                cmd.Parameters.Add("@departure_time", MySqlDbType.VarChar).Value = textBox_departure_time.Text;
                cmd.Parameters.Add("@id_train", MySqlDbType.VarChar).Value = trains.id_train;

                db.openConnection();
                if (textBox_name.Text == "" || textBox_description.Text == "" || textBox_departure_time.Text == "")
                {
                    MessageBox.Show("1.Введите название поезда.\r\n" +
                        "2.Введите описание поезда.\r\n" +
                        "3.Введите время отбытия поезда.\r\n", "Несоответствие форме изменения записи");
                }
                else if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Данные успешно изменены.", "Изменение данных...");

                    db.closeConnection();

                }
                else
                {
                    MessageBox.Show("Ошибка при изменении данных.");
                }
            }
        }

        DateTime date;
        public trains_change()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            DataTable city = new DataTable();
            MySqlConnection connection_city = new MySqlConnection(myConnectionString);
            {
                MySqlCommand command = new MySqlCommand("select idcities, name from cities;", connection_city);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(city);
            }
            comboBox_city.DataSource = city;
            comboBox_city.DisplayMember = "name";
            comboBox_city.ValueMember = "idcities";


            DataTable station = new DataTable();
            MySqlConnection connection_station = new MySqlConnection(myConnectionString);
            {
                MySqlCommand command = new MySqlCommand("select idstations, name from stations;", connection_station);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(station);
            }
            comboBox_station.DataSource = station;
            comboBox_station.DisplayMember = "name";
            comboBox_station.ValueMember = "idstations";


            comboBox_city.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_station.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void trains_change_Load(object sender, EventArgs e)
        {
            MySqlConnection train_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_train = new MySqlCommand("select name from trains where idtrains = @id_train;", train_connection);
            command_train.Parameters.Add("@id_train", MySqlDbType.VarChar).Value = trains.id_train;
            train_connection.Open();

            train = command_train.ExecuteScalar().ToString();
            train_connection.Close();

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection city_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_city = new MySqlCommand("select id_city from trains where idtrains = @id_train;", city_connection);
            command_city.Parameters.Add("@id_train", MySqlDbType.VarChar).Value = trains.id_train;
            city_connection.Open();

            help = command_city.ExecuteScalar().ToString();
            id_city = Int32.Parse(help);
            city_connection.Close();

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection station_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_station = new MySqlCommand("select id_station from trains where idtrains = @id_train;", station_connection);
            command_station.Parameters.Add("@id_train", MySqlDbType.VarChar).Value = trains.id_train;
            station_connection.Open();

            help = command_station.ExecuteScalar().ToString();
            id_station = Int32.Parse(help);
            station_connection.Close();

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection description_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_description = new MySqlCommand("select description from trains where idtrains = @id_train;", description_connection);
            command_description.Parameters.Add("@id_train", MySqlDbType.VarChar).Value = trains.id_train;
            description_connection.Open();

            description = command_description.ExecuteScalar().ToString();
            description_connection.Close();

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection departure_time_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_departure_time = new MySqlCommand("select departure_time from trains where idtrains = @id_train;", departure_time_connection);
            command_departure_time.Parameters.Add("@id_train", MySqlDbType.VarChar).Value = trains.id_train;
            departure_time_connection.Open();

            departure_time = command_departure_time.ExecuteScalar().ToString();
            departure_time_connection.Close();

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            comboBox_city.SelectedValue = id_city;
            comboBox_station.SelectedValue = id_station;


            date = DateTime.Parse(departure_time);
            date.GetDateTimeFormats('u');
            departure_time = date.ToString("HH:mm");


            textBox_name.Text = train;
            textBox_description.Text = description;
            textBox_departure_time.Text = departure_time;

            label_id.Text = trains.id_train.ToString();
        }
    }
}
