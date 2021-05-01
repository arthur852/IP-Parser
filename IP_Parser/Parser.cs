using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace IP_Parser
{
    class Parser : IP
    {
        private void CreateSaveFile()
        {
            if (!Directory.Exists(pathDirectorySave))
            {
                Directory.CreateDirectory("save");
            }

            if (!File.Exists(pathDirectorySave + pathSaveIPSuccessList))
            {
                using StreamWriter sw = File.CreateText(pathDirectorySave + pathSaveIPSuccessList);
            }

            if (!File.Exists(pathDirectorySave + pathSaveIPFailList))
            {
                using StreamWriter sw = File.CreateText(pathDirectorySave + pathSaveIPFailList);
            }
        }
        private protected void CreateDefaultFiles()
        {
            if (!File.Exists(pathIPListDefault))
            {
                using StreamWriter sw = File.CreateText(pathIPListDefault);
            }

            if (!File.Exists(pathIPListCIDR))
            {
                using StreamWriter sw = File.CreateText(pathIPListCIDR);
            }

            if (!File.Exists(pathIPListRange))
            {
                using StreamWriter sw = File.CreateText(pathIPListRange);
            }

            if (!File.Exists(pathIPSuccessList))
            {
                using StreamWriter sw = File.CreateText(pathIPSuccessList);
            }

            if (!File.Exists(pathIPFailList))
            {
                using StreamWriter sw = File.CreateText(pathIPFailList);
            }
        }

        private void EnterPort()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t\t\t ================================= ");
            Console.WriteLine("\t\t\t\t\t ==                             == ");
            Console.WriteLine("\t\t\t\t\t ==          IP Parser          == ");
            Console.WriteLine("\t\t\t\t\t ==                             == ");
            Console.WriteLine("\t\t\t\t\t ================================= ");
            Console.WriteLine();
            Console.WriteLine();
            Console.Write(" Enter port: ");
            int enterport = int.Parse(Console.ReadLine());
            port = enterport;
        }

        private void SelectPort()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t\t\t ================================= ");
            Console.WriteLine("\t\t\t\t\t ==                             == ");
            Console.WriteLine("\t\t\t\t\t ==          IP Parser          == ");
            Console.WriteLine("\t\t\t\t\t ==                             == ");
            Console.WriteLine("\t\t\t\t\t ================================= ");
            Console.WriteLine();
            Console.WriteLine();
            Console.Write(" Do you select the port? (y or n) (default port: 80): ");

            string str = Console.ReadLine();

            switch (str)
            {
                case "Y":
                    EnterPort();
                    break;
                case "y":
                    EnterPort();
                    break;
                case "N":
                    break;
                case "n":
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" Incorrect choice!");
                    Console.ResetColor();
                    Thread.Sleep(1000);
                    Console.Beep();
                    SelectPort();
                    break;
            }
        }

        private string IPStyleChoice()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t\t\t ================================= ");
            Console.WriteLine("\t\t\t\t\t ==                             == ");
            Console.WriteLine("\t\t\t\t\t ==          IP Parser          == ");
            Console.WriteLine("\t\t\t\t\t ==                             == ");
            Console.WriteLine("\t\t\t\t\t ================================= ");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(" Choose a Style IP: ");
            Console.WriteLine();
            Console.WriteLine(" 1. Default (142.250.185.110)");
            Console.WriteLine(" 2. CIDR (37.46.252.0/24)");
            Console.WriteLine(" 3. Range (37.46.252.0-37.46.252.255)");
            Console.WriteLine();

            Console.Write(" Enter: ");
            string ipChoice = Console.ReadLine();
            LoadIPList(ipChoice);
            return ipChoice;
        }

        private void LoadIPList(string ipChoice)
        {
            void ReadIp(string path)
            {
                using StreamReader sr = new StreamReader(path, System.Text.Encoding.Default);
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    ipList.Add(line);
                }
            }

            SelectPort();

            switch (ipChoice)
            {
                case "1":
                    ReadIp(pathIPListDefault);
                    break;
                case "2":
                    ReadIp(pathIPListCIDR);
                    break;
                case "3":
                    ReadIp(pathIPListRange);
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" Incorrect choice!");
                    Console.ResetColor();
                    Thread.Sleep(1000);
                    Console.Beep();
                    IPStyleChoice();
                    break;
            }
        }

        private void WriteIP(string ip, string directory, string path)
        {
            path = directory + path;

            try
            {
                using StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default);
                sw.WriteLine(ip);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void WriteIP(string ip, string path)
        {
            try
            {
                using StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default);
                sw.WriteLine(ip);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void Stats()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t\t\t\t ================================= ");
            Console.WriteLine("\t\t\t\t\t ==                             == ");
            Console.WriteLine("\t\t\t\t\t ==          IP Parser          == ");
            Console.WriteLine("\t\t\t\t\t ==                             == ");
            Console.WriteLine("\t\t\t\t\t ================================= ");
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($" All IP: {ipList.Count}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($" Success IP: {ipSuccessList.Count}");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($" Fail IP: {ipFailList.Count}");
            Console.ResetColor();
        }

        private void ParserStyle()
        {
            string ipChoice = IPStyleChoice();

            CreateSaveFile();

            switch (ipChoice)
            {
                case "1":
                    DefaultParse();
                    break;
                case "2":
                    CIDRParse();
                    break;
                case "3":
                    RangeParse();
                    break;
                default:
                    break;
            }

            void DefaultParse()
            {
                foreach (string ip in ipList)
                {
                    Stats();
                    try
                    {
                        tcpClient = new TcpClient();
                        tcpClient.Connect(ip, port);
                        if (tcpClient.Connected == true)
                        {
                            ipSuccessList.Add(ip);
                            WriteIP(ip, pathIPSuccessList);
                            WriteIP(ip, pathDirectorySave, pathSaveIPSuccessList);
                        }
                    }
                    catch (Exception)
                    {
                        ipFailList.Add(ip);
                        WriteIP(ip, pathIPFailList);
                        WriteIP(ip, pathDirectorySave, pathSaveIPFailList);
                    }
                    tcpClient.Close();
                }
            }

            void CIDRParse()
            {

            }

            void RangeParse()
            {

            }
        }

        public void Start()
        {
            ParserStyle();
            Stats();
            Console.WriteLine(" Finaly..");
            Console.WriteLine(" Press Key..");
            Console.ReadLine();
            ipList.Clear();
            ipSuccessList.Clear();
            ipFailList.Clear();
        }
    }
}
