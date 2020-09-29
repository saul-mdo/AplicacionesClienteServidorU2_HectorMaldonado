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

namespace Ejercicio4_ClienteVuelos
{
    /// <summary>
    /// Lógica de interacción para EditarWindow.xaml
    /// </summary>
    public partial class EditarWindow : Window
    {
        public EditarWindow()
        {
            InitializeComponent();
        }

      
        ClienteVuelos c = new ClienteVuelos();
        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            DatosVuelo datos = this.DataContext as DatosVuelo;
            c.Editar(datos);
            this.Close();
            
        }
    }
}
