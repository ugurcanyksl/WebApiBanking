using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiBanking.Models.Entities;

namespace WebApiBanking.Repositorty.IRepository
{
    public interface IBankCardRepository
    {
        Task<BankCardInfo> GetByIdAsync(int id);
        Task AddAsync(BankCardInfo card);
        Task UpdateAsync(BankCardInfo card);
        Task DeleteAsync(int id);
        Task<IEnumerable<BankCardInfo>> GetAllAsync();
    }
}
