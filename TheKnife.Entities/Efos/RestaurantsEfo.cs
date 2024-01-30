using System.ComponentModel.DataAnnotations.Schema;

namespace TheKnife.Entities.Efos
{
    public class RestaurantsEfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string RName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Desc { get; set; } = string.Empty;
        public int RPhone { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public int NumberOfTables { get; set; }
        public int Capacity { get; set; }
        public string OpeningDays { get; set; } = string.Empty;
        public decimal AveragePrice { get; set; }
        public string OpeningHours { get; set; } = string.Empty;
        public string ClosingHours { get; set; } = string.Empty;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RestaurantRegistration_Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Rresponsible_Id { get; set; }

        public RestaurantRegistrationsEfo RestaurantRegistrations { get; set; }
        public RestaurantResponsiblesEfo RestaurantResponsibles { get; set; }
    }
}
