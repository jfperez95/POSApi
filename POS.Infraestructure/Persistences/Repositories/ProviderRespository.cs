using Microsoft.EntityFrameworkCore;
using POS.Domain.Entities;
using POS.Infraestructure.Commons.Bases.Request;
using POS.Infraestructure.Persistences.Contexts;
using POS.Infraestructure.Persistences.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infraestructure.Persistences.Repositories
{
    public class ProviderRespository : GenericRepository<Provider>, IProviderRepository
    {
        public ProviderRespository(PosContext context) : base(context)
        {

        }

        public async Task<BaseEntityResponse<Provider>> ListProviders(BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<Provider>();

            var provider = GetEntityQuery(x => x.AuditDeleteUser == null && x.AuditDeleteDate == null)
                .Include(x=> x.DocumentType)
                .AsNoTracking();

            if(filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch (filters.NumFilter)
                {
                    case 1:
                        provider = provider.Where(x => x.Name.Contains(filters.TextFilter));
                        break;
                    case 2:
                        provider = provider.Where(x => x.Email.Contains(filters.TextFilter));
                        break;
                    case 3:
                        provider = provider.Where(x => x.DocumentNumber.Contains(filters.TextFilter));
                        break;
                }
            }

            if(filters.StateFilter is not null)
            {
                provider = provider.Where(x => x.State.Equals(filters.StateFilter));
            }

            if(filters.StartDate is not null && filters.EndDate is not null)
            {
                provider = provider.Where(x => x.AuditCreateDate >= Convert.ToDateTime(filters.StartDate) && x.AuditCreateDate <= Convert.ToDateTime(filters.EndDate).AddDays(1));
            }

            if (filters.Sort is null) filters.Sort = "Id";

            response.TotalRecords = await provider.CountAsync();
            response.Items = await Ordening(filters, provider, !(bool)filters.Download!).ToListAsync();

            return response;
        }
    }
}
