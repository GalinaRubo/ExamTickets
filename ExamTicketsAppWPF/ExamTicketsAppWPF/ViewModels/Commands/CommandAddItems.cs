using ExamTicketsAppWPF.Models;
using System.IO;
using System.Linq;
using System.Windows;

namespace ExamTicketsAppWPF.ViewModels.Commands
{
	internal class CommandAddItems : NotifBase
	{
		private Command addQTSCommand;
		public static int flagQTS { get; set; }
		public static string? formatQTS { get; set; }
		public static string? subjectQTS { get; set; }
		public static string? categoryQTS { get; set; }
		public static string? textQTS { get; set; }
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
							if (db.questions.Count(s => s.ContentQuestion == textQTS) == 0)
							{
								Question question = new Question { ContentQuestion = textQTS, FormatQuestion = formatQTS, IdSubject = indexs, IdCategory = indexc };
								db.questions.Add(question);
								db.SaveChanges();
								MessageBox.Show("Вопрос сохранен");
							}
							else MessageBox.Show("Вопрос существует");
							break;
						case 2:

							if (Directory.Exists(textQTS) || File.Exists(textQTS))
							{
								string[] str;
								if (formatQTS == "TXT") str = File.ReadAllLines(textQTS);
								else str = Directory.GetFiles(textQTS);

								foreach (var s in str)
								{
									if (db.questions.Count(q => q.ContentQuestion == s) == 0)
									{
										Question question1 = new Question { ContentQuestion = s, FormatQuestion = formatQTS, IdSubject = indexs, IdCategory = indexc };
										db.questions.Add(question1);
									}
									else MessageBox.Show("Вопрос" + s + "существует");
								}
								db.SaveChanges();
								MessageBox.Show("Новые вопросы добавлены");
							}
							else MessageBox.Show("Путь к файлу/каталогу задан неверно!");
							break;
						case 3:
							if (db.practices.Count(p => p.ContentPractice == textQTS) == 0)
							{
								Practice practice = new Practice { ContentPractice = textQTS, FormatPractice = formatQTS, idsubject = indexs, idcategory = indexc };
								db.practices.Add(practice);
								db.SaveChanges();
								MessageBox.Show("Задача сохранена");
							}
							else
								MessageBox.Show("Задача существует");
							break;
						case 4:
							if (Directory.Exists(textQTS) || File.Exists(textQTS))
							{
								string[] str;
								if (formatQTS == "TXT") str = File.ReadAllLines(textQTS);
								else str = Directory.GetFiles(textQTS);
								foreach (var s in str)
								{
									if (db.practices.Count(p => p.ContentPractice == s) == 0)
									{
										Practice practice1 = new Practice { ContentPractice = s, FormatPractice = formatQTS, idsubject = indexs, idcategory = indexc };
										db.practices.Add(practice1);
									}
									else
										MessageBox.Show("Задача" + s + "существует");
								}
								db.SaveChanges();
								MessageBox.Show("Новые задачи добавлены");
							}
							else MessageBox.Show("Путь к файлу/каталогу задан неверно!");
							break;
						case 5:
							if (db.subjects.Count(s => s.SubjectName == textQTS) == 0)
							{
								Subject subject = new Subject { SubjectName = textQTS };
								db.subjects.Add(subject);
								db.SaveChanges();
								MessageBox.Show("Предмет сохранен");
							}
							else
								MessageBox.Show("Предмет существует");
							break;
						case 6:
							if (db.categories.Count(s => s.CategoryName == textQTS) == 0)
							{
								Category category = new Category { CategoryName = textQTS };
								db.categories.Add(category);
								db.SaveChanges();
								MessageBox.Show("Категория сохранена");
							}
							else MessageBox.Show("Категория существует");
							break;
						default:
							MessageBox.Show("Задание не выбрано");
							break;
					}
				});

			}
		}
	}
}
