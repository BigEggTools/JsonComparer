using BigEgg.ConsoleExtension.Parameters;
using BigEgg.Tools.JsonComparer.Parameters;

namespace BigEgg.Tools.JsonComparer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var parameter = Parser.Default.Parse(args, typeof(SplitParameter));
            if (parameter == null) { return; }
        }
    }
}
