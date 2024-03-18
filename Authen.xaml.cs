using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LOGONSECONDTRY
{
    /// <summary>
    /// Логика взаимодействия для Authen.xaml
    /// </summary>
    public partial class Authen : Window
	{
		AppContext db;
        public Authen()
        {
			InitializeComponent();
            db = new AppContext();
			var users = db.Users.AsNoTracking().ToList();
			foreach ( User user in users ) 
			{
                authenAvatarBox.Source = BytesToImage(user.Avatar); 
			}
        }
        public byte[] ConvertToBytes(BitmapImage bitmapImage)
        {
            using (var stream = new MemoryStream())
            {
                var encoder = new JpegBitmapEncoder(); // or some other encoder
                encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                encoder.Save(stream);
                return stream.ToArray();
            }
        }
        public ImageSource BytesToImage(byte[] imageData)
        {
            using (var ms = new MemoryStream(imageData))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();

                ImageSource imgSrc = image;

                return imgSrc;
            }
        }

		public void Button_AvatarLogin_Click( object sender, RoutedEventArgs e)
		{
			
		}
        public void Button_Reg_Click(object sender, RoutedEventArgs e)
        {
			MainWindow mainWindow = new MainWindow();
			mainWindow.Show();
			Hide();
		}

        public void SignIn_Click(object sender, RoutedEventArgs e)
        {
			string login = authLoginBox.Text.Trim();
			string password = authPassBox.Password.Trim();
			if (login?.Length < 3)
			{
				authLoginBox.ToolTip = "Login must contain 3 or more characters";
				authLoginBox.Background = Brushes.Red;
			}

			else if (password?.Length < 3)
			{
				authPassBox.ToolTip = "Password must contain 3 or more characters";
				authPassBox.Background = Brushes.Red;
			}

			else
			{
				authLoginBox.Background = Brushes.White;
				authPassBox.Background = Brushes.White;

				using (AppContext context = new AppContext())
				{
					bool isUserFound = context.Users.Any( user => user.Login == login &&  user.Password == password );


					if (isUserFound) 
					{
						MessageBox.Show("You successfully authorized");
					}
					else
					{
						MessageBox.Show("Login or password is incorrect");
					}
				}
			}
		}

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
