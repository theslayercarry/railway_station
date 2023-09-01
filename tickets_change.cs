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
    public partial class tickets_change : Form
    {
        Database db = new Database();
        string myConnectionString = "Database=railway_station;Data Source=127.0.0.1;User Id=root;Password=1337";
        String help, departure_time, arrival_time;
        DateTime date;
        int id_passenger, id_route, id_train, id_wagon;
        public tickets_change()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;


            DataTable passenger = new DataTable();
            MySqlConnection connection_passenger = new MySqlConnection(myConnectionString);
            {
                MySqlCommand command = new MySqlCommand("select idpassengers, concat(passengers.surname,\" \",passengers.name,\" \", passengers.patronymic) as name from passengers;", connection_passenger);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(passenger);
            }
            comboBox_passenger.DataSource = passenger;
            comboBox_passenger.DisplayMember = "name";
            comboBox_passenger.ValueMember = "idpassengers";


            DataTable train = new DataTable();
            MySqlConnection connection_train = new MySqlConnection(myConnectionString);
            {
                MySqlCommand command = new MySqlCommand("select idtrains, name from trains;", connection_train);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(train);
            }
            comboBox_train.DataSource = train;
            comboBox_train.DisplayMember = "name";
            comboBox_train.ValueMember = "idtrains";


            DataTable route = new DataTable();
            MySqlConnection connection_route = new MySqlConnection(myConnectionString);
            {
                MySqlCommand command = new MySqlCommand("select idroutes, concat(ct1.name,\" - \",ct2.name) as route from routes\r\njoin cities ct1 on routes.id_city_from = ct1.idcities\r\njoin cities ct2 on routes.id_city_to = ct2.idcities ;", connection_route);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(route);
            }
            comboBox_route.DataSource = route;
            comboBox_route.DisplayMember = "route";
            comboBox_route.ValueMember = "idroutes";


            DataTable wagon = new DataTable();
            MySqlConnection connection_wagon = new MySqlConnection(myConnectionString);
            {
                MySqlCommand command = new MySqlCommand("select idwagons, name from wagons;", connection_wagon);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(wagon);
            }
            comboBox_wagon.DataSource = wagon;
            comboBox_wagon.DisplayMember = "name";
            comboBox_wagon.ValueMember = "idwagons";



            comboBox_passenger.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_route.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_train.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_wagon.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void tickets_change_Load(object sender, EventArgs e)
        {
            MySqlConnection passenger_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_passenger = new MySqlCommand("select id_passenger from tickets where idtickets = @id_ticket;", passenger_connection);
            command_passenger.Parameters.Add("@id_ticket", MySqlDbType.VarChar).Value = tickets.id_ticket;
            passenger_connection.Open();

            help = command_passenger.ExecuteScalar().ToString();
            id_passenger = Int32.Parse(help);
            passenger_connection.Close();

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection route_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_route = new MySqlCommand("select id_route from tickets where idtickets = @id_ticket;", route_connection);
            command_route.Parameters.Add("@id_ticket", MySqlDbType.VarChar).Value = tickets.id_ticket;
            route_connection.Open();

            help = command_route.ExecuteScalar().ToString();
            id_route = Int32.Parse(help);
            route_connection.Close();

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection train_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_train = new MySqlCommand("select id_train from tickets where idtickets = @id_ticket;", train_connection);
            command_train.Parameters.Add("@id_ticket", MySqlDbType.VarChar).Value = tickets.id_ticket;
            train_connection.Open();

            help = command_train.ExecuteScalar().ToString();
            id_train = Int32.Parse(help);
            train_connection.Close();

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection wagon_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_wagon = new MySqlCommand("select id_wagon from tickets where idtickets = @id_ticket;", wagon_connection);
            command_wagon.Parameters.Add("@id_ticket", MySqlDbType.VarChar).Value = tickets.id_ticket;
            wagon_connection.Open();

            help = command_wagon.ExecuteScalar().ToString();
            id_wagon = Int32.Parse(help);
            wagon_connection.Close();

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection departure_time_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_departure_time = new MySqlCommand("select departure_time from tickets where idtickets = @id_ticket;", departure_time_connection);
            command_departure_time.Parameters.Add("@id_ticket", MySqlDbType.VarChar).Value = tickets.id_ticket;
            departure_time_connection.Open();

            departure_time = command_departure_time.ExecuteScalar().ToString();
            departure_time_connection.Close();

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection arrival_time_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_arrival_time = new MySqlCommand("select arrival_time from tickets where idtickets = @id_ticket;", arrival_time_connection);
            command_arrival_time.Parameters.Add("@id_ticket", MySqlDbType.VarChar).Value = tickets.id_ticket;
            arrival_time_connection.Open();

            arrival_time = command_arrival_time.ExecuteScalar().ToString();
            departure_time_connection.Close();

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            comboBox_passenger.SelectedValue = id_passenger;
            comboBox_train.SelectedValue = id_train;
            comboBox_wagon.SelectedValue = id_wagon;
            comboBox_route.SelectedValue = id_route;

            date = DateTime.Parse(departure_time);
            date.GetDateTimeFormats('u');
            departure_time = date.ToString("yyyy-MM-dd HH:mm:ss");

            date = DateTime.Parse(arrival_time);
            date.GetDateTimeFormats('u');
            arrival_time = date.ToString("yyyy-MM-dd HH:mm:ss");

            textBox_departure_time.Text = departure_time;
            textBox_arrival_time.Text = arrival_time;

            label_id.Text = tickets.id_ticket.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox_departure_time.Text == "" || textBox_arrival_time.Text == "")
            {
                MessageBox.Show("1.Введите время отправления.\r\n" +
                        "2.Введите время прибытия.\r\n", "Несоответствие форме изменения записи");
            }
            else
            {
                MySqlCommand cmd = new MySqlCommand("update tickets set id_passenger = @id_passenger, id_train = @id_train, id_route = @id_route, id_wagon = @id_wagon, departure_time = @departure_time, arrival_time = @arrival_time where idtickets = @id_ticket;", db.getConnection());
                cmd.Parameters.Add("@id_passenger", MySqlDbType.VarChar).Value = comboBox_passenger.SelectedValue;
                cmd.Parameters.Add("@id_train", MySqlDbType.VarChar).Value = comboBox_train.SelectedValue;
                cmd.Parameters.Add("@id_route", MySqlDbType.VarChar).Value = comboBox_route.SelectedValue;
                cmd.Parameters.Add("@id_wagon", MySqlDbType.VarChar).Value = comboBox_wagon.SelectedValue;
                cmd.Parameters.Add("@departure_time", MySqlDbType.VarChar).Value = textBox_departure_time.Text;
                cmd.Parameters.Add("@arrival_time", MySqlDbType.VarChar).Value = textBox_arrival_time.Text;
                cmd.Parameters.Add("@id_ticket", MySqlDbType.VarChar).Value = tickets.id_ticket;

                db.openConnection();
                if (textBox_departure_time.Text == "" || textBox_arrival_time.Text == "")
                {
                    MessageBox.Show("1.Введите время отправления.\r\n" +
                        "2.Введите время прибытия.\r\n", "Несоответствие форме изменения записи");
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
    }
}
