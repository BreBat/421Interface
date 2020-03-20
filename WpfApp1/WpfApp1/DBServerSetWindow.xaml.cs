﻿using System;
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
using System.Windows.Shapes;

namespace WpfApp1
{
	/// <summary>
	/// Interaction logic for DBServerSetWindow.xaml
	/// </summary>
	public partial class DBServerSetWindow : Window
	{
		public DBServerSetWindow()
		{
			InitializeComponent();
		}

		private void Window_Closed(object sender, EventArgs e)
		{
			App.Current.MainWindow.IsEnabled = true;
			if (DBInterface.instance.validSetup() == true)
			{
				//App.Current.Resources["SQLsetup"] = true;
			}
		}

		private void Cancel_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void ok_Click(object sender, RoutedEventArgs e)
		{
			ok.Content = "Connecting...";
				//Shameless hack to make my nice loading text work:
			App.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Background, new Action(delegate { }));
			
			string inputName = inputBox.Text;
			DBInterface setter = DBInterface.instance;
			if (setter.setDBServer(inputName) == true)
			{
				this.Close(); //Success
			}
			else
			{
				MessageBox.Show("SQL Server \"" + inputName + "\" could not be reached. Please verify that this server name is correct.", 
					"SQL Connection Failed", MessageBoxButton.OK, MessageBoxImage.Warning);

				inputBox.Text = "";
				ok.Content = "OK";
			}

		}

		private void inputBox_GotFocus(object sender, RoutedEventArgs e)
		{
			inputBox.Text = ""; //Clear default tip
			inputBox.Foreground = Brushes.Black;

			ok.IsEnabled = true; //Ok button is disabled until now to avoid setting the default input box filler text
		}

		private void inputBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter) ok_Click(null, null); //Convenience
		}
	}
}
