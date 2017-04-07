namespace BigEgg.Tools.JsonComparer.JsonDocuments
{
    /// <summary>
    /// The default field
    /// </summary>
    /// <seealso cref="BigEgg.Tools.JsonComparer.JsonDocuments.Field" />
    public class DefaultField : Field
    {
        /// <summary>
        /// The default field name
        /// </summary>
        public readonly static string DEFAULT_FIELD_NAME = "Value";

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultField"/> class.
        /// </summary>
        public DefaultField()
            : base(DEFAULT_FIELD_NAME)
        { }
    }
}
