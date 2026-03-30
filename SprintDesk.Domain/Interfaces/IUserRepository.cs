using CaseFlow.Domain.Entity;

namespace CaseFlow.Domain.Interfaces;

public interface IUserRepository
{
    Task<List<User>> GetAllUsersAsync();
    Task<User?> GetByIdAsync(Guid id);
    Task<List<User>> GetByOrganizationIdAsync(Guid organizationId);
    Task<User> CreateAsync(User user);
    Task<User?> UpdateAsync(User user);
    Task<bool> DeleteAsync(Guid id);
}
