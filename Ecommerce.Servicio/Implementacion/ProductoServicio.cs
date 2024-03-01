using AutoMapper;
using Ecommerce.DTO;
using Ecommerce.Modelo;
using Ecommerce.Repositorio.Contrato;
using Ecommerce.Servicio.Contrato;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Servicio.Implementacion
{
    public class ProductoServicio : IProductoServicio
    {


        private readonly IGenericoRepositorio<Producto> _modeloRepositorio;
        private readonly IMapper _mapper;
        public ProductoServicio(IGenericoRepositorio<Producto> modeloRepositorio, IMapper mapper)
        {
            _modeloRepositorio = modeloRepositorio;
            _mapper = mapper;
        }

        public async Task<List<ProductoDTO>> Catalogo(string categoria, string buscar)
        {
            try
            {
                var consulta = _modeloRepositorio.Consultar(p =>
                     p.Nombre.ToLower().Contains(buscar.ToLower()) &&
                     p.IdcategoriaNavigation.Nombre.ToLower().Contains(categoria.ToLower()));


                List<ProductoDTO> lista = _mapper.Map<List<ProductoDTO>>(await consulta.ToListAsync());

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProductoDTO> Crear(ProductoDTO modelo)
        {
            try
            {
                var dbModelo = _mapper.Map<Producto>(modelo);
                var respModelo = await _modeloRepositorio.Crear(dbModelo);

                if (respModelo.Idcategoria != 0)
                {
                    return _mapper.Map<ProductoDTO>(respModelo);
                }
                else
                {
                    throw new TaskCanceledException("No se puede crear");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Editar(ProductoDTO modelo)
        {
            try
            {
                var consulta = _modeloRepositorio.Consultar(p => p.Idproducto == modelo.Idproducto);
                var fromModelo = await consulta.FirstOrDefaultAsync();

                if (fromModelo != null)
                {
                    fromModelo.Nombre = modelo.Nombre;
                    fromModelo.Descripcion = modelo.Descripcion;
                    fromModelo.Idcategoria = modelo.Idcategoria;
                    fromModelo.Precio = modelo.Precio;
                    fromModelo.Preciooferta = modelo.Preciooferta;
                    fromModelo.Cantidad = modelo.Cantidad;
                    fromModelo.Imagen = modelo.Imagen;
                    var resp = await _modeloRepositorio.Editar(fromModelo);

                    if (!resp)
                    {
                        throw new TaskCanceledException("No se puede editar");
                    }
                    return resp;
                }
                else
                {
                    throw new TaskCanceledException("No se encontraron resultados");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Eliminar(int Id)
        {
            try
            {
                var consulta = _modeloRepositorio.Consultar(p => p.Idproducto == Id);
                var fromModelo = await consulta.FirstOrDefaultAsync();

                if (fromModelo != null)
                {
                    var resp = await _modeloRepositorio.Eliminar(fromModelo);
                    if (!resp)
                    {
                        throw new TaskCanceledException("No se pudo eliminar");
                    }
                    return resp;
                }
                else
                {
                    throw new TaskCanceledException("No se encontraron resultados");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ProductoDTO>> Lista(string buscar)
        {

            try
            {
                var consulta = _modeloRepositorio.Consultar(p =>
                p.Nombre.ToLower().Contains(buscar.ToLower())).Include(c=>c.IdcategoriaNavigation);



                List<ProductoDTO> lista = _mapper.Map<List<ProductoDTO>>(await consulta.ToListAsync());

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProductoDTO> Obtener(int id)
        {
            try
            {
                var consulta = _modeloRepositorio.Consultar(p => p.Idproducto == id).Include(c => c.IdcategoriaNavigation);
                var fromModelo = await consulta.FirstOrDefaultAsync();

                if (fromModelo != null)
                {
                    return _mapper.Map<ProductoDTO>(fromModelo);
                }
                throw new TaskCanceledException("No se encontraron coincidencias");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
