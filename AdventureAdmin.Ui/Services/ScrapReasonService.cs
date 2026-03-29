using AdventureAdmin.Data.Context;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AdventureAdmin.Ui.Services;

public class ScrapReasonService (
    AdventureWorksContext context)
    : IService<Data.Models.ScrapReason, int>
{
    public async Task<Data.Models.ScrapReason?> Buscar(int id)
    {
        return await context.ScrapReasons
            .FirstOrDefaultAsync(x => x.ScrapReasonId == id);
    }

    public async Task<bool> Eliminar(int id)
    {
        var scrapReason = context.ScrapReasons
            .FirstOrDefault(x => x.ScrapReasonId == id);
        if (scrapReason == null)
            return false;
        context.ScrapReasons.Remove(scrapReason);
        var cantidad = await context.SaveChangesAsync();

        return cantidad > 0;
    }

    public async Task<List<Data.Models.ScrapReason>> GetList(Expression<Func<Data.Models.ScrapReason, bool>> criterio)
    {
        return await context.ScrapReasons
            .Where(criterio)
            .ToListAsync();

    }

    public async Task<bool> Guardar(Data.Models.ScrapReason entidad)
    {
        await context.ScrapReasons.AddAsync(entidad);
        var cantidad = await context.SaveChangesAsync();
        return cantidad > 0;
    }
}
