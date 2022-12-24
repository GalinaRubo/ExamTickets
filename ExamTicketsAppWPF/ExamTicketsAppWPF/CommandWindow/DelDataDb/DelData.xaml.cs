using System.Windows;
using ExamTicketsAppWPF.ViewModels;



namespace ExamTicketsAppWPF.CommandWindow.DelDataDb
{
	/// <summary>
	/// Логика взаимодействия для DelData.xaml
	/// </summary>
	public partial class DelData : Window
	{
		CommandViewModel commandViewModel = new CommandViewModel();
		public DelData()
		{
			InitializeComponent();
		}

		private void DelS_Click(object sender, RoutedEventArgs e)
		{
			DelItemsSCQT delItemsSCQT = new DelItemsSCQT(1);
			delItemsSCQT.ShowDialog();
			Close();
		}

		private void DelC_Click(object sender, RoutedEventArgs e)
		{
			DelItemsSCQT delItemsSCQT = new DelItemsSCQT(2);
			delItemsSCQT.ShowDialog();
			Close();
		}

		private void DelSC_Click(object sender, RoutedEventArgs e)
		{
			DelItemsSCQT delItemsSCQT = new DelItemsSCQT(3);
			delItemsSCQT.ShowDialog();
			Close();
		}

		private void DelQ_Click(object sender, RoutedEventArgs e)
		{
			DelItemsSCQT delItemsSCQT = new DelItemsSCQT(4);
			delItemsSCQT.ShowDialog();
			Close();
		}

		private void DelT_Click(object sender, RoutedEventArgs e)
		{
			DelItemsSCQT delItemsSCQT = new DelItemsSCQT(5);
			delItemsSCQT.ShowDialog();
			Close();
		}

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
