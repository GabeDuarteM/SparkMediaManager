using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkMediaManager.Services
{
    public interface IRepositorio<T> where T : class
    {
        bool Adicionar(params T[] obj);

        bool Remover(params T[] obj);

        bool Update(params T[] obj);

        List<T> GetLista();

        T Get(int id);
    }
}
