using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiBanking.DTOClasses;
using WebApiBanking.Models.Context;
using WebApiBanking.Models.Entities;
using WebApiBanking.Presentation;

namespace WebApiBanking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankCardController : ControllerBase
    {
        private readonly IBankCardService _bankCardService;
        private readonly BankCardDbContext _bankCardDbContext;

        public BankCardController(IBankCardService bankCardService, BankCardDbContext bankCardDbContext)
        {
            _bankCardService = bankCardService ?? throw new ArgumentNullException(nameof(bankCardService));
            _bankCardDbContext = bankCardDbContext;
        }

        //public List<BankCardDto> GetAll()
        //{
        //    return _bankCardDbContext.BankCards.Select(x => new BankCardDto
        //    {
        //        CardUserName = x.CardUserName
        //    }).ToList();
        //}

        [HttpPost("validate")]
        public IActionResult ValidateBankCard([FromBody] BankCardInfo cardInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Geçersiz kart bilgileri.");
            }

            bool isValid = _bankCardService.ValidateCard(cardInfo);

            if (isValid)
            {
                return BadRequest("Kart bilgileri geçersiz.");
            }

            //Kartın son kullanma tarihine göre bir kontrol
            string[] expiryDateParts = cardInfo.ExpiryDate.Split('/');
            if (expiryDateParts.Length != 2 ||
                !int.TryParse(expiryDateParts[0], out int month) ||
                !int.TryParse(expiryDateParts[1], out int year))
            {
                return BadRequest("Geçersiz son kullanma tarihi formatı.");
            }

            int currentYear = DateTime.Now.Year % 100; // Son iki hane alınır
            int currentMonth = DateTime.Now.Month;

            if (year < currentYear || (year == currentYear && month < currentMonth))
            {
                return BadRequest("Kartın son kullanma tarihi geçmiş.");
            }

            // CVV'nin uzunluğunu kontrol etme
            if (cardInfo.CVV.Length != 3)
            {
                return BadRequest("Geçersiz CVV uzunluğu.");
            }

            

            return Ok("Kart bilgileri doğrulandı.");
        }

        [HttpGet]
        public ActionResult<IEnumerable<BankCardInfo>> GetBankCards()
        {
            var bankCards = _bankCardService.GetAllBankCards();
            return Ok(bankCards);
        }

        [HttpGet("{id}")]
        public ActionResult<BankCardInfo> GetBankCardId(int id)
        {
            //Banka kartı bilgilerini id'ye göre getirme
            var card = _bankCardService.GetBankCardById(id);

            if (card == null)
            {
                return NotFound("Kart bulunamadı.");
            }

            return Ok(card);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBankCard(int id, [FromBody] BankCardInfo updatedCardInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Geçersiz kart bilgileri.");
            }

            var isUpdated = _bankCardService.UpdateCard(id, updatedCardInfo);

            if (!isUpdated)
            {
                return NotFound("Kart bulunamadı.");
            }

            return Ok("Kart bilgileri güncellendi.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBankCard(int id)
        {
            var isDeleted = _bankCardService.DeleteCard(id);

            if (!isDeleted)
            {
                return NotFound("Kart bulunamadı.");
            }

            return Ok("Kart başarıyla silindi.");
        }
    }
}
