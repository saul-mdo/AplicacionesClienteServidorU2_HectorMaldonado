﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio4_ClienteVuelos
{
    public class ClienteVuelos
    {
        HttpClient cliente = new HttpClient();

        public ClienteVuelos()
        {
            cliente.BaseAddress = new Uri("http://vuelos.itesrc.net/");
        }

        public async void Agregar(DatosVuelo v)
        {
            var json = JsonConvert.SerializeObject(v);
            var result = await cliente.PostAsync("Tablero", new StringContent(json, Encoding.UTF8, "application/json"));
            result.EnsureSuccessStatusCode();
        }

        public async void Eliminar(DatosVuelo v)
        {
            var json = JsonConvert.SerializeObject(v);
            HttpRequestMessage msj = new HttpRequestMessage(HttpMethod.Delete, "Tablero");
            msj.Content = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await cliente.SendAsync(msj);

            result.EnsureSuccessStatusCode();
        }

        public async void Editar(DatosVuelo v)
        {
            var json = JsonConvert.SerializeObject(v);
            var result = await cliente.PutAsync("Tablero", new StringContent(json, Encoding.UTF8, "application/json"));
            result.EnsureSuccessStatusCode();
        }

        public IEnumerable<DatosVuelo> Vuelos { get; set; }

        public delegate void cambio();
        public event cambio AlHaberCambios;

        public IEnumerable<DatosVuelo> Model { get; set; }
        public async void Get()
        {
            var client = new HttpClient();

            var response = await client.GetAsync("http://vuelos.itesrc.net/Tablero");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                Model = JsonConvert.DeserializeObject<IEnumerable<DatosVuelo>>(jsonString);
                AlHaberCambios?.Invoke();
            }
        }

    }
}
