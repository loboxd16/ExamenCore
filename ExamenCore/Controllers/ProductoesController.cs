using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExamenCore.Models;
using ExamenCore.Request;
using ExamenCore.Response;

namespace ExamenCore.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        [HttpGet]
        public List<ProductoResponse> Listar()
        {
            List<ProductoResponse> response = new List<ProductoResponse>();
            try
            {
                using (var _context = new AppDBContext())
                {
                    var productos = _context.Productos.ToList();

                    foreach (var producto in productos)
                    {
                        response.Add(new ProductoResponse
                        {
                            ProductoID = producto.ProductoID,
                            Nombre = producto.Nombre,
                            Precio = producto.Precio

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
        public ResponseBase Insertar(ProductoRequest request)
        {
            ResponseBase response = new ResponseBase();
            if (String.IsNullOrEmpty(request.Nombre))
            {
                response.CodigoError = -2;
                response.Mensaje = "Nombre de producto no puede ser null o vacío";
                return response;
            }
            if (Convert.ToInt32(request.Precio) <= 0)
            {
                response.CodigoError = -2;
                response.Mensaje = "precio debe ser mayor a cero";
                return response;
            }
            if (Convert.ToInt32(request.CategoriaID) <= 0)
            {
                response.CodigoError = -2;
                response.Mensaje = "Categoria debe ser mayor a cero";
                return response;
            }
            try
            {
                Producto producto = new Producto
                {
                    Nombre = request.Nombre,
                    Precio = request.Precio,
                    CategoriaID = request.CategoriaID
                };

                using (var _context = new AppDBContext())
                {
                    _context.Add(producto);
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
        public ProductoResponse ListarPorID(int id)
        {
            ProductoResponse response = new ProductoResponse();
            try
            {
                using (var _context = new AppDBContext())
                {
                    var producto = _context.Productos.Find(id);

                    response.Nombre = producto.Nombre;
                    response.Precio = producto.Precio;
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
