using Evaluacion2Model.DAL;
using ServidorSocketUtils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Evaluacion2Mardones.Comunicacion
{
    public class HebraServidor
    {
        private IMensajesDAL mensajesDAL = MensajesDALArchivos.GetInstancia();
        public void Ejecutar()
        {
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            ServerSocket servidor = new ServerSocket(puerto);
            Console.WriteLine("S: Servidor iniciado en puerto {0}", puerto);
            if (servidor.Iniciar())
            {
                while (true)
                {
                    Console.WriteLine("S: Esperando medidor...");
                    Socket cliente = servidor.ObtenerCliente();
                    Console.WriteLine("S: Medidor recibido");   
                    ClienteCom clienteCom = new ClienteCom(cliente);
                    Console.WriteLine("");
                    HebraCliente clienteThread = new HebraCliente(clienteCom);
                    Thread t = new Thread(new ThreadStart(clienteThread.ejecutar));
                    t.IsBackground = true;
                    t.Start();

                }
            }
            else
            {
                Console.WriteLine("Fallo, no se puede iniciar server en {0}", puerto);
            }
        }
    }
}
