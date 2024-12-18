using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExamenCore.Models;
using ExamenCore.Response;
using ExamenCore.Request;

namespace ExamenCore.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        [HttpGet]
        public List<CategoriaResponse> Listar()
        {
            List<CategoriaResponse> response = new List<CategoriaResponse>();
            try
            {
                using (var _context = new AppDBContext())
                {
                    var categorias = _context.Categorias.ToList();

                    foreach (var categoria in categorias)
                    {
                        response.Add(new CategoriaResponse
                        {
                            ID = categoria.CategoriaID,
                            Nombre = categoria.Nombre,
                            Descripcion = categoria.Descripcion
                        });
                    }
                }
            }

            catch (Exception)
            {
                throw;
            }
            return response;
        }

        [HttpPost]
        public ResponseBase Insertar(CategoriaRequest request)
        {
            ResponseBase response = new ResponseBase();

            if (String.IsNullOrEmpty(request.Nombre))
            {
                response.CodigoError = -2;
                response.Mensaje = "Nombre de categoria no puede ser null o vacío";
                return response;
            }
            try
            {
                Categoria categoria = new Categoria
                {
                    Nombre = request.Nombre,
                    Descripcion = request.Descripcion
                };

                using (var _context = new AppDBContext())
                {
                    _context.Add(categoria);
                    _context.SaveChanges();
                    response.CodigoError = 0;
                    response.Mensaje = "Registro exitoso";
                }
            }
            catch (Exception ex)
            {
                response.CodigoError = -1;
                response.Mensaje = ex.Message.ToString();
            }
            return response;
        }

        [HttpGet]
        public CategoriaResponse ListarPorID(int id)
        {
            CategoriaResponse response = new CategoriaResponse();
            try
            {
                using (var _context = new AppDBContext())
                {
                    var categoria = _context.Categorias.Find(id);

                    response.ID = categoria.CategoriaID;
                    response.Nombre = categoria.Nombre;
                    response.Descripcion = categoria.Descripcion;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
    }
}
