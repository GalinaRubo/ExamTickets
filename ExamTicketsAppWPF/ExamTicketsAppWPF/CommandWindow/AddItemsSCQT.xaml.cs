using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ExamTicketsAppWPF.ViewModels;
using ExamTicketsAppWPF.ViewModels.Commands;

namespace ExamTicketsAppWPF.CommandWindow
{
	/// <summary>
	/// Логика взаимодействия для ItemQuestionWindow.xaml
	/// </summary>
	public partial class ItemQuestionWindow : Window
	{
		readonly MWViewModel mWViwModel = new MWViewModel();
		CommandAddItems commandAddItems = new CommandAddItems();

		public  ItemQuestionWindow(int flag)
		{
			InitializeComponent();
			DataContext = commandAddItems;
			CommandAddItems.flagQTS = flag;
			if (flag < 5)
			{
				Format.ItemsSource = mWViwModel._format;
				Subject.ItemsSource = mWViwModel._subject;
				category.ItemsSource = mWViwModel._category;				
			}
		}

		private void Subject_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

			CommandAddItems.subjectQTS = (string)Subject.SelectedItem;
		}

		private void Question_TextChanged(object sender, TextChangedEventArgs e)
		{
			CommandAddItems.textQTS = (string)Question.Text;
		}

		private void Format_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			CommandAddItems.formatQTS = (string)Format.SelectedItem;
		}

		private void ButtonAddQTS_Click(object sender, RoutedEventArgs e)
		{
		
		}

		private void category_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			CommandAddItems.categoryQTS = (string)category.SelectedItem;
		}

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
			AddInfoWindow newWindow = new AddInfoWindow();
			Application.Current.MainWindow = newWindow;
			newWindow.Show();
			this.Close();
		}

		private void ButtonClean_Click(object sender, RoutedEventArgs e)
		{
			Question.Text = "";			
		}
	}
}
