using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evaluacion2Model.DTO;

namespace Evaluacion2Model.DAL
{
    public interface IMensajesDAL
    {
        void AgregarMensaje(Mensaje mensaje);
        List<Mensaje> ObtenerMensajes();
    }
}