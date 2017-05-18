namespace BigEgg.Tools.JsonComparer.Parameters
{
    using BigEgg.Tools.ConsoleExtension.Parameters;

    [Command("merge", "Merge the splited JSON files to node in one JSON file, to build a big file.")]
    public class MergeParameter
    {
        [StringProperty("root_file", "r", "The JSON file name to be the root.", Required = true)]
        public string FileName { get; set; }

        [StringProperty("node_name", "n", "The root node name for merging.", Required = true)]
        public string NodeName { get; set; }

        [StringProperty("small_file_path", "fp", "The splited JSON files' path.", Required = true)]
        public string FilesPath { get; set; }
    }
}
