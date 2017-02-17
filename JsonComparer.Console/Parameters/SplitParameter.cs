namespace BigEgg.Tools.JsonComparer.Parameters
{
    using BigEgg.Tools.ConsoleExtension.Parameters;

    [Command("split", "Split the big JSON file to multiple small files.")]
    public class SplitParameter
    {
        [StringProperty("input", "i", "The path of JSON file to split.", Required = true)]
        public string FileName { get; set; }

        [StringProperty("output", "o", "The path to store the splited JSON files.", Required = true)]
        public string OutputPath { get; set; }
    }
}
