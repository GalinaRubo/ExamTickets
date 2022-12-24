using ExamTicketsAppWPF.Models;
using ExamTicketsAppWPF.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Xceed.Document.NET;
using Image = Xceed.Document.NET.Image;


namespace ExamTicketsAppWPF.ViewModels
{
	internal partial class CommandViewModel : NotifBase
	{
		public Command? createCommand;

		public string? Kb { get; set; }
		public string? Kq { get; set; }
		public string? Kt { get; set; }

		public string? Sb { get; set; }
		public string? Cb { get; set; }
		public Command Createcommand
		{
			get
			{
				return createCommand = new Command((o) =>
				{
					if (Sb != null && Cb != null)
					{
						using (TicketsDbContext db = new TicketsDbContext())
						{
							db.Database.EnsureCreated();
							var indexc = (db.categories.First(c => c.CategoryName == Cb)).Id;
							var indexs = (db.subjects.First(s => s.SubjectName == Sb)).Id;
							var quest = db.questions.Where(x => x.IdSubject == indexs && x.IdCategory == indexc).ToList();
							var prac = db.practices.Where(a => a.idsubject == indexs && a.idcategory == indexc).ToList();
							int kb = 1;
							int kbr = 0;
							string path = @"E:\SQL\ExamTickets.docx";
							var doc = Xceed.Words.NET.DocX.Create(path);
							if (quest.Count == 0) MessageBox.Show("Вопросов с таким предметом и категорией в базе данных нет");
							else
							{
								kbr = quest.Count - Convert.ToInt32(Kb) * Convert.ToInt32(Kq);
								if (kbr < 0)
								{
									Kb = (quest.Count / Convert.ToInt32(Kq)).ToString();
									Console.Write("Количество билетов может быть только " + Kb + "\n");
								}
							};
							if (prac.Count == 0) MessageBox.Show("Задач с таким предметом и категорией в базе данных нет");
							else
							{
								kbr = prac.Count - Convert.ToInt32(Kb) * Convert.ToInt32(Kt);
								if (kbr < 0)
								{
									Kb = (prac.Count / Convert.ToInt32(Kt)).ToString();
									Console.Write("Количество билетов может быть только " + Kb + "\n");
								}
							};

							while (kb <= Convert.ToInt32(Kb))
							{
								Paragraph headline = doc.InsertParagraph($"Билет № {kb}" + "\n\n");
								headline.Color(System.Drawing.Color.Red);
								headline.Bold();

								if (Kq != "0")
								{

									int i = 0;
									List<string> dellistq = new List<string>();
									var tasksq = new Task[quest.Count()];
									foreach (var q in quest)
									{
										tasksq[i] = Task.Run(() =>
										{
											Paragraph headlineq = doc.InsertParagraph($"Вопрос № {i + 1}");
											headlineq.Color(System.Drawing.Color.Gray);
											headlineq.Bold();
											if (q.FormatQuestion == "TXT") doc.InsertParagraph("\n" + q.ContentQuestion + "\n");
											else
											{
												Image image = doc.AddImage(q.ContentQuestion);
												Picture picture = image.CreatePicture();
												Paragraph p1 = doc.InsertParagraph();
												p1.AppendPicture(picture);
											}
										});
										tasksq[i].Wait();
										dellistq.Add(q.ContentQuestion);
										if (++i == Convert.ToInt32(Kq)) break;
									}
									foreach (var del in dellistq)
									{
										quest.RemoveAll(x => x.ContentQuestion == del);

									}
								};



								if (Kt != "0")
								{

									int i = 0;
									var tasksp = new Task[prac.Count()];
									List<string> dellistp = new List<string>();
									foreach (var p in prac)
									{
										tasksp[i] = Task.Run(() =>
										{
											Paragraph headlinep = doc.InsertParagraph($"Задача № {i + 1}");
											headlinep.Color(System.Drawing.Color.Gray);
											headlinep.Bold();
											if (p.FormatPractice == "TXT") doc.InsertParagraph("\n" + p.ContentPractice + "\n");
											else
											{
												Image image = doc.AddImage(p.ContentPractice);
												Picture picture = image.CreatePicture();
												Paragraph p1 = doc.InsertParagraph();
												p1.AppendPicture(picture);
											}
										});
										tasksp[i].Wait();
										dellistp.Add(p.ContentPractice);
										if (++i == Convert.ToInt32(Kt)) break;
									}
									foreach (var del in dellistp)
									{
										prac.RemoveAll(x => x.ContentPractice == del);
									}
								}
								doc.InsertParagraph($"--------------------------------------------------------------------------------------------------------------------------------\f");
								kb++;
							}

							doc.Save();
						}
						if (Kb != "0")
							MessageBox.Show("Билеты сформированы");
						return;
					}
					if (Sb == null) MessageBox.Show("Введите предмет билетов");
					if (Cb == null) MessageBox.Show("Введите категорию билетов");
					return;
				});
			}
		}
	}
}
