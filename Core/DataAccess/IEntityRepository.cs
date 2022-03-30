using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T : class,IEntity, new () //T bir referans tip olmalı !!! ,gönderdiğimiz nesne new() lenebilmeli
    {
        T Get(Expression<Func<T,bool>> filter);
        IList<T> GetList(Expression<Func<T, bool>> filter = null); //Filtre gönderilmezse hepsini göndersin
        void Add(T entity); 
        void Update(T entity);
        void Delete(T entity);


    }
}
