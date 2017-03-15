namespace BigEgg.Tools.JsonComparer.Services.Compares.Configurations
{
    using System.ComponentModel.Composition;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    using Newtonsoft.Json;

    [Export(typeof(ICompareConfigDocument))]
    internal class CompareConfigDocument : ICompareConfigDocument
    {
        public CompareConfig ReadFromFile(string fileName)
        {
            Preconditions.NotNullOrWhiteSpace(fileName, "fileName");

            CompareConfig config = null;
            var extension = Path.GetExtension(fileName);
            switch (extension)
            {
                case ".json":
                    config = ReadFromJson(fileName);
                    break;
            }

            return config != null
                ? config.Validate().Any()
                    ? null
                    : config
                : null;
        }


        private CompareConfig ReadFromJson(string fileName)
        {
            Trace.Indent();
            Trace.TraceInformation($"Start to read compare config data from file {fileName}");

            using (StreamReader sr = new StreamReader(fileName))
            {
                var jsonString = sr.ReadToEnd();
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Include,
                    MissingMemberHandling = MissingMemberHandling.Error,
                    ContractResolver = new PrivateSetterCamelCasePropertyNamesContractResolver()
                };
                try
                {
                    var config = JsonConvert.DeserializeObject<CompareConfig>(jsonString, settings);
                    Trace.TraceWarning($"End to compare config data from file {fileName}");
                    return config;
                }
                catch (JsonSerializationException ex)
                {
                    Trace.TraceWarning($"Failed to compare config data from file {fileName}. Error message: {ex.Message}");
                    return null;
                }
                finally
                {
                    Trace.Unindent();
                }
            }
        }
    }
}
