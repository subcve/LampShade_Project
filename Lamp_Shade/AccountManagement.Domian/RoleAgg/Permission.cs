namespace AccountManagement.Domian.RoleAgg
{
	public class Permission
	{
		public long Id { get; private set; }
		public string Name { get; private set; }
		public int Code { get; private set; }

		public Permission(int code)
		{
			Code = code;
		}

		public long RoleId { get; private set; }
		public Role Role { get; private set; }

		public Permission(string name, int code)
		{
			Name = name;
			Code = code;
		}
	}
}