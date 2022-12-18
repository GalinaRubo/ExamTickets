using System.Windows;
using ExamTicketsAppWPF.ViewModels;
namespace ExamTicketsAppWPF.CommandWindow
{
	/// <summary>
	/// Логика взаимодействия для AddInfo.xaml
	/// </summary>
	public partial class AddInfoWindow : Window
	{
	    CommandViewModel commandViewModel = new CommandViewModel();
		public AddInfoWindow()
		{
			InitializeComponent();
		}

		private void ButtonAddQ_Click(object sender, RoutedEventArgs e)
		{
			ItemQuestionWindow itemQuestionWindow = new ItemQuestionWindow(1);
			itemQuestionWindow.ShowDialog();
			Close();
		}

		private void AddQs_Click(object sender, RoutedEventArgs e)
		{
			ItemQuestionWindow itemQuestionWindow = new ItemQuestionWindow(2);
			itemQuestionWindow.ShowDialog();		
			Close();
		}

		private void AddT_Click(object sender, RoutedEventArgs e)
		{
			ItemQuestionWindow itemQuestionWindow = new ItemQuestionWindow(3);
			itemQuestionWindow.ShowDialog();
			Close();
		}

		private void AddTs_Click(object sender, RoutedEventArgs e)
		{
			ItemQuestionWindow itemQuestionWindow = new ItemQuestionWindow(4);
			itemQuestionWindow.ShowDialog();
			Close();
		}

		private void AddS_Click(object sender, RoutedEventArgs e)
		{
			ItemQuestionWindow itemQuestionWindow = new ItemQuestionWindow(5);
			itemQuestionWindow.ShowDialog();
			Close();
		}

		private void AddC_Click(object sender, RoutedEventArgs e)
		{
			ItemQuestionWindow itemQuestionWindow = new ItemQuestionWindow(6);
			itemQuestionWindow.ShowDialog();
			Close();
		}

		private void Can_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
