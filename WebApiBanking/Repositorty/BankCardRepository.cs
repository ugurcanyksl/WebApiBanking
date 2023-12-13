using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiBanking.Models.Context;
using WebApiBanking.Models.Entities;
using WebApiBanking.Repositorty.IRepository;

namespace WebApiBanking.Repositorty
{
    public class BankCardRepository : IBankCardRepository
    {
        private readonly BankCardDbContext _dbContext;

        public BankCardRepository(BankCardDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BankCardInfo> GetByIdAsync(int id)
        {
            return await _dbContext.BankCards.FirstOrDefaultAsync(card => card.Id == id);
        }
       
        public async Task AddAsync(BankCardInfo card)
        {
            await _dbContext.BankCards.AddAsync(card);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(BankCardInfo card)
        {
            _dbContext.BankCards.Update(card);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var card = await _dbContext.BankCards.FirstOrDefaultAsync(c => c.Id == id);
            if (card != null)
            {
                _dbContext.BankCards.Remove(card);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<BankCardInfo>> GetAllAsync()
        {
            return await _dbContext.BankCards.ToListAsync();
        }

        
    }
}
