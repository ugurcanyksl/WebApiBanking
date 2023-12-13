using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiBanking.DTOClasses;
using WebApiBanking.Models.Entities;

namespace WebApiBanking.Presentation
{
    public interface IBankCardService
    {
        IEnumerable<BankCardInfo> GetAllBankCards();
        bool ValidateCard(BankCardInfo cardInfo);
        BankCardInfo GetBankCardById(int id);
        bool UpdateCard(int id, BankCardInfo updatedCardInfo);
        bool DeleteCard(int id);
    }
}
