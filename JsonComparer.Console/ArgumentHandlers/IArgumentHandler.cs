namespace BigEgg.Tools.JsonComparer.ArgumentHandlers
{
    using System.Threading.Tasks;

    public interface IArgumentHandler
    {
        bool CanHandle(object parameter);

        Task Handle(object parameterObj);
    }
}
