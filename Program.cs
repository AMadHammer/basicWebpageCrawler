using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Collections;
using System.Text.RegularExpressions;

namespace WebCrawler
{

    public  class linkUsed 
    {
        public int keywordCount;
        public string url;
        public linkUsed(int k, string u)
        {
            this.keywordCount = k;
            this.url = u;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("WebCrawler");

            Console.WriteLine("Please enter the url of the website that you would like to crawl");
            Console.Write("url: ");
            string homeURL = "http://beej.us/guide/";  //Console.ReadLine();

            //Console.WriteLine("Enter the keyword that you want to find");
            //Console.WriteLine("Keyword: ");
            //string keyword = "programming";  //Console.ReadLine();


            Hashtable PagesToSearch = new Hashtable();
            Hashtable PagesSearched = new Hashtable();

            //Send url to function
            string pageHTML =  getHTMLString(homeURL, "keyword");

            var linksInPage = GetNewLinks(pageHTML);



            int wordCount; 

            //Add all of the found URLs to the hashtable
            foreach (string s in linksInPage)                
            {
                pageHTML = getHTMLString(s, "keyword");
                wordCount = pageHTML.Split(' ', '/', '>', '<', '"').Count(p => p.Contains("programming"));

                PagesToSearch.Add(s, new linkUsed(wordCount, s));
            }


            //////print all of the found URLs
            //foreach (var s. in PagesToSearch)
            //{

            //    Console.WriteLine(s);
            //}

            foreach (DictionaryEntry entry in PagesToSearch)
	        {
                Console.WriteLine("{1},\t \t", entry.Value, entry.Key);
	        }


            Console.Write("press Enter to end the program");
            Console.ReadLine();
        }

        [STAThread]
        public static string getHTMLString (string Url, string keyword)
        {
            String homeURL = "Empty URL";

            //Open the  webpage
            WebClient client = new WebClient();
            try
            {
                homeURL = (client.DownloadString(Url));
            }
            catch
            {
                //homeURL = client.DownloadString(Url) ?? "Can't Find the URL";
            }

            return homeURL;
        }


        //credit goes to http://stackoverflow.com/a/10532435/720985
        public static ISet<string> GetNewLinks(string content)
        {
            Regex regexLink = new Regex("(?<=<a\\s*?href=(?:'|\"))[^'\"]*?(?=(?:'|\"))");

            ISet<string> newLinks = new HashSet<string>();
            foreach (var match in regexLink.Matches(content))
            {
                if (!newLinks.Contains(match.ToString()))
                    newLinks.Add(match.ToString());
            }

            return newLinks;
        }

    }



}
