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
    public partial class trains_to_routes : Form
    {
        Database db = new Database();
        string myConnectionString = "Database=railway_station;Data Source=127.0.0.1;User Id=root;Password=1337";
        public trains_to_routes()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;


            DataTable trains = new DataTable();
            MySqlConnection connection_trains = new MySqlConnection(myConnectionString);
            {
                MySqlCommand command = new MySqlCommand("select idtrains, name from trains;", connection_trains);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(trains);
            }
            comboBox_trains.DataSource = trains;
            comboBox_trains.DisplayMember = "name";
            comboBox_trains.ValueMember = "idtrains";


            DataTable routes = new DataTable();
            MySqlConnection connection_routes = new MySqlConnection(myConnectionString);
            {
                MySqlCommand command = new MySqlCommand("select idroutes, concat(ct1.name,\" - \",ct2.name) as routes from routes\r\njoin cities ct1 on routes.id_city_from = ct1.idcities\r\njoin cities ct2 on routes.id_city_to = ct2.idcities;", connection_routes);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(routes);
            }
            comboBox_routes.DataSource = routes;
            comboBox_routes.DisplayMember = "routes";
            comboBox_routes.ValueMember = "idroutes";


            comboBox_trains.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_routes.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void trains_to_routes_Load(object sender, EventArgs e)
        {
            textBox_hours.Text = "0";
            textBox_minutes.Text = "0";
        }


        private Boolean check_records()
        {
            var id_train = comboBox_trains.SelectedValue;
            var id_route = comboBox_routes.SelectedValue;

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("select * from trains_to_routes\r\nwhere id_route = @id_route and id_train = @id_train;", db.getConnection());
            command.Parameters.Add("@id_route", MySqlDbType.Int32).Value = id_route;
            command.Parameters.Add("@id_train", MySqlDbType.Int32).Value = id_train;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("У поезда '" + comboBox_trains.Text + "' уже существует маршрут с заданными параметрами.");
                return true;
            }
            else
            {
                return false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (check_records())
            {
                return;
            }
            else if (textBox_hours.Text == "" || textBox_minutes.Text == "")
            {
                MessageBox.Show("1.Введите количество часов в пути.\r\n" +
                        "2.Введите количество минут в пути.\r\n", "Несоответствие форме добавления записи");
            }
            else
            {
                MySqlCommand cmd = new MySqlCommand("insert into trains_to_routes (id_route, id_train, travel_time_hours, travel_time_minutes) values (@id_route, @id_train, @travel_time_hours, @travel_time_minutes);", db.getConnection());
                cmd.Parameters.Add("@id_route", MySqlDbType.VarChar).Value = comboBox_routes.SelectedValue;
                cmd.Parameters.Add("@id_train", MySqlDbType.VarChar).Value = comboBox_trains.SelectedValue;
                cmd.Parameters.Add("@travel_time_hours", MySqlDbType.VarChar).Value = Int32.Parse(textBox_hours.Text);
                cmd.Parameters.Add("@travel_time_minutes", MySqlDbType.VarChar).Value = Int32.Parse(textBox_minutes.Text);

                db.openConnection();

                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Маршрут '" + comboBox_routes.Text + "' успешно добавлен к поезду '" + comboBox_trains.Text + "'.", "Добавление маршрута...");

                    db.closeConnection();

                }
                else
                {
                    MessageBox.Show("Произошла ошибка при добавлении маршрута к поезду.", "Ошибка при добавлении...");
                }
            }
        }
    }
}
