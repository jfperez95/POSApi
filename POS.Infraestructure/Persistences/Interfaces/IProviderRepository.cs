using POS.Domain.Entities;
using POS.Infraestructure.Commons.Bases.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infraestructure.Persistences.Interfaces
{
    public interface IProviderRepository : IGenericRepository<Provider>
    {
        Task<BaseEntityResponse<Provider>> ListProviders(BaseFiltersRequest filters);
    }
}
