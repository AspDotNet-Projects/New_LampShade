using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.InteropServices.ComTypes;

namespace _0_Framework.Domain
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="Tkey"></typeparam> is id 
    /// <typeparam name="T"></typeparam> is Table or property
    public interface IRepository<Tkey,T>
    {
        T Get(Tkey id);
        List<T> Get();

        void Create(T entity);

        bool Exists(Expression<Func<T, bool>> expression);
        void SaveChange();
    }
}
