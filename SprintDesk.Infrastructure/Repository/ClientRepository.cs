using CaseFlow.Domain.Entity;
using CaseFlow.Domain.Interfaces;
using CaseFlow.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CaseFlow.Infrastructure.Repository;

public class ClientRepository : IClientRepository
{
    private readonly AppDbContext _context;

    public ClientRepository(AppDbContext context) => _context = context;

    public async Task<List<Client>> GetAllAsync()
        => await _context.Clients.AsNoTracking().ToListAsync();

    public async Task<Client?> GetByIdAsync(Guid id)
        => await _context.Clients.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

    public async Task<List<Client>> GetByOrganizationIdAsync(Guid organizationId)
        => await _context.Clients.AsNoTracking().Where(c => c.OrganizationId == organizationId).ToListAsync();

    public async Task<Client> CreateAsync(Client client)
    {
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();
        return client;
    }

    public async Task<Client?> UpdateAsync(Client client)
    {
        var existing = await _context.Clients.FindAsync(client.Id);
        if (existing is null) return null;

        existing.Name = client.Name;
        existing.Email = client.Email;
        existing.Phone = client.Phone;
        existing.Address = client.Address;
        existing.OrganizationId = client.OrganizationId;
        existing.UpdatedAt = client.UpdatedAt;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var existing = await _context.Clients.FindAsync(id);
        if (existing is null) return false;

        _context.Clients.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }
}
