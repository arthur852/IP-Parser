using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace IP_Parser
{
    class IP
    {
        private protected List<string> ipList = new List<string>();
        private protected List<string> ipSuccessList = new List<string>();
        private protected List<string> ipFailList = new List<string>();
        private protected const string pathIPListDefault = "ip_List_Default.txt";
        private protected const string pathIPListCIDR = "ip_List_CIDR.txt";
        private protected const string pathIPListRange = "ip_List_Range.txt";
        private protected const string pathIPSuccessList = "ip_Success_list.txt";
        private protected const string pathIPFailList = "ip_Fail_list.txt";
        private protected const string pathDirectorySave = "save\\";
        private protected readonly static string pathSaveIPSuccessList;
        private protected readonly static string pathSaveIPFailList;
        private protected TcpClient tcpClient = new TcpClient();

        static IP()
        {
            Guid guid = Guid.NewGuid();
            string fileguid = guid.ToString();
            pathSaveIPSuccessList = $"ip_Success_list({fileguid}).txt";
            pathSaveIPFailList = $"ip_Fail_list({fileguid}).txt";
        }
    }
}