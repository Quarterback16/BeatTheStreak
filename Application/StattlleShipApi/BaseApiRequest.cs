using System.Net;

namespace Application.StattlleShipApi
{
    public class BaseApiRequest
    {

        public HttpWebRequest CreateRequest(string url)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(
                requestUriString: url);
            httpWebRequest.ContentType = "application/json";
            //            httpWebRequest.Accept = "*/*";
            httpWebRequest.Accept = "application/vnd.stattleship.com; version=1";
            httpWebRequest.Method = "GET";
            httpWebRequest.Headers.Add("Authorization", "35a160218fc36942348a14ddaec71d43");

            return httpWebRequest;
        }
    }
}
