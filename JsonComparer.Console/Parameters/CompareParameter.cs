namespace BigEgg.Tools.JsonComparer.Parameters
{
    using BigEgg.Tools.ConsoleExtension.Parameters;

    [Command("compare", "Compare the JSON files in two folder.")]
    public class CompareParameter
    {
        [StringProperty("path1", "p1", "The folder name of JSON files to compare.", Required = true)]
        public string Path1 { get; set; }

        [StringProperty("path2", "p2", "The another folder name of JSON files to compare.", Required = true)]
        public string Path2 { get; set; }

        [StringProperty("config", "c", "The configuration file for compare action.", Required = true)]
        public string ConfigFile { get; set; }

        [StringProperty("output", "o", "The File Name of the Compare Result.")]
        public string OutputPath { get; set; }
    }
}
