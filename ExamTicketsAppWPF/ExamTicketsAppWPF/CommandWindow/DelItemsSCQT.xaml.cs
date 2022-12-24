using ExamTicketsAppWPF.Models;
using ExamTicketsAppWPF.ViewModels;
using ExamTicketsAppWPF.ViewModels.Commands;
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
		CommandDelItems commandDelItems = new CommandDelItems();
		TicketsDbContext _db = new TicketsDbContext();

		public DelItemsSCQT(int flag)
		{
			InitializeComponent();
			DataContext = commandDelItems;
			Flag = flag;
			CommandDelItems.flagQTS = flag;
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
				CommandDelItems.subjectQTS = (string)Subject.SelectedItem;
				if (Flag == 0)
				{
				CommandDelItems.Id = indexs;
				CommandDelItems.flagQTS = 1;
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
				CommandDelItems.formatQTS = (string)Format.SelectedItem;
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
				CommandDelItems.categoryQTS = (string)Category.SelectedItem;
			if (Flag == 0)
				{
				CommandDelItems.Id = indexc;
				CommandDelItems.flagQTS = 2;
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
				CommandDelItems.Id = indexq;
				Edit.Text = (string)Q.SelectedItem;
				CommandDelItems.flagQTS = 4;
			}
			CommandDelItems.textQTS = (string)Q.SelectedItem;
		}

		private void ButtonSaveQTSC_Click(object sender, RoutedEventArgs e)
		{
			CommandDelItems.textQTS = Edit.Text;
			Edit.Text = String.Empty;

		}

		private void Edit_TextChanged(object sender, TextChangedEventArgs e)
		{

		}

		private void T_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			indexp = prac.First(c => c.ContentPractice == (string?)T.SelectedItem).Id;
			if (Flag == 0)
			{
				CommandDelItems.Id = indexp;
				Edit.Text = (string)T.SelectedItem;
				CommandDelItems.flagQTS = 5;
			}
			CommandDelItems.textQTS = (string)T.SelectedItem;
		}
	}
}
