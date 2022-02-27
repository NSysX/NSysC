using Application.Interfaces;
using Ardalis.Specification.EntityFrameworkCore;
using Persistence.Contextos;

namespace Persistence.Repository
{
    public class MiRepositorioAsync<T> : RepositoryBase<T>, IRepositoryAsync<T> where T : class
    {
        private readonly NSysPDbContext _nSysPDbContext;

        public MiRepositorioAsync(NSysPDbContext nSysPDbContext) : base(nSysPDbContext)
        {
            this._nSysPDbContext = nSysPDbContext;
        }
    }
}
