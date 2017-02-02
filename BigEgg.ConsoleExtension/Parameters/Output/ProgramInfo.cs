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
            Title = GetAssemblyName();
            Version = GetAssemblyVersion();
            Copyright = GetAssemblyCopyright();
            Product = GetAssemblyProduct();
        }

        public static ProgramInfo Default
        {
            get { return defaultHeaderText.Value; }
        }


        public string Title { get; private set; }

        public string Version { get; private set; }

        public string Copyright { get; private set; }

        public string Product { get; private set; }


        public override string ToString()
        {
            return $"{Title} {Version}" + Environment.NewLine;
        }

        public static implicit operator string(ProgramInfo headerText)
        {
            return headerText.ToString();
        }


        private static string GetAssemblyName()
        {
            return ReflectionHelper.GetAssembly().GetName().Name;
        }

        private static string GetAssemblyVersion()
        {
            return ReflectionHelper.GetAssembly().GetName().Version.ToString();
        }

        private static string GetAssemblyCopyright()
        {
            return ReflectionHelper.GetAssemblyAttribute<AssemblyCopyrightAttribute>().Copyright;
        }

        private static string GetAssemblyProduct()
        {
            return ReflectionHelper.GetAssemblyAttribute<AssemblyProductAttribute>().Product;
        }
    }
}
