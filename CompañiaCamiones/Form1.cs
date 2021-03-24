using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CompañiaCamiones
{
    public partial class Form1 : Form
    {
        SqlConnection conexion = new SqlConnection("Data Source=DESKTOP-245O333\\MSSQLSERVER01; Initial Catalog=Choferes; Integrated Security=true;");
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void Logins()
        {
            try
            {
                using (SqlCommand comando = new SqlCommand("SELECT Username, Passwords FROM Users WHERE Username='" + txtUser.Text + "' AND Passwords='" + txtPass.Text + "'", conexion))
                {
                    conexion.Open();
                    SqlDataReader reader = comando.ExecuteReader();
                    if (reader.Read())
                    {
                        MessageBox.Show("Bienvenido señor Oliver :D ");
                        Form2 ff = new Form2();
                        ff.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("informacion incorrecta, no eres el señor Oliver, el no se equivoca...");
                    }
                    conexion.Close();
                }
            }
            catch(Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }

        private void btnacceder_Click(object sender, EventArgs e)
        {
            Logins();
        }
    }
}
