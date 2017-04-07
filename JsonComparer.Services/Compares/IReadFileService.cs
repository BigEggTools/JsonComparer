namespace BigEgg.Tools.JsonComparer.Services.Compares
{
    using System.Threading.Tasks;

    using BigEgg.Tools.JsonComparer.JsonDocuments;
    using BigEgg.Tools.JsonComparer.Services.Compares.Configurations;

    internal interface IReadFileService
    {
        Task<JsonDocument> GetJsonDocument(string fileName, CompareConfigDocument config);
    }
}