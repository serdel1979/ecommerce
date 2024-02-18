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
    public class CategoriaServicio : ICategoriaServicio
    {

        private readonly IGenericoRepositorio<Categoria> _modeloRepositorio;
        private readonly IMapper _mapper;
        public CategoriaServicio(IGenericoRepositorio<Categoria> modeloRepositorio, IMapper mapper)
        {
            _mapper = mapper;
            _modeloRepositorio = modeloRepositorio;
        }

        public async Task<CategoriaDTO> Crear(CategoriaDTO modelo)
        {
            try
            {
                var dbModelo = _mapper.Map<Categoria>(modelo);
                var respModelo = await _modeloRepositorio.Crear(dbModelo);

                if (respModelo.Idcategoria != 0)
                {
                    return _mapper.Map<CategoriaDTO>(respModelo);
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

        public async Task<bool> Editar(CategoriaDTO modelo)
        {
            try
            {
                var consulta = _modeloRepositorio.Consultar(p => p.Idcategoria == modelo.Idcategoria);
                var fromModelo = await consulta.FirstOrDefaultAsync();

                if (fromModelo != null)
                {
                    fromModelo.Nombre = modelo.Nombre;
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
                var consulta = _modeloRepositorio.Consultar(p => p.Idcategoria == Id);
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

        public async Task<List<CategoriaDTO>> Lista(string buscar)
        {
            try
            {
                var consulta = _modeloRepositorio.Consultar(p =>
                p.Nombre == buscar);

                List<CategoriaDTO> lista = _mapper.Map<List<CategoriaDTO>>(await consulta.ToListAsync());

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CategoriaDTO> Obtener(int id)
        {
            try
            {
                var consulta = _modeloRepositorio.Consultar(p => p.Idcategoria == id);
                var fromModelo = await consulta.FirstOrDefaultAsync();

                if (fromModelo != null)
                {
                    return _mapper.Map<CategoriaDTO>(fromModelo);
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
