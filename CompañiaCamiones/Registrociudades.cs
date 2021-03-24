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
    public partial class Registrociudades : Form
    {
        SqlConnection conexion = new SqlConnection("Data Source=DESKTOP-245O333\\MSSQLSERVER01; Initial Catalog=Choferes; Integrated Security=true;");
        SqlCommand comando;
        Infociudades ciudades = new Infociudades();
        public Registrociudades()
        {
            InitializeComponent();
        }

        private void Registrociudades_Load(object sender, EventArgs e)
        {

        }
        public void Guardar()
        {
            try
            {
                //Abrir conexion
                conexion.Open();

                //Crear comando
                comando = new SqlCommand($"insert into Registrarciudad values('{ciudades.ciudad}')", conexion);

                //Ejecutar el comando
                comando.ExecuteNonQuery();

                //Cerrar la conexion
                conexion.Close();

                MessageBox.Show("Datos guardados correctamente...");
            }
            catch (Exception error)
            {
                MessageBox.Show("Es tan dificil completar la informacion?...");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ciudades.ciudad = txtCiudad.Text;
            Guardar();
        }

        public void Leer()
        {
            //Abrir conexion
            conexion.Open();

            //Crear comando
            comando = new SqlCommand($"select * from Registrarciudad", conexion);

            //Ejecutar el comando
            comando.ExecuteNonQuery();

            DataTable tabla = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(comando);
            adapter.Fill(tabla);
            dataGridView1.DataSource = tabla;

            //Cerrar la conexion
            conexion.Close();
        }

        private void btnacceder_Click(object sender, EventArgs e)
        {
            Leer();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string borrar = "DELETE FROM Registrarciudad where Ciudad=@ciudad";
            conexion.Open();
            SqlCommand comando = new SqlCommand(borrar, conexion);
            comando.Parameters.AddWithValue("@ciudad", txtCiudad.Text);
            comando.ExecuteNonQuery();
            conexion.Close();
            MessageBox.Show("Ciudad eliminada D: ");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string cambio = "UPDATE Registrarciudad SET Ciudad=@ciudad WHERE ID_ciudad=@ID_ciudad";
            conexion.Open();
            SqlCommand comando = new SqlCommand(cambio, conexion);
            comando.Parameters.AddWithValue("@ciudad", txtCiudad.Text);
            comando.Parameters.AddWithValue("@ID_ciudad", txtID_ciudad.Text);
            comando.ExecuteNonQuery();
            conexion.Close();
            MessageBox.Show("Informacion Actualizada...");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlCommand comando = new SqlCommand("SELECT * FROM Registrarciudad WHERE ID_ciudad=@ID_ciudad", conexion);
            comando.Parameters.AddWithValue("@ID_ciudad", txtID_ciudad.Text);
            conexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            if (reader.Read())
            {
                txtCiudad.Text = reader["Ciudad"].ToString();
            }
            conexion.Close();
        }
    }
}
