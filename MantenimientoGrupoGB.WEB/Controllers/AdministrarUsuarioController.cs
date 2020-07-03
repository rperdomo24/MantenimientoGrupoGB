using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MantenimientoGrupoGB.BL.Interfaces;
using MantenimientoGrupoGB.EN.Model;
using MantenimientoGrupoGB.WEB.Models.AuxiliaryModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NToastNotify;

namespace MantenimientoGrupoGB.WEB.Controllers
{
    public class AdministrarUsuarioController : Controller
    {
        private readonly IToastNotification _toastNotification;
        private readonly IUsuarioBaseBL _usuarioBaseBL;
        public AdministrarUsuarioController(
             IToastNotification toastNotification,
             IUsuarioBaseBL usuarioBaseBL)
        {
            _toastNotification = toastNotification;
            _usuarioBaseBL = usuarioBaseBL;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UsuarioBase dataUsuario)
        {
            int result = 0;
            if (ModelState.IsValid)
            {
                result = await _usuarioBaseBL.AgregarObjeto(dataUsuario);
                if (result > 0)
                {
                    _toastNotification.AddSuccessToastMessage("Usuario creado exitosamente");
                }
                else
                {
                    _toastNotification.AddErrorToastMessage("Hubo un error no se pudo crear el nuevo usuario");
                }
            }
            else
            {
                _toastNotification.AddInfoToastMessage("Por favor revise que todos sus datos esten llenos.");

            }

            return View("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int IdUsuario)
        {
            UsuarioBase result = new UsuarioBase();
            result = await _usuarioBaseBL.ObtenerObjeto(IdUsuario);
            if (result != null)
            {
                _toastNotification.AddInfoToastMessage("Usuario obtenido con exito");
                return View(result);
            }
            else
            {
                _toastNotification.AddErrorToastMessage("Hubo un error al obtener el usuario");
                return View("Index");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Update(UsuarioBase dataUsuario)
        {
            int result = 0;
            if (ModelState.IsValid)
            {
                result = await _usuarioBaseBL.ModificarObjeto(dataUsuario);
                if (result > 0)
                {
                    _toastNotification.AddSuccessToastMessage("Usuario modificado exitosamente");
                }
                else
                {
                    _toastNotification.AddErrorToastMessage("Hubo un error no se pudo modificar el nuevo usuario");
                }
            }
            else
            {
                _toastNotification.AddInfoToastMessage("Por favor revise que todos sus datos esten llenos.");

            }

            return View("index");
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int IdUsuario)
        {
            if (IdUsuario > 0)
            {
                int result = await _usuarioBaseBL.EliminarObjeto(IdUsuario);
                if (result > 0)
                {
                    _toastNotification.AddSuccessToastMessage("Eliminado exitosamente");
                    return Json(new { data = result });
                }
                else
                {
                    _toastNotification.AddErrorToastMessage("Hubo un error al intentar eliminar el usuario");
                }
            }
            return Json(new { data = 0 });
        }

        [HttpPost]
        public async Task<IActionResult> Listar([FromBody] DtParameters dtParameters)
        {
            var searchBy = dtParameters.Search?.Value;

            var orderCriteria = string.Empty;
            var orderAscendingDirection = true;

            if (dtParameters.Order != null)
            {
                // in this example we just default sort on the 1st column
                orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
            }
            else
            {
                // if we have an empty search then just order the results by Id ascending
                orderCriteria = "nombres";
                orderAscendingDirection = true;
            }

            var result = await _usuarioBaseBL.ObtenerObjetos();

            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.Nombres != null && r.Nombres.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.Apellidos != null && r.Apellidos.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.Dui != null && r.Dui.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.Nit != null && r.Nit.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.Isss != null && r.Isss.ToUpper().Contains(searchBy.ToUpper())).ToList();

            }




            //result = orderAscendingDirection ? result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Asc).ToList() : result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Desc).ToList();

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            var filteredResultsCount = result.Count();
            var totalResultsCount = result.Count();

            return Json(new
            {
                draw = dtParameters.Draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = result
                   .Skip(dtParameters.Start)
                   .Take(dtParameters.Length)
                   .ToList()
            });
        }
    }
}
