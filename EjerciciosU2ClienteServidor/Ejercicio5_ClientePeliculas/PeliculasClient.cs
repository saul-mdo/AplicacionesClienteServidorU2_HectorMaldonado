using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio5_ClientePeliculas
{
    public class PeliculasClient
    {
        HttpClient cliente = new HttpClient();

        public PeliculasClient()
        {
            cliente.BaseAddress = new Uri("http://localhost:8080/");
        }

        public async void Agregar(DatosPelicula p)
        {
            var json = JsonConvert.SerializeObject(p);
            var result = await cliente.PostAsync("Actividad5/Tablero", new StringContent(json, Encoding.UTF8, "application/json"));
            result.EnsureSuccessStatusCode();
        }

        public async void Eliminar(DatosPelicula p)
        {
            var json = JsonConvert.SerializeObject(p);
            HttpRequestMessage msj = new HttpRequestMessage(HttpMethod.Delete, "Actividad5/Tablero");
            msj.Content = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await cliente.SendAsync(msj);

            result.EnsureSuccessStatusCode();
        }

        public async void Editar(DatosPelicula p)
        {
            var json = JsonConvert.SerializeObject(p);
            var result = await cliente.PutAsync("Actividad5/Tablero", new StringContent(json, Encoding.UTF8, "application/json"));
            result.EnsureSuccessStatusCode();
        }

        public delegate void cambio();
        public event cambio AlHaberCambios;
        public IEnumerable<DatosPelicula> Model { get; set; }
        public async void Get()
        {
            var client = new HttpClient();

            var response = await client.GetAsync("http://localhost:8080/Actividad5/Tablero");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                Model = JsonConvert.DeserializeObject<IEnumerable<DatosPelicula>>(jsonString);
                AlHaberCambios?.Invoke();
            }
        }

    }
}
