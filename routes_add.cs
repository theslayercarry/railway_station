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
    public partial class routes_add : Form
    {
        Database db = new Database();
        string myConnectionString = "Database=railway_station;Data Source=127.0.0.1;User Id=root;Password=1337";
        public routes_add()
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
            comboBox_city_from.DataSource = city_from;
            comboBox_city_from.DisplayMember = "name";
            comboBox_city_from.ValueMember = "idcities";


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


            comboBox_city_from.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_city_to.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        private void routes_add_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (check_records())
            {
                return;
            }
            else if ((int)comboBox_city_from.SelectedValue == (int)comboBox_city_to.SelectedValue)
            {
                MessageBox.Show("Произошла ошибка при добавлении маршрута.");
            }
            else
            {
                MySqlCommand cmd = new MySqlCommand("insert into routes (id_city_from, id_city_to) values (@id_city_from, @id_city_to);", db.getConnection());
                cmd.Parameters.Add("@id_city_from", MySqlDbType.VarChar).Value = comboBox_city_from.SelectedValue;
                cmd.Parameters.Add("@id_city_to", MySqlDbType.VarChar).Value = comboBox_city_to.SelectedValue;

                db.openConnection();
              
                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Маршрут '" + comboBox_city_from.Text + " - " + comboBox_city_to.Text + "' успешно добавлен.", "Добавление маршрута...");

                    db.closeConnection();

                }
                else
                {
                    MessageBox.Show("Произошла ошибка при добавлении маршрута.", "Ошибка при добавлении...");
                }
            }
        }

        private Boolean check_records()
        {
            var id_city_to = comboBox_city_to.SelectedValue;
            var id_city_from = comboBox_city_from.SelectedValue;

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("select * from routes where id_city_from = @id_city_from and id_city_to = @id_city_to", db.getConnection());
            command.Parameters.Add("@id_city_from", MySqlDbType.Int32).Value = id_city_from;
            command.Parameters.Add("@id_city_to", MySqlDbType.Int32).Value = id_city_to;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Маршрут с заданными параметрами уже существует.");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
