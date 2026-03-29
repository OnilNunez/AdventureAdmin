using AdventureAdmin.Data.Context;
using AdventureAdmin.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AdventureAdmin.Ui.Services;

public class BusinessEntityService(
    AdventureWorksContext context
    ) : IService<Data.Models.BusinessEntity, int>
{
    public async Task<BusinessEntity?> Buscar(int id)
    {
        return await context.BusinessEntities
            .FirstOrDefaultAsync(be => be.BusinessEntityId == id);
    }

    public async Task<bool> Eliminar(int id)
    {
        var businessEntity = await context.BusinessEntities
            .FirstOrDefaultAsync(be => be.BusinessEntityId == id);
        if (businessEntity == null)
        return false;
        context.BusinessEntities.Remove(businessEntity);
        var cantidad = await context.SaveChangesAsync();

        return cantidad > 0;

    }

    public async Task<List<BusinessEntity>> GetList(Expression<Func<BusinessEntity, bool>> criterio)
    {
        return await context.BusinessEntities
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<bool> Guardar(BusinessEntity entidad)
    {
        await context.BusinessEntities.AddAsync(entidad);
        var cantidad = await context.SaveChangesAsync();
        return cantidad > 0;
    }
}
