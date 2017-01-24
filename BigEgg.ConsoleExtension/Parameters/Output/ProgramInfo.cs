namespace BigEgg.ConsoleExtension.Parameters.Output
{
    using System;

    using BigEgg.ConsoleExtension.Parameters.Utils;

    internal class ProgramInfo
    {
        private static readonly Lazy<ProgramInfo> defaultHeaderText = new Lazy<ProgramInfo>(() =>
            new ProgramInfo(ReflectionHelper.GetAssemblyName(), ReflectionHelper.GetAssemblyVersion())
        );


        private ProgramInfo(string title, string version)
        {
            Title = title;
            Version = version;
        }


        public static ProgramInfo Default
        {
            get { return defaultHeaderText.Value; }
        }

        public string Title { get; private set; }

        public string Version { get; private set; }


        public override string ToString()
        {
            return string.Format(OutputFormat.PROGRAM_INFO, Title, Version);
        }

        public static implicit operator string(ProgramInfo headerText)
        {
            return headerText.ToString();
        }
    }
}
