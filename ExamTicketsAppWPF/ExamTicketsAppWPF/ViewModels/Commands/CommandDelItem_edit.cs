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
		private Command editQTSCommand;
		public Command EditQTScommand
		{
			get
			{
				return editQTSCommand = new Command((o) =>
				{
					using (TicketsDbContext db = new TicketsDbContext())
					{
						db.Database.EnsureCreated();
						if (flagQTS == 1)
						{
							Subject? subject = db.subjects.First(s => s.Id == Id);
							subject.SubjectName = textQTS;
							db.Update(subject);
						}
						if (flagQTS == 2)
						{
							Category? category = db.categories.First(p => p.Id == Id);
							category.CategoryName = textQTS;
							db.Update(category);
						}
						if (flagQTS == 4)
						{
							Question? question = db.questions.First(q => q.Id == Id);
							question.ContentQuestion = textQTS;
							db.Update(question);

						}
						if (flagQTS == 5)
						{
							Practice? practice = db.practices.First(p => p.Id == Id);
							practice.ContentPractice = textQTS;
							db.Update(practice);
						}
						db.SaveChanges();
						MessageBox.Show("Изменения сохранены");						
					}
				});
			}
		}
	}
}
