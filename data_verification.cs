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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Xml.Linq;

namespace railway_station
{
    public partial class data_verification : Form
    {
        Database db = new Database();
        string myConnectionString = "Database=railway_station;Data Source=127.0.0.1;User Id=root;Password=1337";
        String city_to, city_from_to, wagon_name;

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button_finding_seats_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tickets (id_passenger, id_train, id_route, id_wagon, departure_time, arrival_time) \r\nvalues (@id_passenger, @id_train, @id_route, @id_wagon, @departure_time, @arrival_time);", db.getConnection());
            cmd.Parameters.Add("@id_passenger", MySqlDbType.VarChar).Value = passengers.max_id_passenger;
            cmd.Parameters.Add("@id_train", MySqlDbType.VarChar).Value = main.id_train;
            cmd.Parameters.Add("@id_route", MySqlDbType.VarChar).Value = main.id_route;
            cmd.Parameters.Add("@id_wagon", MySqlDbType.VarChar).Value = main.id_wagon;
            cmd.Parameters.Add("@departure_time", MySqlDbType.VarChar).Value = main.departure_time;
            cmd.Parameters.Add("@arrival_time", MySqlDbType.VarChar).Value = main.arrival_time;


            // --------------------------------------------------------------------------------------- //

            db.openConnection();

            if (cmd.ExecuteNonQuery() == 1)
            {
                String ticket_id;

                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();

                MySqlConnection tickets_connection = new MySqlConnection(myConnectionString);
                MySqlCommand command_max = new MySqlCommand("select max(idtickets) from tickets", tickets_connection);
                tickets_connection.Open();

                ticket_id = command_max.ExecuteScalar().ToString();
                passengers.max_id_ticket = Convert.ToInt32(ticket_id);

                tickets_connection.Close();

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                MySqlCommand cmd_2 = new MySqlCommand("insert into taken_seats (id_route, id_train, id_wagon, seat_number) values\r\n(@id_route, @id_train, @id_wagon, @seat_number);", db.getConnection());
                cmd_2.Parameters.Add("@id_route", MySqlDbType.VarChar).Value = main.id_route;
                cmd_2.Parameters.Add("@id_train", MySqlDbType.VarChar).Value = main.id_train;
                cmd_2.Parameters.Add("@id_wagon", MySqlDbType.VarChar).Value = main.id_wagon;
                cmd_2.Parameters.Add("@seat_number", MySqlDbType.VarChar).Value = main.seat_number;

                adapter.SelectCommand = cmd_2;
                adapter.Fill(table);

                db.closeConnection();

                MessageBox.Show("Пассажир:  " + passengers.surname + " " + passengers.name + " " + passengers.patronymic + "\r\n" +
                "Паспортные данные:  " + passengers.passport_series + " " + passengers.passport_number + "\r\n" +
                "Поезд:  " + city_from_to +", "+  main.train_name +"\r\n" +
                "Место:  " + wagon_name + ", " + main.seat_number.ToString() + " место" + "\r\n" +
                "Отправление:  " + main.station_name + ", " + main.departure_time + "\r\n" +
                "Прибытие:  " + city_to + ", " + main.arrival_time + "\r\n\n" +
                "Ваш билет под номером " + passengers.max_id_ticket + " успешно забронирован.\t\t", "Добавление данных завершено...");

                services frm1 = new services();
                this.Hide();
                frm1.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Ошибка при бронировании билета.", "Ошибка при добавлении данных...");
            }
        }

        public data_verification()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            /*String date = "2023-08-24";
            String time = "02:36:00";*/

            main.departure_time = main.date_string + " " + main.time;

            String hours, minutes;

            DateTime Date = DateTime.Parse(main.departure_time);


            MySqlConnection travel_hours_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_travel_hours = new MySqlCommand("select travel_time_hours from trains_to_routes\r\nwhere id_train = @id_train and id_route = @id_route;", travel_hours_connection);
            command_travel_hours.Parameters.Add("@id_train", MySqlDbType.VarChar).Value = main.id_train;
            command_travel_hours.Parameters.Add("@id_route", MySqlDbType.VarChar).Value = main.id_route;
            travel_hours_connection.Open();

            hours = command_travel_hours.ExecuteScalar().ToString();
            travel_hours_connection.Close();

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection travel_minutes_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_minutes_hours = new MySqlCommand("select travel_time_minutes from trains_to_routes\r\nwhere id_train = @id_train and id_route = @id_route;", travel_minutes_connection);
            command_minutes_hours.Parameters.Add("@id_train", MySqlDbType.VarChar).Value = main.id_train;
            command_minutes_hours.Parameters.Add("@id_route", MySqlDbType.VarChar).Value = main.id_route;
            travel_minutes_connection.Open();

            minutes = command_minutes_hours.ExecuteScalar().ToString();
            travel_minutes_connection.Close();

            Date = Date.AddHours(Convert.ToDouble(hours));
            Date = Date.AddMinutes(Convert.ToDouble(minutes));

            Date.GetDateTimeFormats('u');
            main.arrival_time = Date.ToString("yyyy-MM-dd HH:mm:ss");

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection city_from_to_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_city_from_to = new MySqlCommand("select concat(ct1.name,\" - \" , ct2.name) from routes\r\njoin cities ct1 on routes.id_city_from = ct1.idcities\r\njoin cities ct2 on routes.id_city_to = ct2.idcities\r\nwhere id_city_from = @id_city_from and id_city_to = @id_city_to", city_from_to_connection);
            command_city_from_to.Parameters.Add("@id_city_from", MySqlDbType.VarChar).Value = main.id_city_from;
            command_city_from_to.Parameters.Add("@id_city_to", MySqlDbType.VarChar).Value = main.id_city_to;
            city_from_to_connection.Open();

            city_from_to = command_city_from_to.ExecuteScalar().ToString();
            city_from_to_connection.Close();

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection city_from_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_city_from = new MySqlCommand("select name from cities\r\nwhere idcities = @id_city_to", city_from_connection);
            command_city_from.Parameters.Add("@id_city_to", MySqlDbType.VarChar).Value = main.id_city_to;
            city_from_connection.Open();

            city_to = command_city_from.ExecuteScalar().ToString();
            city_from_connection.Close();

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection wagon_name_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_wagon_name = new MySqlCommand("select name from wagons where idwagons = @id_wagon", wagon_name_connection);
            command_wagon_name.Parameters.Add("@id_wagon", MySqlDbType.VarChar).Value = main.id_wagon;
            wagon_name_connection.Open();

            wagon_name = command_wagon_name.ExecuteScalar().ToString();
            wagon_name_connection.Close();

        }

        private void services_Load(object sender, EventArgs e)
        {

            textBox_name.Text = passengers.name;
            textBox_surname.Text = passengers.surname;
            textBox_patronymic.Text = passengers.patronymic;
            textBox_birthday.Text = passengers.date_of_birth;
            textBox_passport.Text = passengers.passport_series + " " + passengers.passport_number;
            textBox_phone.Text = passengers.phone;
            textBox_email.Text = passengers.email;
            textBox_name.Text = passengers.name;
            textBox_departure_time.Text = main.departure_time;
            textBox_arrival_time.Text = main.arrival_time;
            textBox_train_name.Text = main.train_name;
            textBox_location.Text = wagon_name;
            textBox_station_name.Text = main.station_name;
            textBox_seat_number.Text = main.seat_number.ToString() + " место";
            textBox_city_to.Text = city_to;
            textBox_city_from_to.Text = city_from_to;


        }


    }
}
