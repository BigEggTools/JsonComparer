namespace BigEgg.Tools.JsonComparer
{
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.Reflection;

    using BigEgg.Tools.ConsoleExtension.Parameters;

    using BigEgg.Tools.JsonComparer.Parameters;

    public class Program
    {
        private static CompositionContainer container;
        private static AggregateCatalog catalog;

        public static void Main(string[] args)
        {
            Initialize();

            var parameter = new Parser(container, ParserSettings.Builder().WithDefault().ComputeDisplayWidth().Build()).Parse(args, typeof(SplitParameter));
            if (parameter == null) { return; }
        }


        private static void Initialize()
        {
            catalog = new AggregateCatalog();
            // Add the Framework assembly to the catalog
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Parser).Assembly));
            // Add the Bugger.Presentation assembly to the catalog
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));

            container = new CompositionContainer(catalog);
            CompositionBatch batch = new CompositionBatch();
            batch.AddExportedValue(container);
            container.Compose(batch);
        }
    }
}
