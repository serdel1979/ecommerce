using Ecommerce.Modelo;
using Ecommerce.Repositorio.Contrato;
using Ecommerce.Repositorio.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repositorio.Implementacion
{
    public class VentaRepositorio: GenericoRepositorio<Venta>, IVentaRepositorio
    {
        private readonly EcommerceContext _dbContext;

        public VentaRepositorio(EcommerceContext dbContext): base(dbContext) 
        {
            this._dbContext = dbContext;
        }

        public async Task<Venta> Registrar(Venta modelo)
        {
            
            Venta ventaGenerada = new Venta();

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach(Detalleventa dv in modelo.Detalleventa)
                    {
                        Producto prod = _dbContext.Productos.Where(p=>p.Idproducto == dv.Idproducto).FirstOrDefault(); ;
                        prod.Cantidad = prod.Cantidad - dv.Cantidad;

                        _dbContext.Productos.Update(prod);
                    }
                    await _dbContext.SaveChangesAsync();

                    await _dbContext.Ventas.AddAsync(modelo);
                    await _dbContext.SaveChangesAsync();
                    ventaGenerada = modelo;
                    transaction.Commit();

                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return ventaGenerada;
        }
    }
}
