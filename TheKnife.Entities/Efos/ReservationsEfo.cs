using System.ComponentModel.DataAnnotations.Schema;

namespace TheKnife.Entities.Efos
{
    public class ReservationsEfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public int PhoneNumber { get; set; }
        public DateOnly ReservationDate { get; set; }
        public string ReservationTime { get; set; } = string.Empty;
        public int NumberPeople { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Restaurant_Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int User_Id { get; set; }

        public RestaurantsEfo Restaurants { get; set; }
        public UsersEfo Users { get; set; }
    }
}
