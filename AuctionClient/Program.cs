using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AuctionClient
{
    struct add
    {
        public string name;
        public double startingprice;
    }
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                try
                {
                    string content;

                    Console.WriteLine("Enter method: ");
                    string method = Console.ReadLine();

                    Console.WriteLine("Enter URI: ");
                    string uri = Console.ReadLine();

                    HttpWebRequest req = WebRequest.Create(uri) as HttpWebRequest;
                    req.KeepAlive = false;
                    req.Method = method.ToUpper();

                    if (("POST,PUT").Split(',').Contains(method.ToUpper()))
                    {
                        byte[] bytes = null;
                        if (uri.Contains("add"))
                        {
                            string name = "";
                            Console.WriteLine("podaj imie");
                            name = Console.ReadLine();
                            string price = "";
                            Console.WriteLine("podaj swoja cene");
                            price = Console.ReadLine().Replace(',', '.');
                            bytes = UTF8Encoding.UTF8.GetBytes("{\"name\":\"" + name + "\", \"startingprice\":" + price + "}");
                        }
                        else if (uri.Contains("bid"))
                        {
                            string id = "";
                            Console.WriteLine("podaj id");
                            id = Console.ReadLine();
                            string name = "";
                            Console.WriteLine("podaj imie");
                            name = Console.ReadLine();
                            string price = "";
                            Console.WriteLine("podaj swoja cene");
                            price = Console.ReadLine().Replace(',', '.');
                            bytes = UTF8Encoding.UTF8.GetBytes("{\"id\":\"" + id + "\", \"name\":\"" + name + "\", \"startingprice\":" + price + "}");
                        }
                        else if (uri.Contains("remove") || uri.Contains("finish"))
                        {
                            string id = "";
                            Console.WriteLine("podaj id");
                            id = Console.ReadLine();
                            string name = "";
                            Console.WriteLine("podaj imie");
                            name = Console.ReadLine();
                            string price = "";
                            Console.WriteLine("podaj swoja cene");
                            price = Console.ReadLine().Replace(',', '.');
                            bytes = UTF8Encoding.UTF8.GetBytes("{\"id\":\"" + id + "\"}");
                        }
                        req.ContentLength = bytes.Length;
                        req.ContentType = "application/json";
                        Stream pd = req.GetRequestStream();
                        pd.Write(bytes, 0, bytes.Length);
                        pd.Close();
                    }
                    HttpWebResponse resp = req.GetResponse() as HttpWebResponse;

                    Encoding enc = System.Text.Encoding.GetEncoding(1252);
                    StreamReader sr = new StreamReader(resp.GetResponseStream(), enc);
                    string r = sr.ReadToEnd();
                    sr.Close();
                    resp.Close();
                    Console.WriteLine("Response: {0}", r);
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.Message);
                }
            } while (true);
        }
    }
}
