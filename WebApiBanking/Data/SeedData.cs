using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiBanking.Models.Context;
using WebApiBanking.Models.Entities;

namespace WebApiBanking.MySeed
{
    public static class SeedData
    {
        public static void Initialize(BankCardDbContext context)
        {
            if (!context.BankCards.Any())
            {
                context.BankCards.AddRange(
                    new BankCardInfo { CardUserName = "Emre Aşık", CardNumber = "1111222233334444", ExpiryDate = "12/23", CVV = "123", Limit = 30000, Balance = 60000 }
                );
                context.SaveChanges();
            }
        }
    }
}
