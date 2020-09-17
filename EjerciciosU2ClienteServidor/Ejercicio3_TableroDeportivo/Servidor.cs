using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Web;
using System.Windows.Threading;

namespace Ejercicio3_TableroDeportivo
{
    public class Servidor
    {
        HttpListener listener;
        Dispatcher dispatcher;

        public Servidor()
        {
            dispatcher = Dispatcher.CurrentDispatcher;
            listener = new HttpListener();
            listener.Prefixes.Add("http://*:80/ejercicio3/");
            listener.Start();
            listener.BeginGetContext(OnRequest, null);
        }

        private void OnRequest(IAsyncResult ar)
        {
            var context = listener.EndGetContext(ar);
            listener.BeginGetContext(OnRequest, null);

            var url = context.Request.Url.LocalPath;
            if (url.EndsWith("/"))
            {
                url = url.Remove(url.Length - 1, 1);
            }

            if (url=="/ejercicio3" && context.Request.HttpMethod=="GET")
            {
                var index = File.ReadAllBytes("Index.html");
                context.Response.ContentType = "text/html";
                context.Response.OutputStream.Write(index, 0, index.Length);
                context.Response.OutputStream.Close();
                context.Response.StatusCode = 200;

            }
            else if (context.Request.HttpMethod == "POST" && url == "/ejercicio3")
            {
                StreamReader stream = new StreamReader(context.Request.InputStream);
                string v = stream.ReadToEnd();
                var datos = HttpUtility.ParseQueryString(v);

                ElegirLocal(datos["local"]);
                ElegirVisitante(datos["visitante"]);
                IncrementarGoles(datos["gol"]);
                SeleccionarTiempo(datos["tiempo"]);

                context.Response.StatusCode = 200;
                context.Response.Redirect("/ejercicio3");
            }
            else
            {
                context.Response.StatusCode = 404;
            }
            context.Response.Close();
        }


        public ValoresTablero valores = new ValoresTablero();

        public void ElegirLocal(string local)
        {
            dispatcher.BeginInvoke(new Action(() =>
            {
                if (local != "")
                    valores.EquipoLocal = local;
                else
                    valores.EquipoLocal = valores.EquipoLocal;
            }));
        }

        public void ElegirVisitante(string visitante)
        {
            dispatcher.BeginInvoke(new Action(() =>
            {
                if (visitante != "")
                    valores.EquipoVisitante = visitante;
                else
                    valores.EquipoVisitante = valores.EquipoVisitante;
            }));
        }

        public void IncrementarGoles(string gol)
        {
            dispatcher.BeginInvoke(new Action(() =>
            {
                if (gol != null)
                {
                    if (gol == "Gol Local")
                        valores.GolesEquipoLocal++;
                    else if (gol == "Gol visitante")
                        valores.GolesEquipoVisitante++;
                }
            }));
        }

        public void SeleccionarTiempo(string tiempo)
        {
            dispatcher.BeginInvoke(new Action(() =>
            {
            if (tiempo != null)
                {
                    if (tiempo == "1er")
                        valores.Tiempo = "1er. Tiempo";
                    else if (tiempo == "2do")
                        valores.Tiempo = "2do. Tiempo";
                    else if (tiempo == "extra")
                        valores.Tiempo = "Extra";
                    else
                        valores.Tiempo = "Penales";
                }
                else
                {
                    valores.Tiempo = valores.Tiempo;
                }
            }));
        }

    }
}
