using MantenimientoGrupoGB.DAL.Context;
using MantenimientoGrupoGB.DAL.Interfaces;
using MantenimientoGrupoGB.EN.ConfiguracionGeneral;
using MantenimientoGrupoGB.EN.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantenimientoGrupoGB.DAL
{
    /// <summary>
    /// CLASE PARA EL MANEJO DE USUARIOS
    /// </summary>
    public class UsuarioBaseDAL : IUsuarioBaseDAL
    {
        /// <summary>
        /// CONTEX DE LA BD
        /// </summary>
        private readonly MantenimientogrupogbContext _dbGrupoGB;

        public UsuarioBaseDAL(MantenimientogrupogbContext dbGrupoGB)
        {
            this._dbGrupoGB = dbGrupoGB;
        }

        public async Task<Guid> AgregarObjeto(UsuarioBase pObjeto)
        {
            Guid result = Guid.Empty;
            // INICIO DE TRANSACCION 
            using var Transaccion = _dbGrupoGB.Database.BeginTransaction();

            //Insertamos unicamente la entidad y esperamos su Id GUID
            _dbGrupoGB.Entry(pObjeto).State = EntityState.Added;

            // SI TODO ES CORRECTO GUARDAMOS 
            _dbGrupoGB.SaveChanges();

            await Transaccion.CommitAsync();

            //CONSULTAMOS SU INSERT EXITOSO Y RETORNAMOS SU ID
            if (_dbGrupoGB.UsuarioBase.AsNoTracking().Any(X => X.IdUsuario == pObjeto.IdUsuario && !X.EstadoEliminado))
                result = pObjeto.IdUsuario;

            return result;
        }

        public async Task<int> EliminarObjeto(Guid pIdObjeto)
        {
            int result = 0;
            using var Transaccion = _dbGrupoGB.Database.BeginTransaction();

            // CONSULTAMOS USUARIO CON LA CONDICION DE NO DAR SEGUIMIENTO AL OBJETO (ASNOTRAKING())
            var Objeto = await _dbGrupoGB.UsuarioBase
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdUsuario == pIdObjeto && !x.EstadoEliminado);

            //SETEAMOS NUEVA DATA
            Objeto.FechaModificacion = DateTime.Now;
            Objeto.FechaEliminacion = DateTime.Now;
            Objeto.EstadoEliminado = false;

            //ACTUALIZAMOS
            _dbGrupoGB.UsuarioBase.Update(Objeto);

            result = _dbGrupoGB.SaveChanges();
            await Transaccion.CommitAsync();

            return result;
        }

        public async Task<int> ImportarObjetos(List<UsuarioBase> pObjeto)
        {
            int result = 0;
            // INICIO DE TRANSACCION 
            using var Transaccion = _dbGrupoGB.Database.BeginTransaction();

            //Insertamos unicamente la entidad y esperamos su Id GUID
            await _dbGrupoGB.AddRangeAsync(pObjeto);

            // SI TODO ES CORRECTO GUARDAMOS 
            result = _dbGrupoGB.SaveChanges();

            await Transaccion.CommitAsync();

            return result;
        }

        public async Task<int> ModificarObjeto(UsuarioBase pObjeto)
        {
            // VARIABLE CREADA PARA RETORNAR EL RESULTADO DE LA MODIFICACION
            int result = 0;
            // INICIO DE TRANSACCION 
            using var Transaccion = _dbGrupoGB.Database.BeginTransaction();

            // SE ACTUALIZA EL OBJETO PROVEEDOR A LA BASE DE DATOS PREVIAMENTE LLENO

            _dbGrupoGB.Entry(pObjeto).State = EntityState.Modified;

            // SI TODO ES CORRECTO GUARDAMOS 
            result = _dbGrupoGB.SaveChanges();
            // CERRAMOS TRANSACCION Y SINO SE HACE ROLLBACK() POR EL USING
            await Transaccion.CommitAsync();

            return result;
        }

        public async Task<UsuarioBase> ObtenerObjeto(Guid idUsuario)
        {
            var result = await _dbGrupoGB.UsuarioBase
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdUsuario == idUsuario && !x.EstadoEliminado);

            return result;
        }

        public async Task<List<UsuarioBase>> ObtenerObjetos()
        {
            var result = await _dbGrupoGB.UsuarioBase
               .AsNoTracking()
               .Where(x => !x.EstadoEliminado).ToListAsync();

            return result;
        }
    }
}
