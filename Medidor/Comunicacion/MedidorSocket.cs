using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Medidor.Comunicacion
{
    public class MedidorSocket
    {
        private int puerto;
        private string server;
        private Socket medidor;
        private StreamReader reader;
        private StreamWriter writer;

        public MedidorSocket(string server, int puerto)
        {
            this.server = server;
            this.puerto = puerto;

        }

        public bool Conectar()
        {
            try
            {
                this.medidor = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(this.server), this.puerto);
                this.medidor.Connect(endPoint);
                Stream stream = new NetworkStream(this.medidor);
                this.reader = new StreamReader(stream);
                this.writer = new StreamWriter(stream);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public string Leer()
        {
            try
            {
                return this.reader.ReadLine().Trim();
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public void Escribir(string mensaje)
        {
            try
            {
                this.writer.WriteLine(mensaje);
                this.writer.Flush();
            }
            catch (Exception ex)
            {

            }
        }
        public void Desconectar()
        {
            try
            {
                this.medidor.Close();
            }
            catch (Exception ex)
            {

            }
        }

        internal object Escribir(int nroMedidor)
        {
            throw new NotImplementedException();
        }
    }
}


