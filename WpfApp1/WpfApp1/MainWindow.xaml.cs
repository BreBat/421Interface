using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;



namespace WpfApp1
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			App.Current.Resources.Add("SQLsetup", false);

			InitializeComponent();

			pageFrame.Content = new PageMWBooksIndex();
		}
		private void Window_Activated(object sender, EventArgs e)
		{
			if (App.Current.Resources["SQLsetup"].Equals(false))
			{
				if (DBInterface.instance.validSetup() == true)
				{
					App.Current.Resources["SQLsetup"] = true;
					AddBook.IsEnabled = true;
					AddCopy.IsEnabled = true;
					AddMember.IsEnabled = true;
					RmBook.IsEnabled = true;
					RmCopy.IsEnabled = true;
					RemoveMember.IsEnabled = true;
					pageFrame.Refresh();
				}
				else
				{

				}
			}
		}

		//Utility Method for testing
		private void printDataTable(DataTable data)
		{
			string result = "";
			for (int i = 0; i < data.Rows.Count; i++)
			{
				for (int j = 0; j < data.Columns.Count; j++)
				{
					result += data.Rows[i].ItemArray[j].ToString() + " ";
				}
				result += "\n\n";
			}
			MessageBox.Show(result);
		}

		//FILE MENU OPTIONS////////////////////////////////////////////////////

		private void FileExit_Click(object sender, RoutedEventArgs e)
		{
			App.Current.Shutdown();
		}

		//MANAGE MENU OPTIONS//////////////////////////////////////////////////
		private void SetSQLServer_Click(object sender, RoutedEventArgs e)
		{
			App.Current.MainWindow.IsEnabled = false;
			DBServerSetWindow SQLsetWindow = new DBServerSetWindow();
			SQLsetWindow.Show();
		}

		private void AddBook_Click(object sender, RoutedEventArgs e)
		{
			App.Current.MainWindow.IsEnabled = false;
			DBupdateWindow addBookWindow = new DBupdateWindow();
			addBookWindow.updateType.Content = new PageAddBook();
			addBookWindow.Show();
		}

		private void AddCopy_Click(object sender, RoutedEventArgs e)
		{
			DataTable ye = DBInterface.instance.DatabaseQuery("SELECT * FROM BOOK");
			printDataTable(ye);
		}

		private void AddMember_Click(object sender, RoutedEventArgs e)
		{

		}

		private void RmBook_Click(object sender, RoutedEventArgs e)
		{

		}

		private void RmCopy_Click(object sender, RoutedEventArgs e)
		{

		}

		private void RemoveMember_Click(object sender, RoutedEventArgs e)
		{

		}

		//HELP MENU OPTIONS////////////////////////////////////////////////////

		private void About_Click(object sender, RoutedEventArgs e)
		{

		}

		private void Greytip_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("The program must be told the name of the local SQL server. Use \"Manage/Set SQL server name.\"", "Help", MessageBoxButton.OK, MessageBoxImage.Information);
		}

		//NAVIGATION BUTTONS///////////////////////////////////////////////////

		private void NavBookIndex_Click(object sender, RoutedEventArgs e)
		{
			pageFrame.Content = new PageMWBooksIndex();
		}

		private void NavMemberLookup_Click(object sender, RoutedEventArgs e)
		{
			pageFrame.Content = new PageMWMemberLookup();
		}

	}
}
