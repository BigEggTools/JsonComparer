namespace BigEgg.Tools.JsonComparer.Services.Compares
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BigEgg.Tools.JsonComparer.CompareData;
    using BigEgg.Tools.JsonComparer.JsonDocuments;
    using BigEgg.Tools.JsonComparer.Services.Compares.Configurations;

    internal interface IAnalyzeService
    {
        CompareFile Compare(JsonDocument document1, JsonDocument document2);
    }
}
