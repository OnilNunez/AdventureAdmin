using AdventureAdmin.Data.Context;
using AdventureAdmin.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AdventureAdmin.Ui.Services;

public class SpecialOfferService (
    AdventureWorksContext context)
    : IService<Data.Models.SpecialOffer, int>
{
    public async Task<SpecialOffer?> Buscar(int id)
    {
        return await context.SpecialOffers
            .FirstOrDefaultAsync(x => x.SpecialOfferId == id);
    }

    public async Task<bool> Eliminar(int id)
    {
        var specialOffer = await context.SpecialOffers
            .FirstOrDefaultAsync(x => x.SpecialOfferId == id);
        if (specialOffer == null)
            return false;
        context.SpecialOffers.Remove(specialOffer);
        var cantidad = await context.SaveChangesAsync();

        return cantidad > 0;
    }

    public async Task<List<SpecialOffer>> GetList(Expression<Func<SpecialOffer, bool>> criterio)
    {
        return await context.SpecialOffers
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<bool> Guardar(SpecialOffer entidad)
    {
        await context.SpecialOffers.AddAsync(entidad);
        var cantidad = await context.SaveChangesAsync();
        return cantidad > 0;
    }
}
