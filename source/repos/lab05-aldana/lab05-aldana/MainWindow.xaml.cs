using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Data.SqlClient;

namespace lab05_aldana
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary
    public partial class MainWindow : Window
    {
        SqlConnection connection = new SqlConnection("Server=DESKTOP-80ANVID\\SQLEXPRESS2017;Database=Neptuno;User ID=jhersin;Password=jhersin123;TrustServerCertificate=true;");

        public MainWindow()
        {
            InitializeComponent();
            CargarClientes();
        }

        private void InsertarCliente_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("sp_InsertarCliente", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdCliente", txtIdCliente.Text);
                command.Parameters.AddWithValue("@NombreCompañia", txtNombreCompania.Text);
                command.Parameters.AddWithValue("@NombreContacto", txtNombreContacto.Text);
                command.Parameters.AddWithValue("@CargoContacto", txtCargoContacto.Text);
                command.Parameters.AddWithValue("@Direccion", txtDireccion.Text);
                command.Parameters.AddWithValue("@Ciudad", txtCiudad.Text);
                command.Parameters.AddWithValue("@Region", txtRegion.Text);
                command.Parameters.AddWithValue("@Pais", txtPais.Text);
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Cliente insertado correctamente.");
                CargarClientes();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al insertar cliente: {ex.Message}");
                connection.Close();
            }
        }

        private void ModificarCliente_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("sp_ModificarCliente", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdCliente", txtIdCliente.Text);
                command.Parameters.AddWithValue("@NombreCompañia", txtNombreCompania.Text);
                command.Parameters.AddWithValue("@NombreContacto", txtNombreContacto.Text);
                command.Parameters.AddWithValue("@CargoContacto", txtCargoContacto.Text);
                command.Parameters.AddWithValue("@Direccion", txtDireccion.Text);
                command.Parameters.AddWithValue("@Ciudad", txtCiudad.Text);
                command.Parameters.AddWithValue("@Region", txtRegion.Text);
                command.Parameters.AddWithValue("@Pais", txtPais.Text);
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Cliente modificado correctamente.");
                CargarClientes();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar cliente: {ex.Message}");
                connection.Close();
            }
        }

        private void EliminarCliente_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdCliente.Text))
            {
                MessageBox.Show("Por favor, seleccione un cliente a eliminar.");
                return;
            }

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("sp_EliminarCliente", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdCliente", txtIdCliente.Text);
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Cliente eliminado correctamente.");
                CargarClientes();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar cliente: {ex.Message}");
                connection.Close();
            }
        }

        private void CargarClientes()
        {
            try
            {
                List<Cliente> clientes = new List<Cliente>();
                connection.Open();
                SqlCommand command = new SqlCommand("sp_ListarClientes", connection);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    clientes.Add(new Cliente
                    {
                        IdCliente = reader["idcliente"].ToString(),
                        NombreCompania = reader["nombrecompañia"].ToString(),
                        NombreContacto = reader["nombrecontacto"].ToString(),
                        CargoContacto = reader["cargocontacto"].ToString(),
                        Direccion = reader["direccion"].ToString(),
                        Ciudad = reader["ciudad"].ToString(),
                        Region = reader["region"].ToString(),
                        Pais = reader["pais"].ToString()
                    });
                }
                connection.Close();
                showClients.ItemsSource = clientes;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar clientes: {ex.Message}");
                connection.Close();
            }
        }



        private void LimpiarCampos()
        {
            txtIdCliente.Text = "";
            txtNombreCompania.Text = "";
            txtNombreContacto.Text = "";
            txtCargoContacto.Text = "";
            txtDireccion.Text = "";
            txtCiudad.Text = "";
            txtRegion.Text = "";
            txtPais.Text = "";
        }
    }
}