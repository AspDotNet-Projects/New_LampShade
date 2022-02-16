using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.InteropServices.ComTypes;

namespace _0_Framework.Domain
{
    /// <summary>
    ///  where T:class --->>>>>> T hatman batyad class bashad
    /// </summary>
    /// <typeparam name="Tkey"></typeparam> is id 
    /// <typeparam name="T"></typeparam> is Table or property
    public interface IRepository<Tkey,T> where T:class
    {
        T Get(Tkey id);
        List<T> Get();

        void Create(T entity);

        bool Exists(Expression<Func<T, bool>> expression);
        void SaveChange();
    }
}
