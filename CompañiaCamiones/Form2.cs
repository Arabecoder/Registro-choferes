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
    public partial class Form2 : Form
    {
        SqlConnection conexion = new SqlConnection("Data Source=DESKTOP-245O333\\MSSQLSERVER01; Initial Catalog=Choferes; Integrated Security=true;");
        SqlConnection conexion2 = new SqlConnection("Data Source=DESKTOP-245O333\\MSSQLSERVER01; Initial Catalog=Choferes; Integrated Security=true;");
        SqlCommand comando;
        Infochoferes choferes = new Infochoferes();
        public Form2()
        {
            InitializeComponent();
        }

        private void registroCamionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Registrocamiones ff = new Registrocamiones();
            ff.Show();
        }

        private void registroCiudadesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Registrociudades ff = new Registrociudades();
            ff.Show();
        }

        public void Guardar()
        {
            try
            {
                //Abrir conexion
                conexion.Open();

                //Crear comando
                comando = new SqlCommand($"insert into Choferesruta values('{choferes.nombre}', '{choferes.cedula}', '{choferes.camion}', '{choferes.ciudad}')", conexion);

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

        public void Leer()
        {
            //Abrir conexion
            conexion.Open();

            //Crear comando
            comando = new SqlCommand($"select * from Choferesruta", conexion);

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

        private void button1_Click(object sender, EventArgs e)
        {
            choferes.nombre = txtNombre.Text;
            choferes.cedula = txtCedula.Text;
            choferes.camion = combocamion.Text;
            choferes.ciudad = combociudad.Text;
            Guardar();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string borrar = "DELETE FROM Choferesruta where Nombre=@nombre";
            conexion.Open();
            SqlCommand comando = new SqlCommand(borrar, conexion);
            comando.Parameters.AddWithValue("@nombre", txtNombre.Text);
            comando.ExecuteNonQuery();
            conexion.Close();
            MessageBox.Show("Chofer eliminado");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlCommand comando = new SqlCommand("SELECT * FROM Choferesruta WHERE ID=@ID", conexion);
            comando.Parameters.AddWithValue("@ID", txtID_choferes.Text);
            conexion.Open();
            SqlDataReader reader = comando.ExecuteReader();
            if (reader.Read())
            {
                txtNombre.Text = reader["Nombre"].ToString();
                txtCedula.Text = reader["Cedula"].ToString();
                combocamion.Text = reader["Camion"].ToString();
                combociudad.Text = reader["Ciudad"].ToString();
            }
            conexion.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string cambio = "UPDATE Choferesruta SET Nombre=@nombre , Cedula=@cedula , Camion=@camion , Ciudad=@ciudad WHERE ID=@ID";
            conexion.Open();
            SqlCommand comando = new SqlCommand(cambio, conexion);
            comando.Parameters.AddWithValue("@nombre", txtNombre);
            comando.Parameters.AddWithValue("@cedula", txtCedula);
            comando.Parameters.AddWithValue("@camion", combocamion);
            comando.Parameters.AddWithValue("@ciudad", combociudad);
            comando.Parameters.AddWithValue("@ID", txtID_choferes);
            comando.ExecuteNonQuery();
            conexion.Close();
            MessageBox.Show("Informacion Actualizada...");
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            SqlCommand comando = new SqlCommand("SELECT Placa FROM Registrarcamion", conexion);
            conexion.Open();
            SqlDataReader registro = comando.ExecuteReader();
            while (registro.Read())
            {
                combocamion.Items.Add(registro["Placa"].ToString());
            }
            conexion.Close();


            SqlCommand comando2 = new SqlCommand("SELECT Ciudad FROM Registrarciudad", conexion2);
            conexion2.Open();
            SqlDataReader registro2 = comando2.ExecuteReader();
            while (registro2.Read())
            {
                combociudad.Items.Add(registro2["Ciudad"].ToString());
            }
            conexion2.Close();
        }

        private void combocamion_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
