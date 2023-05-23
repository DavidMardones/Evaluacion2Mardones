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
                Console.WriteLine("Bienvenido!");
                string mensaje = medidorSocket.Leer();
                Console.WriteLine("S: {0}", mensaje);
                int nroMedidor = Convert.ToInt32(Console.ReadLine().Trim());
                medidorSocket.Escribir(nroMedidor);
                mensaje = medidorSocket.Leer();
                Console.WriteLine("S: {0}", mensaje);
                string fecha = Console.ReadLine().Trim();
                medidorSocket.Escribir(fecha);
                mensaje = medidorSocket.Leer();
                Console.WriteLine("S: {0}", mensaje);
                decimal valorConsumo = Convert.ToDecimal(Console.ReadLine().Trim());
                medidorSocket.Escribir(valorConsumo);
                mensaje = medidorSocket.Leer();
                Console.WriteLine("S: Gracias");
                medidorSocket.Desconectar();

            }
            else
            {
                Console.WriteLine("Error de comunicacion");
            }
            Console.ReadKey();
        }
    }
}


