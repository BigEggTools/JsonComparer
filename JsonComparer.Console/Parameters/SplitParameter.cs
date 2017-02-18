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

        [StringProperty("node_name", "n", "The name of node to split.", Required = true)]
        public string NodeName { get; set; }

        [StringProperty("output_pattern", "op", "The output file name pattern. Use '${name}' for node name, ${index} for the child index.")]
        public string OutputFileNamePattern { get; set; }
    }
}
