using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Proyecto.Consultas
{
    class Consulta
    {
        public async static void json(string correo, string contrasenia)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync(new Uri("http://localhost/getJson.php?peticion=registrar&correo="+ correo + "&contrasenia=" + contrasenia)).ConfigureAwait(continueOnCapturedContext: false);

            }
        }
    }
}
