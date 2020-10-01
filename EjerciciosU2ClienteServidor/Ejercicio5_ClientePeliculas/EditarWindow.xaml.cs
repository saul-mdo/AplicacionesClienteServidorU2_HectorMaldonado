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
using System.Windows.Shapes;

namespace Ejercicio5_ClientePeliculas
{
    /// <summary>
    /// Lógica de interacción para EditarWindow.xaml
    /// </summary>
    public partial class EditarWindow : Window
    {
        public EditarWindow()
        {
            InitializeComponent();
            cmbClasificacion.ItemsSource = Enum.GetValues(typeof(Clasificacion));
            cmbSala.ItemsSource = Enum.GetValues(typeof(Sala));
            cmbIdioma.ItemsSource = Enum.GetValues(typeof(Idioma));
        }
        PeliculasClient cliente = new PeliculasClient();
        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            DatosPelicula datos = this.DataContext as DatosPelicula;
            try
            {
                cliente.Editar(datos);
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}
