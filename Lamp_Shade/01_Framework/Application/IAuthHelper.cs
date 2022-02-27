namespace _01_Framework.Application
{
	public interface IAuthHelper
	{
		void SignIn(AuthViewModel account);
		string GetCurrentAccountRole();
		AuthViewModel GetCurrentAccountInfo();
		bool IsAuthenticated();
		void SignOut();
	}
}
