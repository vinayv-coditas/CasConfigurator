using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectConfigure.CheckSqlServer
{
    public  class FileHandler
    {
        public FileHandler()
        {
            Console.WriteLine("INPUT: Please provide basepath for Engine: e.g. C:/ConnectAndSell-new/Cast-A-Net-WCF");
            basePathEngine = Console.ReadLine();
            basePathEngine = Path.Combine(basePathEngine.Split("/"));
            Console.WriteLine("INPUT: Please provide basepath for Web: e.g. C:/ConnectAndSell-new/ConnectAndSell-WCF");
            basePathWeb = Console.ReadLine();
            basePathWeb = Path.Combine(basePathWeb.Split("/"));
            filepaths = new Dictionary<string, string>
            {
                {Path.Combine(basePathEngine,"Data Access Layer","app.config"), @"Data Source=(.*);Initial Catalog=(.*)" },
                {Path.Combine(basePathEngine,"Data Access Layer","DAL.cs"), @"Initial Catalog=(.*);Server=(.*)" },
                {Path.Combine(basePathEngine,"HostWCFEngine","App.config"), @"Data Source=(.*);Initial Catalog=(.*)" },
                {Path.Combine(basePathWeb,"ConnectAndSell.Web","Web.config"), @"Data Source=(.*);Initial Catalog=(.*)" },
                {Path.Combine(basePathEngine,"CasReportsImpl","Properties","Settings.Designer.cs"), @"Data Source=(.*);Initial Catalog=(.*)" },
                {Path.Combine(basePathEngine,"CasReportsImpl","Properties","Settings.settings"), @"Data Source=(.*);Initial Catalog=(.*)" }
            };
        }

        public string basePathEngine { get; set; }
        public string basePathWeb { get; set; }
        Dictionary<string, string> filepaths;

        public static void ReadConfigValue(string configFile, string searchText, string replacementText)
        {
            string fileContents = File.ReadAllText(configFile);
            if (fileContents.Contains(searchText))
            {
                string modifiedContents = fileContents.Replace(searchText, replacementText);
                File.WriteAllText(configFile, modifiedContents);

                Console.WriteLine("SUCCESS: Text replaced successfully.");
            }
            else
            {
                Console.WriteLine("ERROR: Search key does not exists");
            }
        }

        public void readAndUpdateFile(string filepath, string replacementText)
        {
            while (true)
            {
                if (File.Exists(filepath))
                {
                    while (true)
                    {
                        if (filepaths.ContainsKey(filepath))
                        {
                            string searchText = filepaths[filepath];
                            ReadConfigValue(filepath, searchText, replacementText);
                            break;
                        }
                        else
                        {
                            if (filepath.Contains("ConnectAndSell.Web"))
                            {
                                Console.WriteLine("ERROR: Base path for web is incorrect");
                                Console.WriteLine("INPUT: Please provide basepath for Web: e.g. C:/ConnectAndSell-new/ConnectAndSell-WCF");
                                basePathWeb = Console.ReadLine();
                                basePathWeb = Path.Combine(basePathWeb.Split("/"));
                            }
                            else
                            {
                                Console.WriteLine("ERROR: Base path for engine is incorrect");
                                Console.WriteLine("INPUT: Please provide basepath for Engine: e.g. C:/ConnectAndSell-new/Cast-A-Net-WCF");
                                basePathEngine = Console.ReadLine();
                                basePathEngine = Path.Combine(basePathEngine.Split("/"));
                            }

                        }
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("ERROR: File does not exists please reenter the path");
                    filepath = Console.ReadLine();
                }
            }

        }
    }
}
