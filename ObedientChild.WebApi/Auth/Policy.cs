namespace ObedientChild.WebApi
{
	public static class Policy
	{
		public const string MustBeAllAccessMode = "AccessMode:All";
		//public const string MustBeAllOrImportAccessMode = "AccessMode:AllOrImport";
		public const string MustBeAllOrRestrictedAccessMode = "AccessMode:AllOrExport";
		public const string MustBeAdmin = "Role:Admin";
	}
}
