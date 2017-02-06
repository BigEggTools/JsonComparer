namespace BigEgg.ConsoleExtension.Tests.Parameters.FakeParameters
{
    using BigEgg.ConsoleExtension.Parameters;

    [Command("Clone", "Clone a repository into a new directory")]
    public class GitClone
    {
        [StringProperty("repository", "rep", "The (possibly remote) repository to clone from. See the URLS section below for more information on specifying repositories.", Required = true)]
        public string Repository { get; set; }
    }
}
