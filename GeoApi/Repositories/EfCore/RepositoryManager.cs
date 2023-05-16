using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EfCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly Lazy<IParcelRepository> _parcelRepository; 
        private readonly Lazy<IBuildingRepository> _buildingRepository;

        public RepositoryManager(RepositoryContext context)

        {
            _context = context;
            _parcelRepository = new Lazy<IParcelRepository>(() => new ParcelRepository(_context));
            _buildingRepository = new Lazy<IBuildingRepository>(() => new BuildingRepository(_context));
        }

      
        public IParcelRepository Parcel => _parcelRepository.Value; 
        public IBuildingRepository Building => _buildingRepository.Value;

       

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
