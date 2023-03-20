namespace RaffleKing.Models
{
    public class Winnershow
    {
        public int Id { get; set; }
        public string RaffleName { get; set; }
        public int TIcketNo { get; set; }
        public string WinnerName { get; set; }
        public string Country { get; set; }
        public DateTime DrawnAt { get; set; }  
    }
}
