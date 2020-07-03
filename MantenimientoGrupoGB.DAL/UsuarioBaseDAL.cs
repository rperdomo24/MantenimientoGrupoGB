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

        public async Task<int> AgregarObjeto(UsuarioBase pObjeto)
        {
            int result =0;
            // INICIO DE TRANSACCION 
            using var Transaccion = _dbGrupoGB.Database.BeginTransaction();

            try
            {
                //Insertamos unicamente la entidad y esperamos su Id GUID
                _dbGrupoGB.Entry(pObjeto).State = EntityState.Added;

                // SI TODO ES CORRECTO GUARDAMOS 
                _dbGrupoGB.SaveChanges();

                await Transaccion.CommitAsync();

                //CONSULTAMOS SU INSERT EXITOSO Y RETORNAMOS SU ID
                if (_dbGrupoGB.UsuarioBase.AsNoTracking().Any(X => X.IdUsuario == pObjeto.IdUsuario && !X.EstadoEliminado))
                    result = pObjeto.IdUsuario;
            }
            catch (Exception ex)
            {
                await Transaccion.RollbackAsync();
            }
            return result;
        }

        public async Task<int> EliminarObjeto(int pIdObjeto)
        {
            int result = 0;
            using var Transaccion = _dbGrupoGB.Database.BeginTransaction();

            try
            {
                // CONSULTAMOS USUARIO CON LA CONDICION DE NO DAR SEGUIMIENTO AL OBJETO (ASNOTRAKING())
                var Objeto = await _dbGrupoGB.UsuarioBase
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.IdUsuario == pIdObjeto && !x.EstadoEliminado);

                //SETEAMOS NUEVA DATA
                Objeto.FechaModificacion = DateTime.Now;
                Objeto.FechaEliminacion = DateTime.Now;
                Objeto.EstadoEliminado = true;

                //ACTUALIZAMOS
                _dbGrupoGB.UsuarioBase.Update(Objeto);

                result = _dbGrupoGB.SaveChanges();
                await Transaccion.CommitAsync();
            }
            catch (Exception ex)
            {

                await Transaccion.RollbackAsync();
            }

            return result;
        }

        public async Task<int> ImportarObjetos(List<UsuarioBase> pObjeto)
        {
            int result = 0;
            // INICIO DE TRANSACCION 
            using var Transaccion = _dbGrupoGB.Database.BeginTransaction();

            try
            {
                //Insertamos unicamente la entidad y esperamos su Id GUID
                await _dbGrupoGB.AddRangeAsync(pObjeto);
                // SI TODO ES CORRECTO GUARDAMOS 
                result = _dbGrupoGB.SaveChanges();
                await Transaccion.CommitAsync();
            }
            catch (Exception)
            {
                await Transaccion.RollbackAsync();
            }


            return result;
        }

        public async Task<int> ModificarObjeto(UsuarioBase pObjeto)
        {
            // VARIABLE CREADA PARA RETORNAR EL RESULTADO DE LA MODIFICACION
            int result = 0;
            // INICIO DE TRANSACCION 
            using var Transaccion = _dbGrupoGB.Database.BeginTransaction();

            try
            {
                // CONSULTAMOS USUARIO CON LA CONDICION DE NO DAR SEGUIMIENTO AL OBJETO (ASNOTRAKING())
                var Objeto = await _dbGrupoGB.UsuarioBase
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.IdUsuario == pObjeto.IdUsuario && !x.EstadoEliminado);

                //SETEAMOS NUEVA DATA


                Objeto.Nombres = pObjeto.Nombres;
                Objeto.Apellidos = pObjeto.Apellidos;
                Objeto.FechaNacimiento = pObjeto.FechaNacimiento;
                Objeto.Dui = pObjeto.Dui;
                Objeto.Nit = pObjeto.Nit;
                Objeto.Isss = pObjeto.Isss;
                Objeto.Telefono = pObjeto.Telefono;
                Objeto.FechaModificacion = DateTime.Now;

                //ACTUALIZAMOS
                _dbGrupoGB.UsuarioBase.Update(Objeto);

                // SI TODO ES CORRECTO GUARDAMOS 
                result = _dbGrupoGB.SaveChanges();
                // CERRAMOS TRANSACCION Y SINO SE HACE ROLLBACK() POR EL USING
                await Transaccion.CommitAsync();
            }
            catch (Exception ex)
            {
                await Transaccion.RollbackAsync();
            }
            return result;
        }

        public async Task<UsuarioBase> ObtenerObjeto(int idUsuario)
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
