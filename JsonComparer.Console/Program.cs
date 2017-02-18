﻿namespace BigEgg.Tools.JsonComparer
{
    using System;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.Linq;
    using System.Reflection;

    using BigEgg.Tools.ConsoleExtension.Parameters;

    using BigEgg.Tools.JsonComparer.Parameters;
    using BigEgg.Tools.JsonComparer.Services;
    using BigEgg.Tools.JsonComparer.ArgumentHandlers;

    public class Program
    {
        private static CompositionContainer container;
        private static AggregateCatalog catalog;

        public static void Main(string[] args)
        {
            Initialize();

            var parameter = new Parser(
                    container,
                    ParserSettings.Builder().WithDefault().CaseSensitive(false).ComputeDisplayWidth().Build())
                .Parse(args, typeof(SplitParameter));
            if (parameter == null) { return; }

            var handlers = container.GetExportedValues<IArgumentHandler>();
            var handler = handlers.First(h => h.CanHandle(parameter));

            Console.Write("Find Handle to Handle this Parameter.");
            handler.Handle(parameter).Wait();
            Console.Write("Done.");
        }


        private static void Initialize()
        {
            catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Parser).Assembly));
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(IDocumentActionService).Assembly));

            container = new CompositionContainer(catalog);
            CompositionBatch batch = new CompositionBatch();
            batch.AddExportedValue(container);
            container.Compose(batch);
        }
    }
}
