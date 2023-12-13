using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiBanking.DTOClasses;
using WebApiBanking.Models.Context;
using WebApiBanking.Models.Entities;
using WebApiBanking.Presentation;

namespace WebApiBanking.Services
{

    public class BankCardService : IBankCardService
    {
        private readonly BankCardDbContext _dbContext;

        public BankCardService(BankCardDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool DeleteCard(int id)
        {
            var card = _dbContext.BankCards.FirstOrDefault(c => c.Id == id);
            if (card == null)
            {
                return false;
            }

            _dbContext.BankCards.Remove(card);
            _dbContext.SaveChanges();
            return true;
        }

        public IEnumerable<BankCardInfo> GetAllBankCards()
        {
            return _dbContext.BankCards.ToList();
        }

        public BankCardInfo GetBankCardById(int id)
        {
            return _dbContext.BankCards.FirstOrDefault(card => card.Id == id);
        }

        public BankCardInfo GetCardById(int id)
        {
            return _dbContext.BankCards.FirstOrDefault(card => card.Id == id);
        }

        public bool UpdateCard(int id, BankCardInfo updatedCardInfo)
        {
            var card = _dbContext.BankCards.FirstOrDefault(c => c.Id == id);
            if (card == null)
            {
                return false;
            }

            card.CardNumber = updatedCardInfo.CardNumber;
            card.ExpiryDate = updatedCardInfo.ExpiryDate;
            card.CVV = updatedCardInfo.CVV;
            card.Limit = updatedCardInfo.Limit;
            card.Balance = updatedCardInfo.Balance;

            _dbContext.SaveChanges();
            return true;
        }

        public bool ValidateCard(BankCardInfo cardInfo)
        {

            if (!IsCardNumberValid(cardInfo.CardNumber))
            {
                return false;
            }

            if (!IsExpiryDateValid(cardInfo.ExpiryDate))
            {
                return false;
            }

            if (!IsCVVValid(cardInfo.CVV))
            {
                return false;
            }

            

            return true;
        }

        private bool IsCardNumberValid(string cardNumber)
        {
            cardNumber = cardNumber.Replace(" ", "").Replace("-", "");

            int sum = 0;
            bool alternate = false;

            for (int i = cardNumber.Length - 1; i >= 0; i--)
            {
                int digit = int.Parse(cardNumber[i].ToString());

                if (alternate)
                {
                    digit *= 2;
                    if (digit > 9)
                    {
                        digit = (digit % 10) + 1;
                    }
                }

                sum += digit;
                alternate = !alternate;
            }

            return sum % 10 == 0;
        }

        private bool IsExpiryDateValid(string expiryDate)
        {
            //Geçerli bir son kullanma tarihi mi değil mi
            DateTime parsedExpiryDate;
            if (!DateTime.TryParse(expiryDate, out parsedExpiryDate))
            {
                return false;
            }

            return parsedExpiryDate > DateTime.Now;
        }

        private bool IsCVVValid(string cvv)
        {
            // CVV doğrulaması
            // CVV'nin 3 veya 4 haneli olup olmadığını kontrol et.
            return cvv.Length == 3 || cvv.Length == 4;
        }
    }
}
