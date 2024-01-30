using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace TheKnife.Entities.Efos
{
    public class RestaurantRegistrationsEfo
    {
        private string _password = string.Empty;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FlName { get; set; } = string.Empty;
        public int Phone {  get; set; }
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100, ErrorMessage = "A {0} deve ter no mínimo {2} e no máximo {1} caracteres.", MinimumLength = 9)]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{9,}$",
            ErrorMessage = "A {0} deve conter pelo menos 9 caracteres, uma maiúscula, uma minúscula, um número e um caracter especial.")]
        public string Password
        {
            get => _password;
            set
            {
                if (value != null)
                {
                    if (Regex.IsMatch(value, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{9,}$"))
                    {
                        _password = value;
                    }
                    else
                    {
                        throw new ArgumentException("A password não atende aos requisitos de complexidade.");
                    }
                }
            }
        }

        public string RName { get; set; } = string.Empty;
        public string Category {  get; set; } = string.Empty;
        public string Desc {  get; set; } = string.Empty;
        public int RPhone { get; set; }
        public string Location {  get; set; } = string.Empty;
        public string Image {  get; set; } = string.Empty;
        public int NumberOfTables { get; set; }
        public int Capacity { get; set; }
        public string OpeningDays {  get; set; } = string.Empty;
        public decimal AveragePrice {  get; set; }
        public string OpeningHours { get; set; } = string.Empty;
        public string ClosingHours { get; set; } = string.Empty;

    }
}
