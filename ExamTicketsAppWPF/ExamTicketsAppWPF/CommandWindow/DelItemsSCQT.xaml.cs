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
		
		
		int indexc = 0, indexs = 0, indexq = 0, indexp = 0;

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

			if (Flag == 0)
			{
				Subject.ItemsSource = mWViwModel._subject;
				Category.ItemsSource = mWViwModel._category;				
				Q.ItemsSource = _db.questions.Select(s => s.ContentQuestion).ToList();
				T.ItemsSource = _db.practices.Select(s => s.ContentPractice).ToList();
				Del.IsEnabled = false;
			}
			else Save.IsEnabled = false;

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
				Q.ItemsSource = _db.questions.Select(s => s.ContentQuestion).ToList();
			}
			if (flag == 5)
			{
				Subject.ItemsSource = mWViwModel._subject;
				Category.ItemsSource = mWViwModel._category;
				Format.ItemsSource = mWViwModel._format;
				T.ItemsSource = _db.practices.Select(s => s.ContentPractice).ToList();
			}


		}

		private void Subject_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			   	
				indexs = subj.First(s => s.SubjectName == (string?)Subject.SelectedItem).Id;
				commandView.subjectQTS = (string)Subject.SelectedItem;
				if (Flag == 0)
				{
				commandView.Id = indexs;
				commandView.flagQTS = 1;
				Edit.Text = (string)Subject.SelectedItem;
			}
				if (Flag == 4)
				{ 
				ques = ques.Where(x => x.IdSubject == indexs).ToList();
				Q.ItemsSource = ques.Select(x => x.ContentQuestion).ToList();
				}
				if (Flag == 5)
				{
				prac = prac.Where(x => x.idsubject == indexs).ToList();
				T.ItemsSource = prac.Select(x => x.ContentPractice).ToList();
				}

		}

		private void Format_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
				commandView.formatQTS = (string)Format.SelectedItem;
				if (Flag == 4)
				{
				ques = ques.Where(x => x.FormatQuestion == (string)Format.SelectedItem).ToList();
				Q.ItemsSource = ques.Select(x => x.ContentQuestion).ToList();
				} 
				if (Flag == 5)
				{
				prac = prac.Where(x => x.FormatPractice == (string)Format.SelectedItem).ToList();
				T.ItemsSource = prac.Select(x => x.ContentPractice).ToList();
				}

}

		private void Category_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
				
				indexc = categ.First(c => c.CategoryName == (string?)Category.SelectedItem).Id;
				commandView.categoryQTS = (string)Category.SelectedItem;
			if (Flag == 0)
				{
					commandView.Id = indexc;
					commandView.flagQTS = 2;
				    Edit.Text = (string)Category.SelectedItem;
			}
				if (Flag == 4)
				{
				ques = ques.Where(x => x.IdCategory == indexc).ToList();
				Q.ItemsSource = ques.Select(x => x.ContentQuestion).ToList();
				}
				if (Flag == 5)
				{
				prac = prac.Where(x => x.idcategory == indexc).ToList();
				T.ItemsSource = prac.Select(x => x.ContentPractice).ToList();
				}
		}

		private void ButtonDelQTSC_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void ButtonCancalQTSC_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void Q_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			indexq = ques.First(c => c.ContentQuestion == (string?)Q.SelectedItem).Id;
			
			if (Flag == 0)
			{
				commandView.Id = indexq;
				Edit.Text = (string)Q.SelectedItem;
				commandView.flagQTS = 4;
			}
			commandView.textQTS = (string)Q.SelectedItem;
		}

		private void ButtonSaveQTSC_Click(object sender, RoutedEventArgs e)
		{
			commandView.textQTS = Edit.Text;
		}

		private void Edit_TextChanged(object sender, TextChangedEventArgs e)
		{

		}

		private void T_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			indexp = prac.First(c => c.ContentPractice == (string?)T.SelectedItem).Id;
			if (Flag == 0)
			{
				commandView.Id = indexp;
				Edit.Text = (string)T.SelectedItem;				
				commandView.flagQTS = 5;
			}
			commandView.textQTS = (string)T.SelectedItem;
		}
	}
}
