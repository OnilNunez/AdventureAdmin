using AdventureAdmin.Data.Context;
using AdventureAdmin.Data.Models;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AdventureAdmin.Ui.Services;

public class ContactTypeService(
    AdventureWorksContext context
    ) : IService<Data.Models.ContactType, int>
{
    public async Task<ContactType?> Buscar(int id)
    {
        return await context.ContactTypes
            .FirstOrDefaultAsync(ct => ct.ContactTypeId == id);
    }

    public async Task<bool> Eliminar(int id)
    {
        var contactType = await context.ContactTypes
            .FirstOrDefaultAsync(ct => ct.ContactTypeId == id);
        if (contactType == null)
            return false;
        context.ContactTypes.Remove(contactType);
        var cantidad = await context.SaveChangesAsync();

        return cantidad > 0;
    }

    public async Task<List<ContactType>> GetList(Expression<Func<ContactType, bool>> criterio)
    {
        return await context.ContactTypes
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<bool> Guardar(ContactType entidad)
    {
        await context.ContactTypes.AddAsync(entidad);
        var cantidad = await context.SaveChangesAsync();
        return cantidad > 0;
    }
}
