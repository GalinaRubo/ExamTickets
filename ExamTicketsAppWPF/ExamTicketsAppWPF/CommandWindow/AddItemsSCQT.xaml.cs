using System.Windows;
using System.Windows.Controls;
using ExamTicketsAppWPF.ViewModels;

namespace ExamTicketsAppWPF.CommandWindow
{
	/// <summary>
	/// Логика взаимодействия для ItemQuestionWindow.xaml
	/// </summary>
	public partial class ItemQuestionWindow : Window
	{
		readonly MWViewModel mWViwModel = new MWViewModel();
		CommandViewModel commandView  = new CommandViewModel();	
		
		public  ItemQuestionWindow(int flag)
		{
			InitializeComponent();
			
			DataContext = commandView;
			commandView.flagQTS = flag;
			if (flag < 5)
			{
				Format.ItemsSource = mWViwModel._format;
				Subject.ItemsSource = mWViwModel._subject;
				category.ItemsSource = mWViwModel._category;				
			}
		}

		private void Subject_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

			commandView.subjectQTS = (string)Subject.SelectedItem;
		}

		private void Question_TextChanged(object sender, TextChangedEventArgs e)
		{
			commandView.textQTS = (string)Question.Text;
		}

		private void Format_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			commandView.formatQTS = (string)Format.SelectedItem;
		}

		private void ButtonAddQTS_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void category_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			commandView.categoryQTS = (string)category.SelectedItem;
		}
	}
}
