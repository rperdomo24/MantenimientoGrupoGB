using MantenimientoGrupoGB.CROSS.InterfaceServicios;
using MantenimientoGrupoGB.DAL.Context;
using MantenimientoGrupoGB.EN.ConfiguracionGeneral;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace MantenimientoGrupoGB.CROSS
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly AppSettings _appSettings;
        public ConnectionFactory(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public MantenimientogrupogbContext GetMantenimientogrupogbContext =>  new MantenimientogrupogbContext(_appSettings.ConnectionStringMantenimientoGrupoGB);
    }
}
