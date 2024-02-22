﻿using Ecommerce.DTO;

namespace Ecommerce.WebAssembly.Servicios.Contrato
{
    public interface IProductoServicio
    {
        Task<ResponseDTO<List<ProductoDTO>>> Lista(string buscar);

        Task<ResponseDTO<ProductoDTO>> Obtener(int id);

        Task<ResponseDTO<List<ProductoDTO>>> Catalogo(string categoria, string buscar);

        Task<ResponseDTO<ProductoDTO>> Crear(ProductoDTO modelo);

        Task<ResponseDTO<bool>> Eliminar(int id);
        Task<ResponseDTO<bool>> Editar(ProductoDTO modelo);
    }
}
