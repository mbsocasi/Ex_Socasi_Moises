using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace SLC
{
    public interface IHospitalesService
    {
        Hospitales Create(Hospitales hospital);
        Hospitales RetrieveById(int id);
        List<Hospitales> RetrieveAll();
        bool Update(Hospitales hospital);
        bool Delete(int id);
    }
}
