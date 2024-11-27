using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpNewsPAT
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Создём запрос для получения данных на странице
            WebRequest request = WebRequest.Create("http://news.permaviat.ru/main");
            // Выполняем запрос, записывая результат в переменную response
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            // Выводим статус ответа в консоль
            Console.WriteLine(response.StatusDescription);
            // Создаём поток для чтения данных ответа
            Stream dataStream = response.GetResponseStream();
            // Инициализируем поток для чтения данных
            StreamReader reader = new StreamReader(dataStream);
            // Читаем ответ
            string responseFromServer = reader.ReadToEnd();
            // Выодим ответ в консоль
            Console.WriteLine(responseFromServer);
            // Закрываем потоки и соединение
            reader.Close();
            dataStream.Close();
            response.Close();
            Console.Read();
        }
    }
}
