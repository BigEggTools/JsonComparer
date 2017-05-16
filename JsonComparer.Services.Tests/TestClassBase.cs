namespace BigEgg.Tools.JsonComparer.Services.Tests
{
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.Tools.JsonComparer.Services.Compares.Configurations;
    using BigEgg.Tools.JsonComparer.Services.FileActions;
    using BigEgg.Tools.JsonComparer.Services.Json;
    using BigEgg.Tools.JsonComparer.Services.Compares;

    [TestClass]
    public abstract class TestClassBase
    {
        private readonly CompositionContainer container;

        protected TestClassBase()
        {
            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new TypeCatalog(
                typeof(JsonDocumentService), typeof(FileActionService),
                typeof(CompareConfigDocumentType), typeof(ReadFileService), typeof(AnalyzeService), typeof(CompareService)
            ));
            container = new CompositionContainer(catalog);
            CompositionBatch batch = new CompositionBatch();
            batch.AddExportedValue(container);
            container.Compose(batch);
        }

        protected CompositionContainer Container { get { return container; } }


        [TestInitialize]
        public void TestInitialize()
        {
            OnTestInitialize();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            OnTestCleanup();
        }


        protected virtual void OnTestInitialize() { }

        protected virtual void OnTestCleanup() { }
    }
}
