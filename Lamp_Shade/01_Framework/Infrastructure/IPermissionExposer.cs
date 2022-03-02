namespace _01_Framework.Infrastructure
{
	public interface IPermissionExposer
	{
		Dictionary<string,List<PermissionDto>> Expose();
	}
}
