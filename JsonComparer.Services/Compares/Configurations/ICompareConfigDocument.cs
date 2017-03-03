namespace BigEgg.Tools.JsonComparer.Services.Compares.Configurations
{
    internal interface ICompareConfigDocument
    {
        CompareConfig ReadFromFile(string fileName);
    }
}