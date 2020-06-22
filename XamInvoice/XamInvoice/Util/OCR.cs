using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace XamInvoice.Util
{
    public static class OCR
    {

        static string subscriptionKey = "key";

        
        static string uri = "https://apidecomputervision.cognitiveservices.azure.com/vision";

        //EndPoint de ejemplo
       // https://{endpoint}/vision/v3.0/ocr[?language][&detectOrientation]

        public static async Task<String> MakeOCRRequest(byte[] byteData)
        {

            try
            {
                HttpResponseMessage response;
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add(
                       "Ocp-Apim-Subscription-Key", subscriptionKey);

                using (ByteArrayContent content = new ByteArrayContent(byteData))
                {
                    
                    content.Headers.ContentType =
                        new MediaTypeHeaderValue("application/octet-stream");

                 
                    response = await client.PostAsync("https://apidecomputervision.cognitiveservices.azure.com/vision/v3.0/ocr?language=es&detectOrientation=true", content);
                }

                string contentString = await response.Content.ReadAsStringAsync();
                return await Task.FromResult<string>(contentString);

                


            }
            catch (Exception)
            {

                throw;
            }


        }

    }



   



}
