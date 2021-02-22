using Hahn.ApplicatonProcess.February2021.Data.Infrastructure;
using Hahn.ApplicatonProcess.February2021.Domain.Interfaces;
using Hahn.ApplicatonProcess.February2021.Domain.Models.Business;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hahn.ApplicatonProcess.February2021.Data.Repositories
{
    public class AssetRepository : DataRepositoryBase<Asset>, IAssetRepository
    {
        public AssetRepository(AssetContext context):base(context) { }
    }
}
