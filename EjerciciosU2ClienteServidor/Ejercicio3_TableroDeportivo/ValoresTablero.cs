using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio3_TableroDeportivo
{
    public class ValoresTablero : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnProperyChanged(string propiedad)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propiedad));
        }

        private string local;
        public string EquipoLocal
        {
            get { return local; }
            set { local = value;
                OnProperyChanged("EquipoLocal");
            }
        }

        private string visitante;
        public string EquipoVisitante
        {
            get { return visitante; }
            set
            {
                visitante = value;
                OnProperyChanged("EquipoVisitante");
            }
        }

        private int goleslocales;

        public int GolesEquipoLocal
        {
            get { return goleslocales; }
            set { goleslocales = value;
                OnProperyChanged("GolesEquipoLocal");
            }
        }

        private int golesvisitante;

        public int GolesEquipoVisitante
        {
            get { return golesvisitante; }
            set
            {
                golesvisitante = value;
                OnProperyChanged("GolesEquipoVisitante");
            }
        }

        private string tiempo;
        public string Tiempo
        {
            get { return tiempo; }
            set
            {
                tiempo = value;
                OnProperyChanged("Tiempo");
            }
        }

    }
}
