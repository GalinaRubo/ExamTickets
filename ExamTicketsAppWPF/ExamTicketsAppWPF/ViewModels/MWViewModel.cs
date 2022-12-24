
using ExamTicketsAppWPF.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ExamTicketsAppWPF.ViewModels
{
	public class MWViewModel : NotifBase
	{
		
		public List<string> _subject { get; set; }
		public List<string> _category { get; set; }
		public List<string> _format { get; set; }


		public MWViewModel()
		{
			using (TicketsDbContext db = new TicketsDbContext())
			{
				db.Database.EnsureCreated();
				var subj = db.subjects.FromSqlRaw("SELECT * FROM Subjects").Select(s=>s.SubjectName).ToList();
				_subject = subj.ConvertAll(x => x.ToString());
				var categ = db.categories.FromSqlRaw("SELECT * FROM Categories").Select(c => c.CategoryName).ToList();
				_category = categ.ConvertAll(y => y.ToString());
				_format = new List<string>() { "TXT", "FILE"};
			}
		}
	}
}

