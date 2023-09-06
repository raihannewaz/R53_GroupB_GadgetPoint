using R53_GroupB_GadgetPoint.DAL.Interface;

namespace R53_GroupB_GadgetPoint.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericCrud<T> Repository<T>() where T : class;
        Task<int> Complete();
    }
}
