using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Windows.Threading;
using System.ComponentModel;

namespace Ejercicio2_ColorMensaje
{
    public class Servidor : INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnProperyChanged(string property)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        private string texto;

        public string ValorDeTexto
        {
            get { return texto; }
            set
            {
                texto = value;
                OnProperyChanged("ValorDeTexto");
            }
        }

        private string color;

        public string ValorDeColor
        {
            get { return color; }
            set
            {
                color = value;
                OnProperyChanged("ValorDeColor");
            }
        }

        HttpListener listener;

        Dispatcher dispatcher;

        private void ModificarTexto(string texto)
        {
            dispatcher.BeginInvoke(new Action(() =>
            {
                ValorDeTexto = texto;
            }));
        }

        private void ModificarColor(string color)
        {
            dispatcher.BeginInvoke(new Action(() =>
            {
                ValorDeColor = color;
            }));
        }

        public Servidor()
        {
            dispatcher = Dispatcher.CurrentDispatcher;
            listener = new HttpListener();
            listener.Prefixes.Add("http://*:80/ejercicio2/");
            listener.Start();
            listener.BeginGetContext(OnRequest, null);
        }

        private void OnRequest(IAsyncResult ar)
        {
            var context = listener.EndGetContext(ar);
            listener.BeginGetContext(OnRequest, null);

            if (context.Request.Url.LocalPath== "/ejercicio2" || context.Request.Url.LocalPath == "/ejercicio2/")
            {
                var buffer = File.ReadAllBytes("Index.html");
                context.Response.ContentType = "text/html";
                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                context.Response.OutputStream.Close();
                context.Response.StatusCode = 200;
            }

            else if (context.Request.HttpMethod == "GET" && context.Request.Url.LocalPath == "/ejercicio2/textomodificado/" || context.Request.Url.LocalPath == "/ejercicio2/textomodificado")
            {
                if(context.Request.QueryString["texto"] !=null && context.Request.QueryString["color"] !=null)
                {
                    var texto = context.Request.QueryString["texto"];
                    var color = context.Request.QueryString["color"];
                    ModificarColor(color);
                    ModificarTexto(texto);
                    context.Response.StatusCode = 200;
                    context.Response.Redirect("/ejercicio2/");
                }
                else
                {
                    context.Response.StatusCode = 400;
                    context.Response.StatusDescription = "Olvidó seleccionar alguna opción";
                }
            }
            else
            {
                context.Response.StatusCode = 404;
            }
            context.Response.Close();
        }
    }
}
