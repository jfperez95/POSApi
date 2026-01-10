using POS.Infraestructure.FileStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infraestructure.Persistences.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        //Declaraccion de nuestras interfaces a nivel de repositorio
        ICategoryRepository CategoryRepository { get; }
        IUserRepository UserRepository { get; }
        IAzureStorage azureStorage { get;  }
        IProviderRepository ProviderRepository { get;  }

        void SaveChange();
        Task SaveChangesAsync();
    }
}
