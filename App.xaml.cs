using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Configuration;
using System.Data;
using System.Windows;

namespace LOGONSECONDTRY
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			DatabaseFacade facade = new DatabaseFacade(new AppContext());
			facade.EnsureCreated();
		}
		
	}

}
