using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ExamTicketsAppWPF.CommandWindow;
using ExamTicketsAppWPF.CommandWindow.DelDataDb;
using ExamTicketsAppWPF.Models;
using ExamTicketsAppWPF.ViewModels.Commands;
using Xceed.Document.NET;
using Image = Xceed.Document.NET.Image;

namespace ExamTicketsAppWPF.ViewModels
{
	public class CommandViewModel : NotifBase
	{
		public Command? addCommand;
		public Command? editCommand;
		public Command? delCommand;
		public Command? madeCommand;
		public Command? addQTSCommand;
		public Command? delQTSCommand;
		public Command? saveQTSCommand;
		public int flagQTS { get; set; }
		public string formatQTS { get; set; }
		public string subjectQTS { get; set; }
		public string categoryQTS { get; set; }
		public string textQTS { get; set; }
		public int Id { get; set; }
		public string Kb { get; set; }
		public string Kq { get; set; }
		public string Kt { get; set; }

		public string Sb { get; set; }
		public string Cb { get; set; }


		public Command Addcommand
		{
			get
			{
				return addCommand = new Command((o) =>
					{
						AddInfoWindow addInfoWindow = new AddInfoWindow();
						addInfoWindow.ShowDialog();
					});
			}
		}
		public Command Delcommand
		{
			get
			{
				return delCommand = new Command((o) =>
				{
					DelData delData = new DelData();
					delData.ShowDialog();
				});
			}
		}
		public Command Editcommand
		{
			get
			{
				return editCommand = new Command((o) =>
				{
					DelItemsSCQT delItemsSCQT = new DelItemsSCQT(0);
					delItemsSCQT.ShowDialog();
				});
			}
		}
		public Command Madecommand
		{
			get
			{
				return madeCommand = new Command((o) =>
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
				});
			}
		}
		public Command AddQTScommand
		{
			get
			{
				return addQTSCommand = new Command((o) =>
				{
					TicketsDbContext db = new TicketsDbContext();
					MWViewModel mwViewModel = new MWViewModel();

					int indexs = 0;
					int indexc = 0;

					if (flagQTS < 5)
					{
						indexc = (db.categories.First(c => c.CategoryName == categoryQTS)).Id;
						indexs = (db.subjects.First(s => s.SubjectName == subjectQTS)).Id;
					}
					switch (flagQTS)
					{
						case 1:

							Question question = new Question { ContentQuestion = textQTS, FormatQuestion = formatQTS, IdSubject = indexs, IdCategory = indexc };
							db.questions.Add(question);
							db.SaveChanges();
							MessageBox.Show("Вопрос сохранен");
							break;
						case 2:

							if (Directory.Exists(textQTS) || File.Exists(textQTS))
							{
								string[] str;
								if (formatQTS == "TXT") str = File.ReadAllLines(textQTS);
								else str = Directory.GetFiles(textQTS);

								foreach (var s in str)
								{
									Question question1 = new Question { ContentQuestion = s, FormatQuestion = formatQTS, IdSubject = indexs, IdCategory = indexc };
									db.questions.Add(question1);
								}
								db.SaveChanges();
								MessageBox.Show("Вопросы сохранены");
							}
							else MessageBox.Show("Путь к файлу/каталогу задан неверно!");
							break;
						case 3:
							Practice practice = new Practice { ContentPractice = textQTS, FormatPractice = formatQTS, idsubject = indexs, idcategory = indexc };
							db.practices.Add(practice);
							db.SaveChanges();
							MessageBox.Show("Задача сохранена");
							break;
						case 4:
							if (Directory.Exists(textQTS) || File.Exists(textQTS))
							{
								string[] str;
								if (formatQTS == "TXT") str = File.ReadAllLines(textQTS);
								else str = Directory.GetFiles(textQTS);
								foreach (var s in str)
								{
									Practice practice1 = new Practice { ContentPractice = s, FormatPractice = formatQTS, idsubject = indexs, idcategory = indexc };
									db.practices.Add(practice1);
								}
								db.SaveChanges();
								MessageBox.Show("Задачи сохранены");
							}
							else MessageBox.Show("Путь к файлу/каталогу задан неверно!");
							break;
						case 5:
							Subject subject = new Subject { SubjectName = textQTS };
							db.subjects.Add(subject);
							db.SaveChanges();
							MessageBox.Show("Предмет сохранен");
							break;
						case 6:
							Category category = new Category { CategoryName = textQTS };
							db.categories.Add(category);
							db.SaveChanges();
							MessageBox.Show("Категория сохранена");
							break;
						default:
							MessageBox.Show("Задание не выбрано");
							break;
					}
				});

			}
		}
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
							MessageBox.Show("Вопросы и задачи выбранных предмета + категори удалены");
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
		public Command SaveQTScommand
		{
			get
			{
				return saveQTSCommand = new Command((o) =>
				{
					using (TicketsDbContext db = new TicketsDbContext())
					{
						db.Database.EnsureCreated();
						if (flagQTS == 1)
						{
							Subject subject = db.subjects.First(s => s.Id == Id);
							subject.SubjectName = textQTS;
							db.Update(subject);
						}
						if (flagQTS == 2)
						{
							Category category = db.categories.First(p => p.Id == Id);
							category.CategoryName = textQTS;
							db.Update(category);
						}
						if (flagQTS == 4)
						{ 
						Question question = db.questions.First(q => q.Id == Id);
							question.ContentQuestion = textQTS;
							db.Update(question);
						
						}
						if (flagQTS == 5)
						{
						Practice practice = db.practices.First(p => p.Id == Id);
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
