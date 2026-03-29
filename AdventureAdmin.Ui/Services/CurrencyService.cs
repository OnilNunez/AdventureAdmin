using AdventureAdmin.Data.Context;
using AdventureAdmin.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AdventureAdmin.Ui.Services;

public class CurrencyService(
    AdventureWorksContext context) 
    : IService<Data.Models.Currency, string>
{
    public async Task<Currency?> Buscar(string id)
    {
        return await context.Currencies
            .FirstOrDefaultAsync(c => c.CurrencyCode == id);
    }

    public  async Task<bool> Eliminar(string id)
    {
       var currency = context.Currencies
            .FirstOrDefault(c => c.CurrencyCode == id);
        if (currency == null)
            return false;
        context.Currencies.Remove(currency);
        var cantidad =  await context.SaveChangesAsync();

        return cantidad > 0;
        
    }

    public async Task<List<Currency>> GetList(Expression<Func<Currency, bool>> criterio)
    {
        return await context.Currencies
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<bool> Guardar(Currency entidad)
    {
        await context.Currencies.AddAsync(entidad);
        var cantidad = await context.SaveChangesAsync();
        return cantidad > 0;
    }
}
