using AdventureAdmin.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AdventureAdmin.Ui.Services;

public class ProductServices (
    AdventureWorksContext context): Aplicada1.Core.IService<Data.Models.Product, int>
{
    public Task<Data.Models.Product?> Buscar(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Eliminar(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Data.Models.Product>> GetList(Expression<Func<Data.Models.Product, bool>> criterio)
    {
        return await context.Products
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<bool> Guardar(Data.Models.Product entidad)
    {
        await context.Products.AddAsync(entidad);
        var cantidad = await context.SaveChangesAsync();
        return cantidad > 0;
    }
}
