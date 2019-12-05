using System;
using System.Net;

namespace ADASProject
{
    public static class Autodetect
    {
        public static string GetCity(string ip)
        {
            Console.WriteLine("Waiting for location response...");
            // запрашиваем ip устройства
            var client = new WebClient() { BaseAddress = ip };
            var locationResponse = client.DownloadString("https://2ip.ua/ru/services/information-service/site-location");
            // получаем ip
            locationResponse = GetResponseToRequest(locationResponse, "ip=", 3, '"');
            var textOfRequest = "https://check-host.net/ip-info?host=";
            // делаем ссылку для запроса
            textOfRequest = textOfRequest + locationResponse;
            // запрашиваем местоположение
            var location = new WebClient().DownloadString(textOfRequest);
            // выводим страну
            var country = GetResponseToRequest(location, "Country", 74, '<');
            // выводим город
            var city = GetResponseToRequest(location, "City", 15, '<');

            return city;
        }
        // метод, обрабатывающий строку для получения нужной информации
        private static string GetResponseToRequest(string locationResponse, string subString, int countOfSymbols, char symbol)
        {
            // номер подстроки
            int indexOfChar = locationResponse.IndexOf(subString);
            // обрезаем все, что находится до подстроки
            locationResponse = locationResponse.Substring(indexOfChar + countOfSymbols);
            // обрезаем все, что находится после подстроки
            int i = 0;
            while (locationResponse[i] != symbol)
                i++;
            locationResponse = locationResponse.Substring(0, i);
            return locationResponse;
        }
    }
}
