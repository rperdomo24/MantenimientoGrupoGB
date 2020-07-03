using MantenimientoGrupoGB.EN.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MantenimientoGrupoGB.DAL.Interfaces
{
    /// <summary>
    /// INTERFAZ PARA EXPONER CLASES DE USUARIO DAL
    /// </summary>
    public interface IUsuarioBaseDAL
    {
        /// <summary>
        ///  METODO ENCARGADO DE OBTENER UN REGISTRO POR SU ID
        /// </summary>
        /// <param name="idUsuario">ID DE USUARIO</param>
        /// <returns>RETORNA UN OBJETO USUARIO SI ES ENCONTRADO Y SE ENCUENTRA HABILITADO</returns>
        Task<UsuarioBase> ObtenerObjeto(int idUsuario);

        /// <summary>
        /// OBTIENE UNA LISTA DE USUARIOS SI SU ESTADO ES HABILITADO
        /// </summary>
        /// <returns>RETORNA UNA LISTA DE USUARIOS HABILTIADOS</returns>
        Task<List<UsuarioBase>> ObtenerObjetos();

        /// <summary>
        /// METODO PARA AGREGAR UN USUARIO A LA BD
        /// </summary>
        /// <param name="pObjeto">OBJETO TIPO USUARIO LLENO</param>
        /// <returns>RETORNA EL ID GUID DEL USUARIO INSERTADOS</returns>
        Task<int> AgregarObjeto(UsuarioBase pObjeto);

        /// <summary>
        /// METODO PARA MODIFICAR UN USUARIO
        /// </summary>
        /// <param name="pObjeto">OBJETO TIPO USUARIO LLENO</param>
        /// <returns>RETORNA UN INT MAYOR A 0 SI SE HA REALIZADO ACCION EXITOSAMENTE</returns>
        Task<int> ModificarObjeto(UsuarioBase pObjeto);

        /// <summary>
        /// METODO PARA HACER UN ELIMINADO LOGICO DE UN USUARIO POR SU ID
        /// </summary>
        /// <param name="pIdObjeto">ID DEL USUARIO</param>
        /// <returns>RETORNA UN INT MAYOR A 0 SI SE HA REALIZADO ACCION EXITOSAMENTE</returns>
        Task<int> EliminarObjeto(int pIdObjeto);

        /// <summary>
        /// INSERTA DE FORMA MASIVA USUARIOS
        /// </summary>
        /// <param name="pObjeto">LISTA DE USUARIOS COMPLETA</param>
        /// <returns>RETORNA UN INT MAYOR A 0 SI SE HA REALIZADO ACCION EXITOSAMENTE</returns>
        Task<int> ImportarObjetos(List<UsuarioBase> pObjeto);       
    }
}
