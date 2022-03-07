namespace _01_Framework.Infrastructure
{
    public class Roles
	{
		public const string Administration = "1";
		public const string SystemUser = "2";
		public const string ColleagueUser = "3";

		public static string GetRoleBy(long roleId)
		{
			switch (roleId)
			{
				case 1:
					return "مدیر سیستم";
				case 3:
					return "کاربر همکار";
				default:
					return "";
			}
		}
	}
}
