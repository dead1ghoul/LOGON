using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOGONSECONDTRY
{
	class User
	{
		public int Id { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
		public string BirthDay { get; set; }
		public byte[] Avatar { get; set; }


		public User(string login, string password, string birthDay, byte[] avatar)
		{
			Login = login;
			Password = password;
			BirthDay = birthDay;
			Avatar = avatar;
		}
	}
}
