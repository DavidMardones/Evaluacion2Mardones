using Evaluacion2Model.DAL;
using Evaluacion2Model.DTO;
using ServidorSocketUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluacion2Mardones.Comunicacion
{
    public class HebraCliente
    {
        private ClienteCom clienteCom;
        private IMensajesDAL mensajesDAL = MensajesDALArchivos.GetInstancia();

        public HebraCliente(ClienteCom clienteCom)
        {
            this.clienteCom = clienteCom;
        }

        public void ejecutar()
        {
            clienteCom.Escribir("Ingrese el numero de medidor: ");
            int nroMedidor = Convert.ToInt32(clienteCom.Leer());
            clienteCom.Escribir("Ingrese la fecha(AAAA-MM-DD HH-MM-SS): ");
            string fecha = clienteCom.Leer();
            clienteCom.Escribir("Ingrese el valor de consumo(kw/h): ");
            decimal valorConsumo = Convert.ToDecimal(clienteCom.Leer());
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

            clienteCom.Desconectar();
        }
    }
}
