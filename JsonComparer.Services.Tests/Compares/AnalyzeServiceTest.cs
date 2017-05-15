namespace BigEgg.Tools.JsonComparer.Services.Tests.Compares
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.UnitTesting;

    using BigEgg.Tools.JsonComparer.JsonDocuments;
    using BigEgg.Tools.JsonComparer.Services.Compares;

    public class AnalyzeServiceTest
    {
        [TestClass]
        public class PreconditionTest : TestClassBase
        {
            [TestMethod]
            public void DocumentBothNull_PreconditionCheck()
            {
                var service = Container.GetExportedValue<IAnalyzeService>();

                AssertHelper.ExpectedException<ArgumentException>(() => service.Compare(null, null));
            }

            [TestMethod]
            public void DocumentNotSameName_PreconditionCheck()
            {
                var service = Container.GetExportedValue<IAnalyzeService>();

                AssertHelper.ExpectedException<ArgumentException>(() => service.Compare(GenerateJsonDocument("file1"), GenerateJsonDocument("file2")));
            }


            private JsonDocument GenerateJsonDocument(string name)
            {
                var document = new JsonDocument(name, new List<KeyValuePair<string, FieldType>>() { new KeyValuePair<string, FieldType>("name", FieldType.String) });

                document.CreateProperty("_document.data");
                document["_document.data"]["name"] = "value";

                return document;
            }
        }

        [TestClass]
        public class LogicTest : TestClassBase
        {
            [TestMethod]
            public async Task Document1_Null()
            {
                var service = Container.GetExportedValue<IAnalyzeService>();

                var result = await service.Compare(null, GenerateJsonDocument("file"));
                Assert.IsNotNull(result);
                Assert.AreEqual("file", result.FileName);
                Assert.AreEqual(1, result.CompareItems.Count);

                Assert.AreEqual("_document.data", result.CompareItems[0].PropertyName);
                Assert.IsFalse(result.CompareItems[0].HasData1);
                Assert.IsTrue(result.CompareItems[0].HasData2);
                Assert.AreEqual(1, result.CompareItems[0].Data.Count);

                Assert.IsNotNull(result.CompareItems[0].Data["name"]);
                Assert.IsNull(result.CompareItems[0].Data["name"].Item1);
                Assert.IsNotNull(result.CompareItems[0].Data["name"].Item2);
                Assert.AreEqual("value", result.CompareItems[0].Data["name"].Item2.Value);
            }

            [TestMethod]
            public async Task Document2_Null()
            {
                var service = Container.GetExportedValue<IAnalyzeService>();

                var result = await service.Compare(GenerateJsonDocument("file"), null);
                Assert.IsNotNull(result);
                Assert.AreEqual("file", result.FileName);
                Assert.AreEqual(1, result.CompareItems.Count);

                Assert.AreEqual("_document.data", result.CompareItems[0].PropertyName);
                Assert.IsTrue(result.CompareItems[0].HasData1);
                Assert.IsFalse(result.CompareItems[0].HasData2);
                Assert.AreEqual(1, result.CompareItems[0].Data.Count);

                Assert.IsNotNull(result.CompareItems[0].Data["name"]);
                Assert.IsNotNull(result.CompareItems[0].Data["name"].Item1);
                Assert.IsNull(result.CompareItems[0].Data["name"].Item2);
                Assert.AreEqual("value", result.CompareItems[0].Data["name"].Item1.Value);
            }

            [TestMethod]
            public async Task Document1_Empty()
            {
                var service = Container.GetExportedValue<IAnalyzeService>();

                var result = await service.Compare(GenerateEmptyJsonDocument("file"), GenerateJsonDocument("file"));
                Assert.IsNotNull(result);
                Assert.AreEqual("file", result.FileName);
                Assert.AreEqual(1, result.CompareItems.Count);

                Assert.AreEqual("_document.data", result.CompareItems[0].PropertyName);
                Assert.IsFalse(result.CompareItems[0].HasData1);
                Assert.IsTrue(result.CompareItems[0].HasData2);
                Assert.AreEqual(1, result.CompareItems[0].Data.Count);

                Assert.IsNotNull(result.CompareItems[0].Data["name"]);
                Assert.IsNull(result.CompareItems[0].Data["name"].Item1);
                Assert.IsNotNull(result.CompareItems[0].Data["name"].Item2);
                Assert.AreEqual("value", result.CompareItems[0].Data["name"].Item2.Value);
            }

            [TestMethod]
            public async Task Document2_Empty()
            {
                var service = Container.GetExportedValue<IAnalyzeService>();

                var result = await service.Compare(GenerateJsonDocument("file"), GenerateEmptyJsonDocument("file"));
                Assert.IsNotNull(result);
                Assert.AreEqual("file", result.FileName);
                Assert.AreEqual(1, result.CompareItems.Count);

                Assert.AreEqual("_document.data", result.CompareItems[0].PropertyName);
                Assert.IsTrue(result.CompareItems[0].HasData1);
                Assert.IsFalse(result.CompareItems[0].HasData2);
                Assert.AreEqual(1, result.CompareItems[0].Data.Count);

                Assert.IsNotNull(result.CompareItems[0].Data["name"]);
                Assert.IsNotNull(result.CompareItems[0].Data["name"].Item1);
                Assert.IsNull(result.CompareItems[0].Data["name"].Item2);
                Assert.AreEqual("value", result.CompareItems[0].Data["name"].Item1.Value);
            }

            [TestMethod]
            public async Task DifferentData()
            {
                var service = Container.GetExportedValue<IAnalyzeService>();

                var result = await service.Compare(GenerateComplexJsonDocument1("file"), GenerateComplexJsonDocument2("file"));
                Assert.IsNotNull(result);
                Assert.AreEqual("file", result.FileName);
                Assert.AreEqual(3, result.CompareItems.Count);

                #region Check Only Document1 Exist Property
                Assert.AreEqual("_document.data1", result.CompareItems[0].PropertyName);
                Assert.IsTrue(result.CompareItems[0].HasData1);
                Assert.IsFalse(result.CompareItems[0].HasData2);
                Assert.AreEqual(2, result.CompareItems[0].Data.Count);

                Assert.IsNotNull(result.CompareItems[0].Data["name1"]);
                Assert.IsNotNull(result.CompareItems[0].Data["name1"].Item1);
                Assert.IsNull(result.CompareItems[0].Data["name1"].Item2);
                Assert.AreEqual("value1", result.CompareItems[0].Data["name1"].Item1.Value);

                Assert.IsNotNull(result.CompareItems[0].Data["name2"]);
                Assert.IsNotNull(result.CompareItems[0].Data["name2"].Item1);
                Assert.IsNull(result.CompareItems[0].Data["name2"].Item2);
                Assert.AreEqual("value2", result.CompareItems[0].Data["name2"].Item1.Value);
                #endregion

                #region Check Only Document2 Exist Property
                Assert.AreEqual("_document.data2", result.CompareItems[1].PropertyName);
                Assert.IsFalse(result.CompareItems[1].HasData1);
                Assert.IsTrue(result.CompareItems[1].HasData2);
                Assert.AreEqual(2, result.CompareItems[1].Data.Count);

                Assert.IsNotNull(result.CompareItems[1].Data["name1"]);
                Assert.IsNull(result.CompareItems[1].Data["name1"].Item1);
                Assert.IsNotNull(result.CompareItems[1].Data["name1"].Item2);
                Assert.AreEqual("value1", result.CompareItems[1].Data["name1"].Item2.Value);

                Assert.IsNotNull(result.CompareItems[1].Data["name2"]);
                Assert.IsNull(result.CompareItems[1].Data["name2"].Item1);
                Assert.IsNotNull(result.CompareItems[1].Data["name2"].Item2);
                Assert.AreEqual("value2", result.CompareItems[1].Data["name2"].Item2.Value);
                #endregion

                #region Check Both Existed Property
                Assert.AreEqual("_document.moreData", result.CompareItems[2].PropertyName);
                Assert.IsTrue(result.CompareItems[2].HasData1);
                Assert.IsTrue(result.CompareItems[2].HasData2);
                Assert.AreEqual(2, result.CompareItems[2].Data.Count);

                Assert.IsNotNull(result.CompareItems[2].Data["name1"]);
                Assert.IsNotNull(result.CompareItems[2].Data["name1"].Item1);
                Assert.IsNotNull(result.CompareItems[2].Data["name1"].Item2);
                Assert.AreEqual("value1", result.CompareItems[2].Data["name1"].Item1.Value);
                Assert.AreEqual("value1", result.CompareItems[2].Data["name1"].Item2.Value);

                Assert.IsNotNull(result.CompareItems[2].Data["name2"]);
                Assert.IsNotNull(result.CompareItems[2].Data["name2"].Item1);
                Assert.IsNotNull(result.CompareItems[2].Data["name2"].Item2);
                Assert.AreEqual("value2", result.CompareItems[2].Data["name2"].Item1.Value);
                Assert.AreEqual("value2", result.CompareItems[2].Data["name2"].Item2.Value);
                #endregion
            }



            private JsonDocument GenerateJsonDocument(string name)
            {
                var document = new JsonDocument(name, new List<KeyValuePair<string, FieldType>>() { new KeyValuePair<string, FieldType>("name", FieldType.String) });

                document.CreateProperty("_document.data");
                document["_document.data"]["name"] = "value";

                return document;
            }

            private JsonDocument GenerateEmptyJsonDocument(string name)
            {
                var document = new JsonDocument(name, new List<KeyValuePair<string, FieldType>>() { new KeyValuePair<string, FieldType>("name", FieldType.String) });

                return document;
            }

            private JsonDocument GenerateComplexJsonDocument1(string name)
            {
                var document = new JsonDocument(name, new List<KeyValuePair<string, FieldType>>()
                {
                    new KeyValuePair<string, FieldType>("name1", FieldType.String),
                    new KeyValuePair<string, FieldType>("name2", FieldType.String)
                });

                document.CreateProperty("_document.data1");
                document["_document.data1"]["name1"] = "value1";
                document["_document.data1"]["name2"] = "value2";

                document.CreateProperty("_document.moreData");
                document["_document.moreData"]["name1"] = "value1";
                document["_document.moreData"]["name2"] = "value2";

                return document;
            }

            private JsonDocument GenerateComplexJsonDocument2(string name)
            {
                var document = new JsonDocument(name, new List<KeyValuePair<string, FieldType>>()
                {
                    new KeyValuePair<string, FieldType>("name1", FieldType.String),
                    new KeyValuePair<string, FieldType>("name2", FieldType.String)
                });

                document.CreateProperty("_document.data2");
                document["_document.data2"]["name1"] = "value1";
                document["_document.data2"]["name2"] = "value2";

                document.CreateProperty("_document.moreData");
                document["_document.moreData"]["name1"] = "value1";
                document["_document.moreData"]["name2"] = "value2";

                return document;
            }
        }
    }
}
