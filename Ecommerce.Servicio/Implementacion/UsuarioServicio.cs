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
    public class UsuarioServicio : IUsuarioServicio
    {

        private readonly IGenericoRepositorio<Usuario> _modeloRepositorio;
        private readonly IMapper _mapper;
        public UsuarioServicio(IGenericoRepositorio<Usuario> modeloRepositorio,
           IMapper mapper )
        {
            _modeloRepositorio = modeloRepositorio;
            _mapper = mapper;
        }


        public async Task<SesionDTO> Autorizacion(LoginDTO modelo)
        {
            try
            {
                var consulta = _modeloRepositorio.Consultar(p => p.Correo == modelo.Correo && p.Clave == modelo.Clave);
                var fromDbModelo = await consulta.FirstOrDefaultAsync();

                if (fromDbModelo != null)
                {
                    return _mapper.Map<SesionDTO>(fromDbModelo);
                }
                else
                {
                    throw new TaskCanceledException("No se encontraron coincidencias");
                }


            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UsuarioDTO> Crear(UsuarioDTO modelo)
        {
            try
            {
                var dbModelo = _mapper.Map<Usuario>(modelo);
                var respModelo = await _modeloRepositorio.Crear(dbModelo);

                if(respModelo.Idusuario != 0)
                {
                    return _mapper.Map<UsuarioDTO>(respModelo);
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

        public async Task<bool> Editar(UsuarioDTO modelo)
        {
            try
            {
                var consulta = _modeloRepositorio.Consultar(p=>p.Idusuario == modelo.Idusuario);
                var fromModelo = await consulta.FirstOrDefaultAsync();

                if(fromModelo != null)
                {
                    fromModelo.Nombrecompleto = modelo.Nombrecompleto;
                    fromModelo.Correo = modelo.Correo;
                    fromModelo.Clave = modelo.Clave;
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
                var consulta = _modeloRepositorio.Consultar(p => p.Idusuario == Id);
                var fromModelo = await consulta.FirstOrDefaultAsync();

                if( fromModelo != null)
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

        public async Task<List<UsuarioDTO>> Lista(string rol, string buscar)
        {
            try
            {
                var consulta = _modeloRepositorio.Consultar(p =>
                p.Rol == rol &&
                string.Concat(p.Nombrecompleto.ToLower(), p.Correo.ToLower()).Contains(buscar.ToLower())
                );

                List<UsuarioDTO> lista = _mapper.Map<List<UsuarioDTO>>(await consulta.ToListAsync());

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UsuarioDTO> Obtener(int id)
        {
            try
            {
                var consulta = _modeloRepositorio.Consultar(p => p.Idusuario == id);
                var fromModelo = await consulta.FirstOrDefaultAsync();

                if(fromModelo != null)
                {
                    return _mapper.Map<UsuarioDTO>(fromModelo); 
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
