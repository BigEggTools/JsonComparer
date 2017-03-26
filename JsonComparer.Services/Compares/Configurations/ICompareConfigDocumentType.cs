namespace BigEgg.Tools.JsonComparer.Services.Compares.Configurations
{
    /// <summary>
    /// The document related logic
    /// </summary>
    internal interface ICompareConfigDocumentType
    {
        /// <summary>
        /// Reads the config file.
        /// </summary>
        /// <param name="fileName">Name of the config file.</param>
        /// <returns>The <seealso cref="CompareConfigDocument"/> type.</returns>
        CompareConfigDocument Read(string fileName);
    }
}