﻿using Blazored.LocalStorage;
using Blazored.Toast.Services;
using Ecommerce.DTO;
using Ecommerce.WebAssembly.Servicios.Contrato;

namespace Ecommerce.WebAssembly.Servicios.Implementacion
{
    public class CarritoServicio : ICarritoServicio
    {
        private ILocalStorageService _localStorageService;
        private ISyncLocalStorageService _synLocalStorageService;
        private IToastService _toastService;

        public CarritoServicio(ILocalStorageService localStorageService,
            ISyncLocalStorageService _synLocalStorageService,
            IToastService _toastService)
        {
            this._localStorageService = localStorageService;
            this._synLocalStorageService = _synLocalStorageService;
            this._toastService = _toastService;
        }

        public event Action MostrarItems;

        public async Task AgregarCarrito(CarritoDTO modelo)
        {
            try
            {
                var carrito = await _localStorageService.GetItemAsync<List<CarritoDTO>>("carrito");
                if(carrito == null)
                {
                    carrito = new List<CarritoDTO>();
                }
                var encontrado = carrito.FirstOrDefault(c => c.Producto.Idproducto == modelo.Producto.Idproducto);
                if(encontrado != null)
                {
                    carrito.Remove(encontrado);
                }
                carrito.Add(modelo);
                await _localStorageService.SetItemAsync("carrito",carrito);

                if(encontrado != null)
                {
                    _toastService.ShowSuccess("Producto Actualizado");
                }else{
                    _toastService.ShowSuccess("Producto Agregado");
                }

                MostrarItems.Invoke();

            }
            catch (Exception ex)
            {
                _toastService.ShowError("No se pudo agregar al carrito");
            }
        }

        public int CantidadProductos()
        {
            var carrito = _synLocalStorageService.GetItem<List<CarritoDTO>>("carrito");

            return carrito==null ? 0 : carrito.Count();
        }

        public async Task<List<CarritoDTO>> DevolverCarrito()
        {
            var carrito = await _localStorageService.GetItemAsync<List<CarritoDTO>>("carrito");
            if(carrito == null)
            {
                carrito = new List<CarritoDTO>();
            }
            return carrito;
        }

        public async Task EliminarCarrito(int idProducto)
        {
            try
            {
                var carrito = await _localStorageService.GetItemAsync<List<CarritoDTO>>("carrito");
                if(carrito != null)
                {
                    var elemento = carrito.FirstOrDefault(c=>c.Producto.Idproducto == idProducto);
                    if(elemento != null)
                    {
                        carrito.Remove(elemento);
                        await _localStorageService.SetItemAsync("carrito", carrito);
                        MostrarItems.Invoke();
                    }
                }

            }
            catch
            {
                _toastService.ShowError("Error al intentar eliminar");
            }
        }

        public async Task LimpiarCarrito()
        {
            await _localStorageService.RemoveItemAsync("carrito");
            MostrarItems.Invoke();
        }
    }
}
