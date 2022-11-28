using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IService<T>
    {
        List<T> GetList();

        void Add(T t);

        void Delete(T t);

        void Update(T t);

        T? GetById(int id);
    }
}
