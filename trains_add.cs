using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace railway_station
{
    public partial class trains_add : Form
    {
        Database db = new Database();
        string myConnectionString = "Database=railway_station;Data Source=127.0.0.1;User Id=root;Password=1337";
        public trains_add()
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

        private void trains_add_Load(object sender, EventArgs e)
        {
            maskedTextBox_departure_time.Mask = "00:00";
            maskedTextBox_departure_time.ValidatingType = typeof(DateTime);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox_name.Text == "" || textBox_description.Text == "" || maskedTextBox_departure_time.MaskCompleted == false)
            {
                MessageBox.Show("1.Введите название поезда.\r\n" +
                        "2.Введите описание поезда.\r\n" +
                        "3.Введите время отбытия поезда.\r\n", "Несоответствие форме добавления записи");
            }
            else
            {
                MySqlCommand cmd = new MySqlCommand("insert into trains (name, id_city, id_station, description, departure_time) values (@name, @id_city, @id_station, @description, @departure_time);", db.getConnection());
                cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = textBox_name.Text;
                cmd.Parameters.Add("@id_city", MySqlDbType.VarChar).Value = comboBox_city.SelectedValue;
                cmd.Parameters.Add("@id_station", MySqlDbType.VarChar).Value = comboBox_station.SelectedValue;
                cmd.Parameters.Add("@description", MySqlDbType.VarChar).Value = textBox_description.Text;
                cmd.Parameters.Add("@departure_time", MySqlDbType.VarChar).Value = maskedTextBox_departure_time.Text;

                db.openConnection();
                if (textBox_name.Text == "" || textBox_description.Text == "" || maskedTextBox_departure_time.MaskCompleted == false)
                {
                    MessageBox.Show("1.Введите название поезда.\r\n" +
                        "2.Введите описание поезда.\r\n" +
                        "3.Введите время отбытия поезда.\r\n", "Несоответствие форме добавления записи");
                }
                else if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Поезд '" + textBox_name.Text + "' успешно добавлен.", "Добавление поезда...");

                    db.closeConnection();

                }
                else
                {
                    MessageBox.Show("Произошла ошибка при добавлении поезда.", "Ошибка при добавлении...");
                }
            }
        }
    }
}
