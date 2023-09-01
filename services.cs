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
    public partial class services : Form
    {
        Database db = new Database();
        string myConnectionString = "Database=railway_station;Data Source=127.0.0.1;User Id=root;Password=1337";
        String cost;
        public services()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void services_Load(object sender, EventArgs e)
        {
            DataTable services = new DataTable();
            MySqlConnection connection_services = new MySqlConnection(myConnectionString);
            {
                MySqlCommand command = new MySqlCommand("select idservices, name from services", connection_services);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(services);
            }
            listBox1.DataSource = services;
            listBox1.DisplayMember = "name";
            listBox1.ValueMember = "idservices";

            listBox1.ClearSelected();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedValue != null)
            {
                label_service_cost.Text = "";
                label_service_name.Text = "";

                MySqlCommand command = new MySqlCommand("insert into tickets_to_services (id_ticket, id_service, time_of_purchase) values (@id_ticket, @id_service, @time_of_purchase)", db.getConnection());
                command.Parameters.Add("@id_ticket", MySqlDbType.Int32).Value = passengers.max_id_ticket;
                command.Parameters.Add("@id_service", MySqlDbType.Int32).Value = (int)listBox1.SelectedValue;
                command.Parameters.Add("@time_of_purchase", MySqlDbType.DateTime).Value = DateTime.Now;


                db.openConnection();
                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Услуга успешно добавлена.", "Добавление услуги...", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    db.closeConnection();
                }
                else
                {
                    MessageBox.Show("Произошла ошибка при добавлении услуги.", "Ошибка при добавлении данных...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                listBox1.ClearSelected();
            }
            else
            MessageBox.Show("Ни одна услуга не выбрана.");
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedValue != null)
            {
                label_service_name.Text = listBox1.Text;

                MySqlConnection service_cost_connection = new MySqlConnection(myConnectionString);
                MySqlCommand command_service_cost = new MySqlCommand("select cost from services where idservices = @id_service", service_cost_connection);
                command_service_cost.Parameters.Add("@id_service", MySqlDbType.VarChar).Value = listBox1.SelectedValue;
                service_cost_connection.Open();

                cost = command_service_cost.ExecuteScalar().ToString();
                label_service_cost.Text = cost + " ₽";
                service_cost_connection.Close();
            }
            else
                MessageBox.Show("Ни одна услуга не выбрана.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                if (Application.OpenForms[i].Name != "main")
                {
                    Application.OpenForms[i].Close();
                }
            }
        }
    }
}
