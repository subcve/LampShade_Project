namespace _01_Framework.Infrastructure
{
	public class NeedsPermissionAttribute: Attribute
	{
		public int Permission { get; set; }

		public NeedsPermissionAttribute(int permission)
		{
			Permission = permission;
		}
	}
}
