using Boilerplate.Entities;
using Boilerplate.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Boilerplate.Interfaces
{
    public interface IUserTenantRepository
    {
        Task<IEnumerable<MsUserTenant>> GetAllAsync();
        Task<IEnumerable<MsUserTenant>> GetByUserIdAsync(Guid userId);
        Task<MsUserTenant?> AddAsync(MsUserTenant data);
        Task<bool> DeleteAsync(Guid id);
    }
}