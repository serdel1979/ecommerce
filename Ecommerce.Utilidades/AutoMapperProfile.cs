using AutoMapper;
using Ecommerce.DTO;
using Ecommerce.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Utilidades
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {

            CreateMap<Usuario, UsuarioDTO>();

            CreateMap<Usuario, SesionDTO>();

            CreateMap<UsuarioDTO, Usuario>();

            CreateMap<Categoria, CategoriaDTO>();

            CreateMap<CategoriaDTO, Categoria>();


            CreateMap<Producto, ProductoDTO>();
            CreateMap<ProductoDTO, Producto>().ForMember(destino =>
                destino.IdcategoriaNavigation,
                opt => opt.Ignore()
            );

            CreateMap<Detalleventa, DetalleVentaDTO>();

            CreateMap<DetalleVentaDTO, Detalleventa>();


            CreateMap<Venta, VentaDTO>();

            CreateMap<VentaDTO, Venta>();





        }

    }
}
