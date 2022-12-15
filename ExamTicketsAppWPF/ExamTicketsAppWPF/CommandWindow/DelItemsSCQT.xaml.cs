using ExamTicketsAppWPF.Models;
using ExamTicketsAppWPF.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ExamTicketsAppWPF.CommandWindow
{
	/// <summary>
	/// Логика взаимодействия для DelItemsSCQT.xaml
	/// </summary>
	public partial class DelItemsSCQT : Window
	{
		List<Subject> subj = new List<Subject>();
		List<Category> categ = new List<Category>();
		List<Question> ques = new List<Question>();
		List<Practice> prac = new List<Practice>();
		
		
		int indexc = 0, indexs = 0;

		int Flag;
		readonly MWViewModel mWViwModel = new MWViewModel();
		CommandViewModel commandView = new CommandViewModel();
		TicketsDbContext _db = new TicketsDbContext();

		public DelItemsSCQT(int flag)
		{
			InitializeComponent();
			DataContext = commandView;
			Flag = flag;
			commandView.flagQTS = flag;
			_db.Database.EnsureCreated();
			subj = _db.subjects.ToList();
			categ = _db.categories.ToList();
			ques = _db.questions.ToList();
			prac = _db.practices.ToList();
					
			if (flag == 1 ) Subject.ItemsSource = mWViwModel._subject;
			if (flag == 2) Category.ItemsSource = mWViwModel._category;
			if (flag == 3)
			{
				Subject.ItemsSource = mWViwModel._subject;
				Category.ItemsSource = mWViwModel._category;
			}
			if (flag == 4)
			{
				Subject.ItemsSource = mWViwModel._subject;
				Category.ItemsSource = mWViwModel._category;
				Format.ItemsSource = mWViwModel._format;
				QT.ItemsSource = _db.questions.Select(s => s.ContentQuestion).ToList();
			}
			if (flag == 5)
			{
				Subject.ItemsSource = mWViwModel._subject;
				Category.ItemsSource = mWViwModel._category;
				Format.ItemsSource = mWViwModel._format;
				QT.ItemsSource = _db.practices.Select(s => s.ContentPractice).ToList();
			}


		}

		private void Subject_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			    commandView.subjectQTS = (string)Subject.SelectedItem;	
				indexs = subj.First(s => s.SubjectName == (string)Subject.SelectedItem).Id;
				if (Flag == 4)
				{ 
				ques = ques.Where(x => x.IdSubject == indexs).ToList();
				QT.ItemsSource = ques.Select(x => x.ContentQuestion).ToList();
				}
				if (Flag == 5)
				{
				prac = prac.Where(x => x.idsubject == indexs).ToList();
				QT.ItemsSource = prac.Select(x => x.ContentPractice).ToList();
				}

		}

		private void Format_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
				commandView.formatQTS = (string)Format.SelectedItem;
				if (Flag == 4)
				{
				ques = ques.Where(x => x.FormatQuestion == (string)Format.SelectedItem).ToList();
				QT.ItemsSource = ques.Select(x => x.ContentQuestion).ToList();
			} 
				if (Flag == 5)
				{
				prac = prac.Where(x => x.FormatPractice == (string)Format.SelectedItem).ToList();
				QT.ItemsSource = prac.Select(x => x.ContentPractice).ToList();
				}

}

		private void Category_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
				commandView.categoryQTS = (string)Category.SelectedItem;
				indexc = categ.First(c => c.CategoryName == (string)Category.SelectedItem).Id;
				if (Flag == 4)
				{
				ques = ques.Where(x => x.IdCategory == indexc).ToList();
				QT.ItemsSource = ques.Select(x => x.ContentQuestion).ToList();
			}
				if (Flag == 5)
				{
				prac = prac.Where(x => x.idcategory == indexc).ToList();
				QT.ItemsSource = prac.Select(x => x.ContentPractice).ToList();
			}
		}

		private void ButtonDelQTSC_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void QT_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
				commandView.textQTS = (string)QT.SelectedItem;
		}
	}
}
