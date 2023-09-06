namespace R53_GroupB_GadgetPoint.DAL.Interfaces
{
    public interface IGenericCrud<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
