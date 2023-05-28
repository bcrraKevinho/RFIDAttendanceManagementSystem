using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;


namespace sistemaAsistenciaDashboard.Helpers
{
    public class ConsultaApi
    {
        public string Post(string url, string json, string autorizacion = null)
        {
            try
            {
                var client = new RestClient(url);
                var request = new RestRequest(string.Empty, Method.Post);
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                if (autorizacion != null)
                {
                    request.AddHeader("Authorization", autorizacion);
                }

                RestResponse response = client.Execute(request);

                //dynamic datos = JsonConvert.DeserializeObject(response.Content);

                return response.Content;
            }
            catch (Exception ex)
            {
                throw;
                //return ex;
            }
        }
        public string Get(string url)
        {
            HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create(url);
            myWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:23.0) Gecko/20100101 Firefox/23.0";
            //myWebRequest.CookieContainer = myCookie;
            myWebRequest.Credentials = CredentialCache.DefaultCredentials;
            myWebRequest.Proxy = null;
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
            Stream myStream = myHttpWebResponse.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myStream);
            //Leemos los datos
            string Datos = HttpUtility.HtmlDecode(myStreamReader.ReadToEnd());

            //dynamic data = JsonConvert.DeserializeObject(Datos);

            return Datos;
        }
    }
}