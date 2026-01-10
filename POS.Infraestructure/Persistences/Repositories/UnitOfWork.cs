using Microsoft.Extensions.Configuration;
using POS.Infraestructure.FileStorage;
using POS.Infraestructure.Persistences.Contexts;
using POS.Infraestructure.Persistences.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infraestructure.Persistences.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PosContext _context;
        public ICategoryRepository CategoryRepository { get; private set; }

        public IUserRepository UserRepository { get; private set; }

        public IAzureStorage azureStorage { get; private set; }

        public IProviderRepository ProviderRepository { get; private set; }

        public UnitOfWork(PosContext context, IConfiguration configuration)
        {
            _context = context;
            CategoryRepository = new CategoryRepository(_context);
            UserRepository = new UserRepository(_context);
            azureStorage = new AzureStorage(configuration);
            ProviderRepository = new ProviderRespository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void SaveChange()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
