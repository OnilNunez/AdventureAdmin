using AdventureAdmin.Data.Context;
using AdventureAdmin.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AdventureAdmin.Ui.Services;

public class CountryRegionService(
    AdventureWorksContext context
    ) : IService<Data.Models.CountryRegion, string>
{
    public async Task<CountryRegion?> Buscar(string id)
    {
        return await context.CountryRegions
            .FirstOrDefaultAsync(cr => cr.CountryRegionCode == id);
    }

    public async Task<bool> Eliminar(string id)
    {
        var countryRegion = context.CountryRegions
            .FirstOrDefault(cr => cr.CountryRegionCode == id);
        if (countryRegion == null)
            return false;
        context.CountryRegions.Remove(countryRegion);
        var cantidad = await context.SaveChangesAsync();

        return cantidad > 0;
    }

    public async Task<List<CountryRegion>> GetList(Expression<Func<CountryRegion, bool>> criterio)
    {
        return await context.CountryRegions
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<bool> Guardar(CountryRegion entidad)
    {
        await context.CountryRegions.AddAsync(entidad);
        var cantidad = await context.SaveChangesAsync();
        return cantidad > 0;
    }
}
