
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IParcelService> _parcelService;
        private readonly Lazy<IBuildingService> _buildingService;
        public ServiceManager(IRepositoryManager repositoryManager)
        {
            _parcelService = new Lazy<IParcelService>(() => new ParcelManager(repositoryManager));
            _buildingService = new Lazy<IBuildingService>(() => new BuildingManager(repositoryManager));
        }

        public IParcelService ParcelService => _parcelService.Value;
        public IBuildingService BuildingService => _buildingService.Value;
    }
}
