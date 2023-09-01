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
    public partial class passengers : Form
    {
        Database db = new Database();

        public static int max_id_passenger, id_gender, max_id_ticket;
        public static String name, surname, patronymic, date_of_birth, passport_series, passport_number, phone, email;

        private void textBox_phone_MouseLeave(object sender, EventArgs e)
        {
            if (textBox_phone.Text == "" || textBox_phone.Text == "+7 XXX XXX XX XX")
                textBox_phone.ForeColor = Color.Gray;
        }

        private void textBox_email_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && !Char.IsLetter(number) && number != 45 && number != 46 && number != 64)
            {
                e.Handled = true;
            }
        }

        private void textBox_phone_MouseHover(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(textBox_phone, "Минимальная длина номера телефона 11 цифр");
        }

        private void textBox_surname_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (number != 8 && !Char.IsLetter(number) && number != 32)
            {
                e.Handled = true;
            }
        }

        private void textBox_name_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (number != 8 && !Char.IsLetter(number) && number != 32)
            {
                e.Handled = true;
            }
        }

        private void textBox_patronymic_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (number != 8 && !Char.IsLetter(number) && number != 32)
            {
                e.Handled = true;
            }
        }

        private void textBox_pass_series_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox_pass_number_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void maskedTextBox_birthday_TypeValidationCompleted(object sender, TypeValidationEventArgs e)
        {

        }

        private void textBox_phone_MouseEnter(object sender, EventArgs e)
        {
            textBox_phone.ForeColor = Color.DarkSlateGray;
        }

        private void textBox_phone_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox_phone_Leave(object sender, EventArgs e)
        {
            if (textBox_phone.Text == "")
            {
                textBox_phone.Text = "+7 XXX XXX XX XX";
                textBox_phone.ForeColor = Color.DimGray;
            }
        }

        private void textBox_phone_Enter(object sender, EventArgs e)
        {
            if (textBox_phone.Text == "+7 XXX XXX XX XX")
                textBox_phone.Text = "";
        }

        string myConnectionString = "Database=railway_station;Data Source=127.0.0.1;User Id=root;Password=1337";
        public passengers()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            textBox_phone.Text = "+7 XXX XXX XX XX";
            textBox_phone.ForeColor = Color.DimGray;

            comboBox_gender.DropDownStyle = ComboBoxStyle.DropDownList;


            DataTable table_genders = new DataTable();

            MySqlConnection connection_genders = new MySqlConnection(myConnectionString);
            {
                MySqlCommand command = new MySqlCommand("select idgenders, name from genders", connection_genders);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(table_genders);
            }
            comboBox_gender.DataSource = table_genders;
            comboBox_gender.DisplayMember = "name";
            comboBox_gender.ValueMember = "idgenders";
        }

        private void passengers_Load(object sender, EventArgs e)
        {
            textBox_phone.MaxLength = 11;
            textBox_name.MaxLength = 100;
            textBox_surname.MaxLength = 100;
            textBox_patronymic.MaxLength = 100;
            maskedTextBox_birthday.MaxLength = 10;
            textBox_pass_series.MaxLength = 4;
            textBox_pass_number.MaxLength = 6;
            textBox_email.MaxLength = 100;

            maskedTextBox_birthday.Mask = "0000-00-00";
            maskedTextBox_birthday.ValidatingType = typeof(DateTime);
        }

        private void button_finding_seats_Click(object sender, EventArgs e)
        {

            ///////////////////////////////////////////////
            String passengers_count;
            ///////////////////////////////////////////////
            name = textBox_name.Text;
            surname = textBox_surname.Text;
            patronymic = textBox_patronymic.Text;
            id_gender = (int)comboBox_gender.SelectedValue;
            date_of_birth = maskedTextBox_birthday.Text;
            passport_series = textBox_pass_series.Text;
            passport_number = textBox_pass_number.Text;
            phone = textBox_phone.Text;
            email = textBox_email.Text;




            MySqlCommand cmd = new MySqlCommand("insert into passengers (name, surname, patronymic, id_gender, date_of_birth, passport_series, passport_number, phone, email) values\r\n(@name, @surname, @patronymic, @id_gender, @date_of_birth, @passport_series, @passport_number, @phone, @email);", db.getConnection());
            cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = name;
            cmd.Parameters.Add("@surname", MySqlDbType.VarChar).Value = surname;
            cmd.Parameters.Add("@patronymic", MySqlDbType.VarChar).Value = patronymic;
            cmd.Parameters.Add("@id_gender", MySqlDbType.VarChar).Value = id_gender;
            cmd.Parameters.Add("@date_of_birth", MySqlDbType.VarChar).Value = date_of_birth;
            cmd.Parameters.Add("@passport_series", MySqlDbType.VarChar).Value = passport_series;
            cmd.Parameters.Add("@passport_number", MySqlDbType.VarChar).Value = passport_number;
            cmd.Parameters.Add("@phone", MySqlDbType.VarChar).Value = phone;
            cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = email;


            // --------------------------------------------------------------------------------------- //

            db.openConnection();
            if (textBox_name.TextLength < 1 || textBox_surname.TextLength < 1 || textBox_patronymic.TextLength < 1 || maskedTextBox_birthday.TextLength < 10 || textBox_pass_series.TextLength < 4 || textBox_pass_number.TextLength < 6 || textBox_phone.TextLength < 11 || textBox_email.TextLength < 9 || textBox_phone.Text == "+7 XXX XXX XX XX" ||  maskedTextBox_birthday.MaskCompleted == false)
            {
                MessageBox.Show("1.Укажите фамилию пассажира.\r\n" +
                    "2.Укажите имя пассажира.\r\n" +
                    "3.Укажите отчество пассажира.\r\nДругие ошибки выделены ниже:\r\n\n" +
                    "4.Введите существующий адрес эл.почты\r\n" +
                    "5.Укажите дату в формате ГГГГ.ММ.ДД,\nнапример,1990-12-25\r\n" +
                    "6.Проверьте серию и номер паспорта\r\n" +
                    "7.Укажите номер телефона\r\n" +
                    "8.Укажите электронную почту", "Ошибка при добавлении данных\r\n");
            }
            else if (cmd.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Данные были успешно добавлены.");


                MySqlConnection passengers_connection = new MySqlConnection(myConnectionString);
                MySqlCommand command_max = new MySqlCommand("select max(idpassengers) from passengers;", passengers_connection);
                passengers_connection.Open();

                passengers_count = command_max.ExecuteScalar().ToString();
                max_id_passenger = Convert.ToInt32(passengers_count);
                passengers_connection.Close();


                db.closeConnection();

                data_verification frm1 = new data_verification();
                this.Hide();
                frm1.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Ошибка при добавлении данных пассажира.", "Ошибка при добавлении...");
            }
        }
    }
}
