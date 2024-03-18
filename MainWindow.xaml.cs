using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.IO;
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
using static System.Net.Mime.MediaTypeNames;

namespace LOGONSECONDTRY
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		AppContext db;

		byte[] avatar;
		public MainWindow()
		{
			InitializeComponent();

			db = new AppContext();
            
			var users = db.Users.AsNoTracking().ToList();
			byte[] str;
			foreach (User user in users)
			{
				str = user.Avatar;
			}
			
        }
		public void BrowseImage_Click(object sender, RoutedEventArgs e)
		{

			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";

			if (openFileDialog.ShowDialog() == true)
			{
				BitmapImage bitmap = new BitmapImage(new Uri(openFileDialog.FileName));
				avatarBox.Source = bitmap;
				JpegBitmapEncoder encoder = new JpegBitmapEncoder();
				encoder.Frames.Add(BitmapFrame.Create(bitmap));

				using (MemoryStream memoryStream = new MemoryStream())
				{
					encoder.Save(memoryStream);
					avatar = memoryStream.ToArray();
				}
			}
		}
		private void SignUp_Click(object sender, RoutedEventArgs e)
		{
			string login = regLoginBox.Text.Trim();
			string password = regPassBox.Password.Trim();
			string passwordCheck = regPassCheckBox.Password.Trim();
			string bday = pickedDataBox.Text.Trim();


			if (login?.Length < 3)
			{
				regLoginBox.ToolTip = "Login must contain 3 or more characters";
				regLoginBox.Background = Brushes.Red;
			}

			else if (password?.Length < 3)
			{
				regPassBox.ToolTip = "Password must contain 3 or more characters";
				regPassBox.Background = Brushes.Red;
			}

			else if (bday?.Length < 8)
			{
				pickedDataBox.ToolTip = "You should fill in this line with date";
				pickedDataBox.Background = Brushes.Red;
			}

			else if (passwordCheck != password)
			{
				regPassCheckBox.ToolTip = "Passwords are not the same";
				regPassCheckBox.Background = Brushes.Red;
			}

			else if(avatarBox.Source == null)
			{
				MessageBox.Show("Choose your avatar");

			}
			else
			{
				regLoginBox.ToolTip = "Login is entered correctly";
				regLoginBox.Background = Brushes.White;

				regPassBox.ToolTip = "Password is entered correctly";
				regPassBox.Background = Brushes.White;

				regPassBox.ToolTip = "Passwords are entered correctly";
				regPassCheckBox.Background = Brushes.White;

				User user = new User(login, password, bday, avatar);

				var exist = db.Users.FirstOrDefault(x => x.Login == user.Login);

				if (exist != null)
				{
					MessageBox.Show("Tакой логин уже есть");
				}
				else
				{
					db.Users.Add(user);
					db.SaveChanges();
                    MessageBox.Show("Вы успешно прошли регистрацию");
                }

			}
		}
		
		
		private void Button_SignInForm_Click(object sender, RoutedEventArgs e)
		{
			Authen authen = new Authen();
			authen.Show();
			Hide();
		}

		private void RadioButton_Checked(object sender, RoutedEventArgs e)
		{

		}

	}
}