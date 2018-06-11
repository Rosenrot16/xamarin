using Proyecto.Objetos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Proyecto.Consultas
{
    class ConsultaPoliticaApp
    {
        public async static Task<List<ObjetoPoliticaApp>> getJsonPoliticaApp(String url)
        {
            using (HttpClient client = new HttpClient())
            {
                ILeerDatos lid = DependencyService.Get<ILeerDatos>();
                string id = lid.LecturaFichero();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Using https to satisfy iOS ATS requirements
                var response = await client.GetAsync(new Uri(url + id)).ConfigureAwait(continueOnCapturedContext: false);

                //response.EnsureSuccessStatusCode(); //I was playing around with this to see if it makes any difference
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    //categoryLinks = JsonConvert.DeserializeObject<IEnumerable<Link>>(content);

                    List<ObjetoPoliticaApp> msg = JsonConvert.DeserializeObject<List<ObjetoPoliticaApp>>(content);

                    System.Diagnostics.Debug.WriteLine("Contenido del json política app: " + content);

                    return msg;
                }
                else
                {
                    return null;
                }
            }

        }
    }
}
