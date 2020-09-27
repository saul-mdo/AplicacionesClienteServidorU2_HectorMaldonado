using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ejercicio4_ClienteVuelos
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DatosVuelo datos = new DatosVuelo();
        ClienteVuelos cv = new ClienteVuelos();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = datos;
            cv.Get();
            cv.AlHaberCambios += Cv_AlHaberCambios;
        }

        private void Cv_AlHaberCambios()
        {
            gridLista.ItemsSource = cv.Model;
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cv.Agregar(datos);
                txtDestino.Clear();
                txtHota.Clear();
                txtVuelo.Clear();
                cv.Get();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cv.Eliminar(datos);
                cv.Get();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cv.Editar(datos);
                cv.Get();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
