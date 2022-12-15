namespace ExamTicketsAppWPF.Models
{
	public class Question : NotifBase
	{
		public int Id { get; set; }
		public string? ContentQuestion { get; set; }
		public string FormatQuestion { get; set; }
		public int IdSubject { get; set; }
		public int IdCategory { get; set; }

	}
}
