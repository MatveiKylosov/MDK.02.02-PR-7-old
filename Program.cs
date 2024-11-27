using HtmlAgilityPack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpNewsPAT
{
    internal class Program
    {
        public static void ParsingHtml(string htmlCode)
        {
            var html = new HtmlDocument();
            // Загружаем код страницы в объект HTML
            html.LoadHtml(htmlCode);
            // Получаем структуру страницы
            var Document = html.DocumentNode;
            // Получаем элементы по классу News
            IEnumerable DivsNews = Document.Descendants(0).Where(n => n.HasClass("news"));
            // Перебираем элементы
            foreach (HtmlNode DivNews in DivsNews)
            {
                // Получаем SRC картинки
                var src = DivNews.ChildNodes[1].GetAttributeValue("src", "none");
                // Получаем наименоване
                var name = DivNews.ChildNodes[3].InnerText;
                // Получаем описание
                var description = DivNews.ChildNodes[5].InnerText;
                // Выводим надпись
                Console.WriteLine(name + "\n" + "Изображение: " + src + "\n" + "Описание: " + description + "\n");
            }
        }

        public static void SingIn(string Login, string Password)
        {
            // Задаём URL
            string url = "http://news.permaviat.ru/ajax/login.php";
            // Выводим в Debug адрес куда обращаемся
            Debug.WriteLine($"Выполняем запрос: {url}");
            // Создаём запрос для авторизации на сайте
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            // Указываем метод передачи POST
            request.Method = "POST";
            // Указываем тип передачи
            request.ContentType = "application/x-www-form-urlencoded";
            // Создаём контейнер для Cookies
            request.CookieContainer = new CookieContainer();
            // Создаём FormData
            string postData = $"login={Login}&password={Password}";
            // Конвертируем в ASCII
            byte[] Data = Encoding.ASCII.GetBytes(postData);
            // Указываем длину сообщения
            request.ContentLength = Data.Length;
            // Записываем дату в запрос
            using (var stream = request.GetRequestStream())
            {
                stream.Write(Data, 0, Data.Length);
            }
            // Выполняем запрос, записывая результат в переменную response
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            // Выводим статус обращения
            Debug.WriteLine($"Статус выполннения: {response.StatusCode}");
            // Читаем ответ
            string responseFromServer = new StreamReader(response.GetResponseStream()).ReadToEnd();
            // Выодим ответ в консоль
            Console.WriteLine(responseFromServer);
        }

        public static void GetContent(Cookie Token)
        {
            // Задаём URL
            string url = "http://news.permaviat.ru/main";
            Debug.WriteLine($"Выполняем запрос: {url}");
            // Создаём запрос для авторизации на сайте
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            // Создаём контейнер для Cookies
            request.CookieContainer = new CookieContainer();
            // Добавлям Cookie авторизированного пользователя
            request.CookieContainer.Add(Token);
            // Выполняем запрос, записывая результат в переменную response
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            // Выводим статус обращения
            Debug.WriteLine($"Статус выполннения: {response.StatusCode}");
            // Читаем ответ
            string responseFromServer = new StreamReader(response.GetResponseStream()).ReadToEnd();
            Console.WriteLine(responseFromServer);
        }

        static void Main(string[] args)
        {
            SingIn("student", "Asdfg123");
            Console.Read();
        }
    }
}
