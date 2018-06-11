using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Proyecto.Consultas
{
    class ConsultaModificarPerfil
    {
        public async static void json(string nombre, string cumpleanios, string nacionalidad, string sexo, string imagen)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync(new Uri("http://localhost/getJson.php?peticion=actualizar_perfil&nombre=" + nombre + "&cumpleanios=" + cumpleanios + "&nacionalidad=" + nacionalidad + "&sexo=" + sexo + "&id=" + 1 + "&imagen=" + imagen)).ConfigureAwait(continueOnCapturedContext: false);

            }
        }
    }
}
