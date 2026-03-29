using AdventureAdmin.Data.Context;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AdventureAdmin.Ui.Services;

public class ProductService (
    AdventureWorksContext context)
    : IService<Data.Models.Product, int>
{
    public Task<Data.Models.Product?> Buscar(int id)
    {
        return context.Products
            .FirstOrDefaultAsync(p => p.ProductId == id);
    }

    public async Task<bool> Eliminar(int id)
    {
        var product = context.Products
            .FirstOrDefault(p => p.ProductId == id);
        if (product is null)
            return false;
        var cantidad = await context.SaveChangesAsync();
        return cantidad > 0;

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
