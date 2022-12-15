namespace ExamTicketsAppWPF.Models
{
	public class Practice : NotifBase
	{
		public int Id { get; set; }
		public string? ContentPractice { get; set; }
		public string FormatPractice { get; set; }
		public int idsubject { get; set; }
		public int idcategory { get; set; }

	}
}
