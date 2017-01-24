namespace BigEgg.ConsoleExtension.Parameters.Utils
{
    using System;
    using System.Linq;
    using System.Reflection;

    internal class ReflectionHelper
    {
        public static TAttribute GetAttribute<TAttribute>() where TAttribute : Attribute
        {
            return GetAssembly().GetCustomAttributes<TAttribute>().FirstOrDefault();
        }

        public static Assembly GetAssembly()
        {
            return Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
        }
    }
}
