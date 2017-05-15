namespace BigEgg.Tools.JsonComparer.Services.Compares
{
    using System.Threading.Tasks;

    using BigEgg.Tools.JsonComparer.CompareData;
    using BigEgg.Tools.JsonComparer.JsonDocuments;

    internal interface IAnalyzeService
    {
        Task<CompareFile> Compare(JsonDocument document1, JsonDocument document2);
    }
}
