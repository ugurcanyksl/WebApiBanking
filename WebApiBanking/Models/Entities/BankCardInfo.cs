using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiBanking.Models.Entities
{
    public class BankCardInfo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CardUserName { get; set; }
        [Required]
        [StringLength(16, MinimumLength = 16)]
        public string CardNumber { get; set; }
        [Required]
        [RegularExpression(@"^(0[1-9]|1[0-2])/[0-9]{2}$", ErrorMessage = "Geçersiz son kullanma tarihi")]
        public string ExpiryDate { get; set; }
        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string CVV { get; set; }
        public decimal Limit { get; set; }
        public decimal Balance { get; set; }
    }
}
