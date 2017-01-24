namespace BigEgg.ConsoleExtension.Parameters
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    /// <summary>
    /// The assembly header attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
    [ComVisible(false)]
    public class AssemblyHeaderAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyHeaderAttribute"/> class.
        /// </summary>
        /// <param name="lines">The lines of the header text.</param>
        public AssemblyHeaderAttribute(params string[] lines)
        {
            Lines = lines;
        }

        /// <summary>
        /// Gets the lines of header text.
        /// </summary>
        /// <value>
        /// The lines.
        /// </value>
        public IEnumerable<string> Lines { get; private set; }
    }
}
