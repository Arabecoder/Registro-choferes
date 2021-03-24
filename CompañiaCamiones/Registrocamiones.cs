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
    public partial class Registrocamiones : Form
    {
        SqlConnection conexion = new SqlConnection("Data Source=DESKTOP-245O333\\MSSQLSERVER01; Initial Catalog=Choferes; Integrated Security=true;");
        SqlCommand comando;
        Infocamiones camiones = new Infocamiones();
        public Registrocamiones()
        {
            InitializeComponent();
        }

        private void Registrocamiones_Load(object sender, EventArgs e)
        {

        }

        private void btnacceder_Click(object sender, EventArgs e)
        {
            Leer();
        }

        public void Leer()
        {
            //Abrir conexion
            conexion.Open();

            //Crear comando
            comando = new SqlCommand($"select * from Registrarcamion", conexion);

            //Ejecutar el comando
            comando.ExecuteNonQuery();

            DataTable tabla = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(comando);
            adapter.Fill(tabla);
            dataGridView1.DataSource = tabla;

            //Cerrar la conexion
            conexion.Close();
        }

        public void Guardar()
        {
            try
            {
                //Abrir conexion
                conexion.Open();

                //Crear comando
                comando = new SqlCommand($"insert into Registrarcamion values('{camiones.placa}', '{camiones.marca}', '{camiones.modelo}', '{camiones.año}')", conexion);

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
            camiones.placa = int.Parse(txtPlaca.Text);
            camiones.marca = txtMarca.Text;
            camiones.modelo = txtModelo.Text;
            camiones.año = int.Parse(txtAño.Text);
            Guardar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string cambio = "UPDATE Registrarcamion SET Placa=@placa , Marca=@marca , Modelo=@modelo , Año=@año WHERE ID_camion=@ID_camion";
            conexion.Open();
            SqlCommand comando = new SqlCommand(cambio, conexion);
            comando.Parameters.AddWithValue("@placa", txtPlaca.Text);
            comando.Parameters.AddWithValue("@marca", txtMarca.Text);
            comando.Parameters.AddWithValue("@modelo", txtModelo.Text);
            comando.Parameters.AddWithValue("@año", txtAño.Text);
            comando.Parameters.AddWithValue("@ID_camion", txtID_camion.Text);
            comando.ExecuteNonQuery();
            conexion.Close();
            MessageBox.Show("Informacion Actualizada...");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string borrar = "DELETE FROM Registrarcamion where Placa=@placa";
            conexion.Open();
            SqlCommand comando = new SqlCommand(borrar, conexion);
            comando.Parameters.AddWithValue("@placa", txtPlaca.Text);
            comando.ExecuteNonQuery();
            conexion.Close();
            MessageBox.Show("Chofer eliminado");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlCommand comando = new SqlCommand("SELECT * FROM Registrarcamion WHERE ID_camion=@ID_camion", conexion);
            comando.Parameters.AddWithValue("@ID_camion", txtID_camion.Text);
            conexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            if (reader.Read())
            {
                txtPlaca.Text = reader["Placa"].ToString();
                txtMarca.Text = reader["Marca"].ToString();
                txtModelo.Text = reader["Modelo"].ToString();
                txtAño.Text = reader["Año"].ToString();
            }
            conexion.Close();
        }
    }
}
