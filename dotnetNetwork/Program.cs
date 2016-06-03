using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace dotnetNetwork
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isTCP = true;
            string selection;
            string ipAddress;

            NetworkUDP networkUDP = new NetworkUDP();
            NetworkTCP networkTCP = new NetworkTCP();

            //console
            Console.WriteLine("select 'TCP' or 'UDP'");
            selection = Console.ReadLine();
            if (selection == "TCP" || selection == "tcp")
            {
                isTCP = true;
            }
            else if (selection == "UDP" || selection == "udp")
            {
                isTCP = false;
            }

            Console.WriteLine("select 'host' or 'client'");
            selection = Console.ReadLine();

            //debug values
            if (selection == "host")
            {
                if (isTCP == false)
                {
                    networkUDP.setBoundPort(55001);
                }
                else
                {
                    networkTCP.host(55001);
                }
                while(true)
                {
                    if (isTCP == false)
                    {
                        networkUDP.receiveMessages();
                        for (int i = 0; i < networkUDP.getNumberOfMessages(); i++)
                        {
                            Console.WriteLine(networkUDP.getMessage(networkUDP.getNumberOfMessages() - 1));
                        }
                        networkUDP.clearMessages();
                    }
                    else
                    {
                        networkTCP.listenForConnections();
                        networkTCP.receiveMessages();
                        for (int i = 0; i < networkTCP.getNumberOfMessages(); i++)
                        {
                            Console.WriteLine(networkTCP.getMessage(networkTCP.getNumberOfMessages() - 1));
                        }
                        networkTCP.clearMessages();
                    }
                }
            }
            else if (selection == "client")
            {
                Console.WriteLine("select IP address");
                ipAddress = Console.ReadLine();

                if (isTCP == true)
                {
                    networkTCP.connect(ipAddress, 55001);
                }

                while (true)
                {
                    Console.WriteLine("Enter message");
                    string text;
                    text = Console.ReadLine();
                    if (text == "exit" || text == "quit")
                    {
                        break;
                    }
                    else
                    {
                        if (isTCP == false)
                        {
                            networkUDP.sendMessage(ipAddress, 55001, text);
                        }
                        else
                        {
                            networkTCP.sendMessage(text);
                        }
                    }
                }
            }
        }
    }
}