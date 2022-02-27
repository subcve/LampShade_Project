using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Framework.Infrastructure
{
	public class Roles
	{
		public const string Administration = "1";
		public const string SystemUser = "2";
		public const string ContentUploader = "3";

		public static string GetRoleBy(long roleId)
		{
			switch (roleId)
			{
				case 1:
					return "مدیر سیستم";
					break;

				case 3:
					return "محتوا گذار";
					break;
				default:
					return "";
					break;
			}
		}
	}
}
