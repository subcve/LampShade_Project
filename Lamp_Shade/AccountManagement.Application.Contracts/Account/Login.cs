namespace AccountManagement.Application.Contracts.Account
{
	public class Login
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public bool RememberMe { get; set; }
	}
}
