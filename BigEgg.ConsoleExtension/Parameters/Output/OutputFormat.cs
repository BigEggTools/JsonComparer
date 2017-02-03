namespace BigEgg.ConsoleExtension.Parameters.Output
{
    using BigEgg.ConsoleExtension.Parameters.Output.Text;

    internal static class OutputFormat
    {
        private static VersionInfo versionInfo = new VersionInfo();

        public static VersionInfo VERSION_INFO { get { return versionInfo; } }
    }
}
