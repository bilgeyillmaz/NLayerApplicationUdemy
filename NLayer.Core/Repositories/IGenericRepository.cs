using System.Linq.Expressions;

namespace NLayer.Core.Repositories;

public interface IGenericRepository<T> where T : class
{
    Task<T> GetByIdAsync(int id);
    IQueryable<T> GetAll();
    //entityi alacak ve geriye true ya da false dönecek, idsi 5 ten büyük mü evet true o zaman dahil et listeye
    //IQueryable kullanmamızın sebebi where sorgusunun ardından orderby gibi sorgular da kullanabilmektir.
    //List ile orderby sorgusunu kullanamazdık.
    IQueryable<T> Where(Expression<Func<T, bool>> expression);
    Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    //EF Core tarafında update ve remove komutlarının asenkron verisyonları yoktur. 
    //Uzun süren bir işlem değildir. Yalnızca o memoryde takip etmiş olduğu verinin stateini update ediyor.
    //Asenkron programlama var olan threadleri bloklamamak/ efektif kullanmak için kullanılır.
    void Update(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
}