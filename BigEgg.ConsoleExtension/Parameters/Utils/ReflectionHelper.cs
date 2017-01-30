namespace BigEgg.ConsoleExtension.Parameters.Utils
{
    using System;
    using System.Linq;
    using System.Reflection;

    internal static class ReflectionHelper
    {
        public static TAttribute GetAssemblyAttribute<TAttribute>() where TAttribute : Attribute
        {
            return GetAssembly().GetCustomAttributes<TAttribute>().FirstOrDefault();
        }

        public static Assembly GetAssembly()
        {
            return Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
        }

        public static CommandAttribute GetCommand(this Type type)
        {
            var attribute = type.GetTypeInfo()
                                .GetCustomAttributes(typeof(CommandAttribute), true)
                                .FirstOrDefault();
            return attribute == null
                ? null
                : (CommandAttribute)attribute;
        }
    }
}
