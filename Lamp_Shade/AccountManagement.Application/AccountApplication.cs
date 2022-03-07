using _0_Framework.Application;
using _01_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domian.AccountAgg;
using AccountManagement.Domian.RoleAgg;

namespace AccountManagement.Application
{
	public class AccountApplication : IAccountApplication
	{
		private readonly IAccountRepository _accountRepository;
		private readonly IRoleRepository _roleRepository;
		private readonly IPasswordHasher _passwordHasher;
		private readonly IFileUpload _fileUpload;
		private readonly IAuthHelper _authHelper;
		public AccountApplication(IAccountRepository accountRepository, IPasswordHasher passwordHasher, IFileUpload fileUpload, IAuthHelper authHelper, IRoleRepository roleRepository)
		{
			_accountRepository = accountRepository;
			_passwordHasher = passwordHasher;
			_fileUpload = fileUpload;
			_authHelper = authHelper;
			_roleRepository = roleRepository;
		}

		public OperationResult ChangePassword(ChangePassword command)
		{
			OperationResult operation = new();

			var account = _accountRepository.Get(command.Id);

			if (account == null)
				return operation.Failed(ApplicationMessages.RecordNotFound);
			if(command.Password != command.RePassword)
				return operation.Failed(ApplicationMessages.PasswordsNotMatch);

			var password = _passwordHasher.Hash(command.Password);
			account.ChangePassword(password);
			_accountRepository.SaveChanges();
			return operation.Succeed();
		}

		public OperationResult Register(RegisterAccount command)
		{
			OperationResult operation = new();
			if (_accountRepository.Exists(c => c.UserName == command.UserName || c.Mobile == command.Mobile))
				return operation.Failed(ApplicationMessages.DuplicatedRecord);

			var password = _passwordHasher.Hash(command.Password);
			var picturepath = "";
			if(command.ProfilePhoto != null)
				picturepath = _fileUpload.Upload(command.ProfilePhoto, "ProfilePhotos"); 
			var account = new Account(command.Fullname,command.UserName,password,command.Mobile,command.RoleId,picturepath);
			_accountRepository.Create(account);
			_accountRepository.SaveChanges();
			return operation.Succeed();
		}

		public OperationResult Edit(EditAccount command)
		{
			OperationResult operation = new();
			var account = _accountRepository.Get(command.Id);

			if (account == null)
				return operation.Failed(ApplicationMessages.RecordNotFound);
			if (_accountRepository.Exists(c => (c.UserName == command.UserName || c.Mobile == command.Mobile) && c.Id != command.Id))
				return operation.Failed(ApplicationMessages.DuplicatedRecord);
			var picturepath = _fileUpload.Upload(command.ProfilePhoto, "ProfilePhotos");
			account.Edit(command.Fullname, command.UserName,command.Mobile, command.RoleId, picturepath);
			_accountRepository.SaveChanges();
			return operation.Succeed();
		}

		public EditAccount GetDetails(long id)
		{
			return _accountRepository.GetDetails(id);
		}

		public OperationResult Login(Login command)
		{
			var operation = new OperationResult();
			var account = _accountRepository.GetBy(command.Username);
			if(account == null)
				return operation.Failed("رمز و یا نام کاربری وارد شده اشتباه می باشد");

			(bool Verified, bool NeedsUpgrade) result = _passwordHasher.Check(account.Password, command.Password);
			if (!result.Verified)
				return operation.Failed("رمز و یا نام کاربری وارد شده اشتباه می باشد");

			var permissons = _roleRepository
				.Get(account.RoleId)
				.Permissions
				.Select(c=>c.Code)
				.ToList();

			var authViewModel = new AuthViewModel(account.Id, account.RoleId, account.Fullname, account.UserName, permissons)
			{
				RememberMe = command.RememberMe,
			};

			_authHelper.SignIn(authViewModel);
			return operation.Succeed();
		}

		public void LogOut()
		{
			_authHelper.SignOut();
		}

		public List<AccountViewModel> Search(AccountSearchModel searchModel)
		{
			return _accountRepository.Search(searchModel);
		}
	}
}