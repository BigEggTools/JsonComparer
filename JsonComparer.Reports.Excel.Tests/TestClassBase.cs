namespace JsonComparer.Reports.Excel.Tests
{
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.Tools.JsonComparer.Reports.Excel;

    [TestClass]
    public abstract class TestClassBase
    {
        private readonly CompositionContainer container;

        protected TestClassBase()
        {
            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new TypeCatalog(
                typeof(ExcelReportDocumentType), typeof(ExcelReportService)
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
