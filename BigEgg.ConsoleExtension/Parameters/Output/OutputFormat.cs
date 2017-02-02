namespace BigEgg.ConsoleExtension.Parameters.Output
{
    using BigEgg.ConsoleExtension.Parameters.Output.Text;

    internal static class OutputFormat
    {
        private static VersionTitle versionTitle = new VersionTitle();

        public static VersionTitle VERSION_TITLE { get { return versionTitle; } }
    }
}
