namespace RaffleKing.Models
{
	public class RaffleDetails
	{
		public int Id { get; set; }
		public int RD_Raffle_Id { get; set; }
		public int RD_User_Id { get; set; }
		public int RD_Raffle_block { get; set; }
		public string RD_BookedBy { get; set; }
		public bool RD_Booked_Status { get; set; }
		public string RD_Winners { get; set; }
	}
}
