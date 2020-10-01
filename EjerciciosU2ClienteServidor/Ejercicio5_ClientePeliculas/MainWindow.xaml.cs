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
using System.Windows.Threading;

namespace Ejercicio5_ClientePeliculas
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DatosPelicula datos = new DatosPelicula();
        PeliculasClient cliente = new PeliculasClient();
        private int time = 0;
        private DispatcherTimer Timer;
        public MainWindow()
        {
            InitializeComponent();
            cmbClasificacion.ItemsSource = Enum.GetValues(typeof(Clasificacion));
            cmbSala.ItemsSource = Enum.GetValues(typeof(Sala));
            cmbIdioma.ItemsSource = Enum.GetValues(typeof(Idioma));
            this.DataContext = datos;
            cliente.AlHaberCambios += Cliente_AlHaberCambios;
            cliente.Get();


            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 5);
            Timer.Tick += Timer_Tick;
            Timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (time >= 0)
            {
                cliente.Get();
                time++;
            }
        }

        private void Cliente_AlHaberCambios()
        {
            dtgPeliculas.ItemsSource = cliente.Model;
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cliente.Agregar(datos);
                txtHora.Text = txtTitulo.Text = "";
                cmbClasificacion.SelectedIndex = cmbSala.SelectedIndex = cmbIdioma.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dtgPeliculas.SelectedIndex != -1)
                {
                     datos = dtgPeliculas.SelectedItem as DatosPelicula;

                    if (MessageBox.Show($"¿Desea eliminar la pelicula {datos.Nombre} con función a las {datos.Hora}?", "Confirme", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        cliente.Eliminar(datos);
                        MessageBox.Show("Pelicula eliminada de cartelera", "Vuelo Eliminado", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("La pelicula no se eliminó de la cartelera");
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione una pelicula para eliminar de cartelera", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

                if(dtgPeliculas.SelectedIndex != -1)
                {
                    EditarWindow ventanaEdit = new EditarWindow();
                    datos = dtgPeliculas.SelectedItem as DatosPelicula;
                    ventanaEdit.DataContext = datos;
                    ventanaEdit.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Seleccione una pelicula para editar", "Atecion", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }

                cliente.Editar(datos);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
