namespace StudentRandomizer
{
	public static class AppConsts
	{
		public static string AppDataPath { get; } = Path.Combine(
			Environment.GetFolderPath(
				Environment.SpecialFolder.LocalApplicationData),
			"StudentRandomizer");
		public static string DbName { get; } = "master.db";
		public static string DbPath { get; } = Path.Combine(AppDataPath,
															DbName);
	}
}
