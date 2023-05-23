using Medidor.Comunicacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Medidor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            string servidor = ConfigurationManager.AppSettings["servidor"];
            Console.WriteLine("Conectando a Servidor {0} en puerto {1}", servidor, puerto);
            MedidorSocket medidorSocket = new MedidorSocket(servidor, puerto);
            if (medidorSocket.Conectar())
            {
                Console.WriteLine("Conectado...");
                string mensaje = medidorSocket.Leer();
                Console.WriteLine("M: {0}", mensaje);

                    string nombre = Console.ReadLine().Trim();
                    medidorSocket.Escribir(nombre);
                    mensaje = medidorSocket.Leer();
                    Console.WriteLine("M: {0}", mensaje);
                    if (mensaje == "Chao")
                    {
                        medidorSocket.Desconectar();
                        Console.WriteLine("Desconectado");
                    }

                
            }
            else
            {
                Console.WriteLine("Error de comunicacion");
            }
            Console.ReadKey();
        }
    }
}


