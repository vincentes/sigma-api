using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace API.Utils
{
    public static class Firebase
    {
        public static void SendNotification(string token, string title, string body)
        {
            WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            tRequest.Method = "post";
            //serverKey - Key from Firebase cloud messaging server  
            tRequest.Headers.Add(string.Format("Authorization: key={0}", "AAAAO1RqYb0:APA91bFBNSUfhsJzjKqv-uO6cPTjpW2R39MIWqKiC5eyqgrAVpUxv67306up7Edm_19RQMGGKc3pyX6ZswgRYsGms3cyeYbUmAZEzgYJ5zPN0m2AkaA9CQqq_R5obPJRj5-dKqUNidSh"));
            //Sender Id - From firebase project setting  
            tRequest.Headers.Add(string.Format("Sender: id={0}", "254819328445"));
            tRequest.ContentType = "application/json";
            var payload = new
            {
                to = token,
                priority = "high",
                content_available = true,
                notification = new
                {
                    title,
                    body,
                    badge = 1
                },
            };

            string postbody = JsonConvert.SerializeObject(payload).ToString();
            Byte[] byteArray = Encoding.UTF8.GetBytes(postbody);
            tRequest.ContentLength = byteArray.Length;
            using (Stream dataStream = tRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                using (WebResponse tResponse = tRequest.GetResponse())
                {
                    using (Stream dataStreamResponse = tResponse.GetResponseStream())
                    {
                        if (dataStreamResponse != null) using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                //result.Response = sResponseFromServer;
                            }
                    }
                }
            }
        }
    }
}
