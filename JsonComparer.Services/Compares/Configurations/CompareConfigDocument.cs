namespace BigEgg.Tools.JsonComparer.Services.Compares.Configurations
{
    using System;
    using System.ComponentModel.Composition;
    using System.Diagnostics;
    using System.IO;

    using Newtonsoft.Json;

    [Export(typeof(ICompareConfigDocument))]
    internal class CompareConfigDocument : ICompareConfigDocument
    {
        public CompareConfig ReadFromFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) { throw new ArgumentException("fileName"); }

            CompareConfig config = null;

            var extension = Path.GetExtension(fileName);
            switch (extension)
            {
                case ".json":
                    config = ReadFromJson(fileName);
                    break;
            }



            return config;
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
                var config = JsonConvert.DeserializeObject<CompareConfig>(jsonString, settings);

                Trace.TraceWarning(config == null
                    ? $"Failed to compare config data from file {fileName}"
                    : $"End to compare config data from file {fileName}");
                Trace.Unindent();

                return config;
            }
        }
    }
}
