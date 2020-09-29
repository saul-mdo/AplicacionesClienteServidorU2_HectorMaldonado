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
        ClienteVuelos cv = new ClienteVuelos();
        DatosVuelo datos = new DatosVuelo();

        public MainWindow()
        {
            InitializeComponent();
            cv.Get();
            this.DataContext = datos;
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
                MessageBox.Show("Vuelo agregado con exito");
                txtDestino.Clear();
                txtHora.Clear();
                txtVuelo.Clear();
                cmbEdo.Text = "";
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
                if (gridLista.SelectedIndex >= 0)
                {
                    datos = gridLista.SelectedItem as DatosVuelo;
                    DatosVuelo v = gridLista.SelectedItem as DatosVuelo;

                    if (MessageBox.Show($"¿Desea eliminar el vuelo {datos.Vuelo} con destino a {datos.Destino} cuya salida es a las {datos.Hora}?", "Confirme", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        cv.Eliminar(datos);
                        MessageBox.Show("Vuelo eliminado con exito", "Vuelo Eliminado", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("El vuelo no se eliminó");
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione un vuelo para eliminar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
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
                if (gridLista.SelectedIndex >= 0)
                {
                    EditarWindow ventanaEditar = new EditarWindow();
                    DatosVuelo ve = gridLista.SelectedItem as DatosVuelo;
                    ventanaEditar.DataContext = ve;
                    ventanaEditar.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Seleccione un vuelo para poder editar");
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
       
    }
}
