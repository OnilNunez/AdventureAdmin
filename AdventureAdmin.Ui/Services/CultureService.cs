using AdventureAdmin.Data.Context;
using AdventureAdmin.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AdventureAdmin.Ui.Services;

public class CultureService(
    AdventureWorksContext context
    ) : Aplicada1.Core.IService<Culture, string>
{
    public async Task<Culture?> Buscar(string id)
    {
        return await context.Cultures
            .FirstOrDefaultAsync(c => c.CultureId == id);
    }

    public async Task<bool> Eliminar(string id)
    {
        var culture = context.Cultures
            .FirstOrDefault(c => c.CultureId == id);
        if (culture == null)
            return false;
        context.Cultures.Remove(culture);
        var cantidad = await context.SaveChangesAsync();

        return cantidad > 0;

    }

    public async Task<List<Culture>> GetList(Expression<Func<Culture, bool>> criterio)
    {
        return await context.Cultures
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<bool> Guardar(Culture entidad)
    {
        await context.Cultures.AddAsync(entidad);
        var cantidad = await context.SaveChangesAsync();
        return cantidad > 0;

    }
}
