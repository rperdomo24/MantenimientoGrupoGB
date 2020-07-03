using MantenimientoGrupoGB.BL.Interfaces;
using MantenimientoGrupoGB.DAL.Interfaces;
using MantenimientoGrupoGB.EN.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MantenimientoGrupoGB.BL
{
    public class UsuarioBaseBL : IUsuarioBaseBL
    {
        private readonly IUsuarioBaseDAL _usuarioBaseDAL;
        public UsuarioBaseBL(IUsuarioBaseDAL usuarioBaseDAL)
        {
            _usuarioBaseDAL = usuarioBaseDAL;
        }

        public async Task<int> AgregarObjeto(UsuarioBase pObjeto)
        {
            pObjeto.FechaCreacion = DateTime.Now;
            pObjeto.EstadoEliminado = false;

            return await _usuarioBaseDAL.AgregarObjeto(pObjeto);
        }

        public async Task<int> EliminarObjeto(int pIdObjeto)
        {
            var Objeto = 0;
            Objeto = await _usuarioBaseDAL.EliminarObjeto(pIdObjeto);
            return Objeto;
        }

        public Task<int> ImportarObjetos(List<UsuarioBase> pObjeto)
        {
            throw new NotImplementedException();
        }

        public async Task<int> ModificarObjeto(UsuarioBase pObjeto)
        {
            pObjeto.FechaModificacion = DateTime.Now;
            return await _usuarioBaseDAL.ModificarObjeto(pObjeto);
        }

        public async Task<UsuarioBase> ObtenerObjeto(int idUsuario)
        {
            var Objeto = new UsuarioBase();
            Objeto = await _usuarioBaseDAL.ObtenerObjeto(idUsuario);
            return Objeto;
        }

        public async Task<List<UsuarioBase>> ObtenerObjetos()
        {
            var Objetos = new List<UsuarioBase>();
            Objetos = await _usuarioBaseDAL.ObtenerObjetos();

            return Objetos;
        }
    }
}
