namespace RaffleKing.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int User_id { get; set; }
        public string User_Email { get; set; }
        public string address { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string cc_name { get; set; }
        public string cc_number { get; set; }
        public string cc_expiration { get; set; }
        public string cc_cvv { get; set; }
        public int blockid { get; set; }
        public int raffleid { get; set; }
        public string winnerName { get; set; } 
        public string eft_name { get; set; }
        public string eft_number { get; set; }
        public string eft_branch { get; set; }
        public string eft_reference { get; set; }
    }
}
