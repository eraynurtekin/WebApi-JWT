using Core.Utilites.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        IDataResult<Category> GetById(int categoryid);
        IDataResult<List<Category>> GetList();
        //IResult Add(Category category);
        //IResult Update(Category category);
        //IResult Delete(Category category);

    }
}
