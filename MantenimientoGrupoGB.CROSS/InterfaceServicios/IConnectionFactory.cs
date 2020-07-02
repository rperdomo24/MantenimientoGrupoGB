using MantenimientoGrupoGB.DAL.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace MantenimientoGrupoGB.CROSS.InterfaceServicios
{
    public interface IConnectionFactory
    {
        public MantenimientogrupogbContext GetMantenimientogrupogbContext { get; }
    }
}
