namespace _01_Framework.Application
{
	public interface IAuthHelper
	{
		AuthViewModel GetCurrentAccountInfo();
		void SignIn(AuthViewModel account);
		string GetCurrentAccountRole();
		List<int> GetAccountPermissions();
		bool IsAuthenticated();
		void SignOut();
	}
}
