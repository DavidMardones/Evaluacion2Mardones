using Evaluacion2Mardones.Comunicacion;
using Evaluacion2Model.DAL;
using Evaluacion2Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Evaluacion2Mardones
{
    public class Program
    {
        private static IMensajesDAL mensajesDAL = MensajesDALArchivos.GetInstancia();

        static bool Menu()
        {
            bool continuar = true;
            Console.WriteLine("Selecciones una opcion");
            Console.WriteLine(" 1. Ingresar \n 2. Mostrar \n 0. Salir");
            switch (Console.ReadLine().Trim())
            {
                case "1":
                    Ingresar();
                    break;
                case "2":
                    Mostrar();
                    break;
                case "0":
                    continuar = false;
                    break;
                default:
                    Console.WriteLine("Ingrese de nuevo");
                    break;
            }
            return continuar;
        }

        static void IniciarServidor()
        {


        }
        static void Main(string[] args)
        {

            HebraServidor hebra = new HebraServidor();
            Thread t = new Thread(new ThreadStart(hebra.Ejecutar));
            t.Start();
            while (Menu()) ;
        }

        static void Ingresar()
        {
            Console.WriteLine("Ingrese numero de medidor: ");
            int nroMedidor = Convert.ToInt32(Console.ReadLine().Trim());
            Console.WriteLine("Ingrese la fecha(AAAA-MM-DD HH-MM-SS): ");
            DateTime fecha = Convert.ToDateTime(Console.ReadLine().Trim());
            Console.WriteLine("Ingrese el valor de consumo(kw/h): ");
            decimal valorConsumo = Convert.ToDecimal(Console.ReadLine().Trim());
            Mensaje mensaje = new Mensaje()
            {
                NroMedidor = nroMedidor,
                Fecha = fecha,
                ValorConsumo = valorConsumo
            };
            lock (mensajesDAL)
            {
                mensajesDAL.AgregarMensaje(mensaje);
            }
        }

        static void Mostrar()
        {
            List<Mensaje> lecturas = null;
            lock (mensajesDAL)
            {
                lecturas= mensajesDAL.ObtenerMensajes();
            }
            foreach (Mensaje mensaje in lecturas)
            {
                Console.WriteLine(mensaje);
            }
        }
    }
}