using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Shop
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Waiting for location response...");
            var locationResponse = new WebClient().DownloadString("https://2ip.ua/ru/services/information-service/site-location");
            locationResponse = GetResponseToRequest(locationResponse, "ip=", 3, '"');
            var textOfRequest = "https://prozavr.ru/tools/uznat_ip_adres/";
            textOfRequest = textOfRequest + locationResponse + ".html";
            var location = new WebClient().DownloadString(textOfRequest);
            location = GetResponseToRequest(location, "Европа &rarr;", 146, '<');
            location = location.Replace(" &rarr;", ",");
            Console.WriteLine(location);
            Console.ReadKey();
        }

        private static string GetResponseToRequest(string locationResponse, string subString, int countOfSymbols, char symbol)
        {
            int indexOfChar = locationResponse.IndexOf(subString);
            locationResponse = locationResponse.Substring(indexOfChar);
            locationResponse = locationResponse.Substring(countOfSymbols);
            int i = 0;
            while (locationResponse[i] != symbol)
                i++;
            locationResponse = locationResponse.Substring(0, i);
            return locationResponse;
        }


    }
}