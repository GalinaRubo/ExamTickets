using ExamTicketsAppWPF.CommandWindow;
using ExamTicketsAppWPF.CommandWindow.DelDataDb;
using ExamTicketsAppWPF.ViewModels.Commands;

namespace ExamTicketsAppWPF.ViewModels
{
	internal partial class CommandViewModel : NotifBase
	{
		public Command? addCommand;
		public Command? editCommand;
		public Command? delCommand;


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
	}
}
