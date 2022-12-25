using ExamTicketsAppWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ExamTicketsAppWPF.ViewModels.Commands
{
	public partial class CommandDelItems : NotifBase
	{
		private Command? delQTSCommand;
		public static int Id { get; set; }
		public static int flagQTS { get; set; }
		public static string? formatQTS { get; set; }
		public static string? subjectQTS { get; set; }
		public static string? categoryQTS { get; set; }
		public static string? textQTS { get; set; }
		public Command DelQTScommand
		{
			get
			{
				return delQTSCommand = new Command((o) =>
				{
					MessageBox.Show("flag = " + Convert.ToInt32(flagQTS) + "\nformat =  " + formatQTS + "\nsubject = " + subjectQTS + "\ncategory = " + categoryQTS + "\ntext = " + textQTS);
					TicketsDbContext db = new TicketsDbContext();
					MWViewModel mwViewModel = new MWViewModel();

					switch (flagQTS)
					{
						case 1:
							//удаление предмета и всех вопросов и задач этого предмета
							int indexs = (db.subjects.First(s => s.SubjectName == subjectQTS)).Id;
							foreach (var q in db.questions)
								if (q.IdSubject == indexs) db.questions.Remove(q);
							foreach (var p in db.practices)
								if (p.idsubject == indexs) db.practices.Remove(p);
							db.subjects.Remove(db.subjects.First(s => s.Id == indexs));
							db.SaveChanges();
							MessageBox.Show("Предмет удален");
							break;
						//удаление категории и всех вопросов и задач этой категории
						case 2:
							int indexc = (db.categories.First(c => c.CategoryName == categoryQTS)).Id;
							foreach (var q in db.questions)
								if (q.IdCategory == indexc) db.questions.Remove(q);
							foreach (var p in db.practices)
								if (p.idsubject == indexc) db.practices.Remove(p);
							db.categories.Remove(db.categories.First(c => c.Id == indexc));
							db.SaveChanges();
							MessageBox.Show("Категория удалена");
							break;
						//удаление вопросов и задач выбранных предмета + категория
						case 3:
							int index_s = (db.subjects.First(s => s.SubjectName == subjectQTS)).Id;
							int index_c = (db.categories.First(c => c.CategoryName == categoryQTS)).Id;
							foreach (var q in db.questions)
								if (q.IdSubject == index_s && q.IdCategory == index_c) db.questions.Remove(q);
							foreach (var p in db.practices)
								if (p.idsubject == index_s && p.idcategory == index_c) db.practices.Remove(p);
							db.SaveChanges();
//							MessageBox.Show("Вопросы и задачи выбранных предмета + категори удалены");
							break;
						case 4:
							db.questions.Remove(db.questions.First(c => c.ContentQuestion == textQTS));
							db.SaveChanges();
							MessageBox.Show("Вопрос удален");
							break;
						case 5:
							db.practices.Remove(db.practices.First(c => c.ContentPractice == textQTS));
							db.SaveChanges();
							MessageBox.Show("Задача удалена");
							break;
					}
				});
			}
		}

	}
}
