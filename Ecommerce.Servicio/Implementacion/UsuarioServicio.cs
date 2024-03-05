using AutoMapper;
using Ecommerce.DTO;
using Ecommerce.Modelo;
using Ecommerce.Repositorio.Contrato;
using Ecommerce.Servicio.Contrato;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
                var consulta = _modeloRepositorio.Consultar(p => p.Correo == modelo.Correo);
                var fromDbModelo = await consulta.FirstOrDefaultAsync();

                if (fromDbModelo != null)
                {
                    // Verificar la contraseña
                    if (VerifyPassword(modelo.Clave, fromDbModelo.Clave))
                    {
                        // La contraseña es correcta, devolver el DTO de sesión
                        return _mapper.Map<SesionDTO>(fromDbModelo);
                    }
                }

                // Si el usuario no existe o la contraseña es incorrecta, lanzar excepción
                throw new TaskCanceledException("No se encontraron coincidencias");
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
                var consulta = _modeloRepositorio.Consultar(p => p.Correo == modelo.Correo);
                var fromDbModelo = await consulta.FirstOrDefaultAsync();

                if (fromDbModelo != null)
                {
                    throw new TaskCanceledException("Esa cuenta de email ya existe!!!");
                }

                modelo.Clave = HashPassword(modelo.Clave);
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

        private string HashPassword(string password)
        {
            // Puedes utilizar un algoritmo de hash seguro, por ejemplo, SHA-256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Convertir la contraseña en bytes
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convertir los bytes del hash a una representación de cadena
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        private bool VerifyPassword(string enteredPassword, string storedHashedPassword)
        {
            // Utilizar el mismo algoritmo de hash (por ejemplo, SHA-256) y comparar los hashes
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Convertir la contraseña proporcionada en bytes
                byte[] enteredBytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(enteredPassword));

                // Convertir los bytes del hash a una representación de cadena
                StringBuilder enteredBuilder = new StringBuilder();
                for (int i = 0; i < enteredBytes.Length; i++)
                {
                    enteredBuilder.Append(enteredBytes[i].ToString("x2"));
                }

                // Comparar los hashes
                return storedHashedPassword.Equals(enteredBuilder.ToString(), StringComparison.OrdinalIgnoreCase);
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
                    modelo.Clave = HashPassword(modelo.Clave);
                    fromModelo.Nombrecompleto = modelo.Nombrecompleto;
                    fromModelo.Correo = modelo.Correo;
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
