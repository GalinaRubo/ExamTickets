using System.Windows;
using System.Windows.Controls;
using ExamTicketsAppWPF.ViewModels;
using ExamTicketsAppWPF.ViewModels.Commands;
using System.Windows.Input;

namespace ExamTicketsAppWPF
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		MWViewModel mwViewModel = new MWViewModel();
		CommandViewModel commandView = new CommandViewModel();
		public MainWindow()
		{
			InitializeComponent();
			DataContext = commandView;
			Subject.ItemsSource = mwViewModel._subject;
			Category.ItemsSource = mwViewModel._category;
		}

		private void Category_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

			commandView.Cb = (string)Category.SelectedItem;
		}

		private void kb_TextChanged(object sender, TextChangedEventArgs e)
		{
			
		}

		private void kq_OnKeyDownHandler(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				if (kq.Text != null) commandView.Kq = kq.Text;
				else commandView.Kq = "0";
				kt.Focus();
			}

		}

		private void kb_OnKeyDownHandler(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				if (kb.Text != null) commandView.Kb = kb.Text;
				else commandView.Kb = "0";
				kq.Focus();
			}

		}

		private void kt_OnKeyDownHandler(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				if (kt.Text != null) commandView.Kt = kt.Text;
				else commandView.Kt = "0";
				create.Focus();
			}

		}

		private void kq_TextChanged(object sender, TextChangedEventArgs e)
		{

		}

		private void kt_TextChanged(object sender, TextChangedEventArgs e)
		{

		}

		private void ButtonAdd_Click(object sender, RoutedEventArgs e)
		{

		}

		private void ButtonDel_Click(object sender, RoutedEventArgs e)
		{

		}

		private void ButtonEdit_Click(object sender, RoutedEventArgs e)
		{

		}

		private void ButtonMade_Click(object sender, RoutedEventArgs e)
		{
			kb.Text = string.Empty;
			kq.Text = string.Empty;
			kt.Text = string.Empty;

		}

		private void Subject_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

			commandView.Sb = (string)Subject.SelectedItem;
		}

		private void ButtonExit_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
