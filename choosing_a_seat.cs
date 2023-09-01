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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace railway_station
{
    public partial class choosing_a_seat : Form
    {
        Database db = new Database();
        string myConnectionString = "Database=railway_station;Data Source=127.0.0.1;User Id=root;Password=1337";

        String route;

        public choosing_a_seat()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            DataTable wagons = new DataTable();
            MySqlConnection connection_wagons = new MySqlConnection(myConnectionString);
            {
                MySqlCommand command = new MySqlCommand("select idwagons, name from wagons", connection_wagons);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(wagons);
            }
            comboBox_wagons.DataSource = wagons;
            comboBox_wagons.DisplayMember = "name";
            comboBox_wagons.ValueMember = "idwagons";

            comboBox_wagons.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void ticket_selection_Load(object sender, EventArgs e)
        {

            if(main.id_wagon == 0)
            {
                label_seat.Visible = false;
                label_wagon_and_seat.Visible = true;
            }
            else
            {
                label_wagon_and_seat.Visible = false;
                label_seat.Visible = true;
                comboBox_wagons.SelectedValue = main.id_wagon;
            }


            MySqlConnection ticket_cost_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_ticket_cost = new MySqlCommand("select cost from wagons where idwagons = @idwagon", ticket_cost_connection);
            command_ticket_cost.Parameters.Add("@idwagon", MySqlDbType.VarChar).Value = comboBox_wagons.SelectedValue;
            ticket_cost_connection.Open();

            main.ticket_cost = command_ticket_cost.ExecuteScalar().ToString();
            label_cost.Text = main.ticket_cost + " ₽";
            ticket_cost_connection.Close();

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection route_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_route = new MySqlCommand("select idroutes from routes \r\nwhere id_city_from = @id_city_from and id_city_to = @id_city_to", route_connection);
            command_route.Parameters.Add("@id_city_from", MySqlDbType.VarChar).Value = main.id_city_from;
            command_route.Parameters.Add("@id_city_to", MySqlDbType.VarChar).Value = main.id_city_to;
            route_connection.Open();

            route = command_route.ExecuteScalar().ToString();
            main.id_route = Convert.ToInt32(route);
            route_connection.Close();

            main.seat_number = -1;
        }

        private void button_finding_seats_Click(object sender, EventArgs e)
        {

            if (radioButton_1.Checked)
            {
                main.seat_number = 1;
            }
            else if (radioButton_2.Checked)
            {
                main.seat_number = 2;
            }
            else if (radioButton_3.Checked)
            {
                main.seat_number = 3;
            }
            else if (radioButton_4.Checked)
            {
                main.seat_number = 4;
            }
            else if (radioButton_5.Checked)
            {
                main.seat_number = 5;
            }
            else if (radioButton_6.Checked)
            {
                main.seat_number = 6;
            }
            else if (radioButton_7.Checked)
            {
                main.seat_number = 7;
            }
            else if (radioButton_8.Checked)
            {
                main.seat_number = 8;
            }
            else if (radioButton_9.Checked)
            {
                main.seat_number = 9;
            }
            else if (radioButton_10.Checked)
            {
                main.seat_number = 10;
            }
            else if (radioButton_11.Checked)
            {
                main.seat_number = 11;
            }
            else if (radioButton_12.Checked)
            {
                main.seat_number = 12;
            }
            else if (radioButton_13.Checked)
            {
                main.seat_number = 13;
            }
            else if (radioButton_14.Checked)
            {
                main.seat_number = 14;
            }
            else if (radioButton_15.Checked)
            {
                main.seat_number = 15;
            }
            else if (radioButton_16.Checked)
            {
                main.seat_number = 16;
            }
            else if (radioButton_17.Checked)
            {
                main.seat_number = 17;
            }
            else if (radioButton_18.Checked)
            {
                main.seat_number = 18;
            }
            else if (radioButton_19.Checked)
            {
                main.seat_number = 19;
            }
            else if (radioButton_20.Checked)
            {
                main.seat_number = 20;
            }
            else if (radioButton_21.Checked)
            {
                main.seat_number = 21;
            }
            else if (radioButton_22.Checked)
            {
                main.seat_number = 22;
            }
            else if (radioButton_23.Checked)
            {
                main.seat_number = 23;
            }
            else if (radioButton_24.Checked)
            {
                main.seat_number = 24;
            }

            if (check_records())
            {
                return;
            }
            else if (main.seat_number == -1)
            {
                MessageBox.Show("Выберите посадочное место.");
            }
            else
            {
                main.id_wagon = (int)comboBox_wagons.SelectedValue;

                passengers frm1 = new passengers();
                this.Hide();
                frm1.ShowDialog();
                this.Show();

            }
        }

        private void comboBox_wagons_SelectionChangeCommitted(object sender, EventArgs e)
        {
            MySqlConnection ticket_cost_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_ticket_cost = new MySqlCommand("select cost from wagons where idwagons = @idwagon", ticket_cost_connection);
            command_ticket_cost.Parameters.Add("@idwagon", MySqlDbType.VarChar).Value = comboBox_wagons.SelectedValue;
            ticket_cost_connection.Open();

            main.ticket_cost = command_ticket_cost.ExecuteScalar().ToString();
            label_cost.Text = main.ticket_cost + " ₽";
            ticket_cost_connection.Close();

        }


        private Boolean check_records()
        {
            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("select * from taken_seats\r\nwhere id_route = @id_route and id_train = @id_train and id_wagon = @id_wagon and seat_number = @seat_number", db.getConnection());
            command.Parameters.Add("@id_route", MySqlDbType.Int32).Value = main.id_route;
            command.Parameters.Add("@id_train", MySqlDbType.Int32).Value = main.id_train;
            command.Parameters.Add("@id_wagon", MySqlDbType.Int32).Value = comboBox_wagons.SelectedValue;
            command.Parameters.Add("@seat_number", MySqlDbType.Int32).Value = main.seat_number;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Это место уже продано.");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
