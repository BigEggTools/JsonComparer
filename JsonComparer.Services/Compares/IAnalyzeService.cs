namespace BigEgg.Tools.JsonComparer.Services.Compares
{
    using BigEgg.Tools.JsonComparer.CompareData;
    using BigEgg.Tools.JsonComparer.JsonDocuments;

    internal interface IAnalyzeService
    {
        CompareFile Compare(JsonDocument document1, JsonDocument document2);
    }
}
