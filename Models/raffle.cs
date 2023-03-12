using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaffleKing.Models
{
    public class raffle
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

		[Required]
		public string R_UniqueRaffleCode { get; set; }

		[Required]
		public string R_Title { get; set; }

		[Required]
        public DateTime R_DrawnAt { get; set; }

        [Required]
        public double R_TicketPrice { get; set; }

        [Required]
        public int R_Total_Available { get; set; } 

        [Required]
        public int R_Total_Booked { get; set; }

        [Required]
        public int R_BlockStartFrom { get; set; }

        [Required]
        public string R_ShortDecription { get; set; }

        [Required]
        public string R_FullDecription { get; set; }

        [Required]
        public string R_ThumbnailImage { get; set; }

        [Required]
        public string R_MainImage { get; set; }

        [Required]
        public bool R_BlockGenerated { get; set; }
    }
}
