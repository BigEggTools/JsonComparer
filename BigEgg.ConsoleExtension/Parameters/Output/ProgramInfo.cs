namespace BigEgg.ConsoleExtension.Parameters.Output
{
    using System;
    using System.Reflection;

    using BigEgg.ConsoleExtension.Parameters.Utils;

    internal class ProgramInfo
    {
        private static readonly Lazy<ProgramInfo> defaultHeaderText = new Lazy<ProgramInfo>(() => new ProgramInfo());


        private ProgramInfo()
        {
            Title = ReflectionHelper.GetAssembly().GetName().Name;
            Version = ReflectionHelper.GetAssembly().GetName().Version.ToString();
            Copyright = ReflectionHelper.GetAssemblyAttribute<AssemblyCopyrightAttribute>().Copyright;
            Product = ReflectionHelper.GetAssemblyAttribute<AssemblyProductAttribute>().Product;
        }

        public static ProgramInfo Default
        {
            get { return defaultHeaderText.Value; }
        }


        public string Title { get; private set; }

        public string Version { get; private set; }

        public string Copyright { get; private set; }

        public string Product { get; private set; }
    }
}
