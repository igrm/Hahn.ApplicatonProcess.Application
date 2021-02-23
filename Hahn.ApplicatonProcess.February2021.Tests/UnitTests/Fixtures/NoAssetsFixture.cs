using Hahn.ApplicatonProcess.February2021.Data.Services;
using Hahn.ApplicatonProcess.February2021.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hahn.ApplicatonProcess.February2021.Tests.UnitTests.Fixtures
{
    public class NoAssetsFixture: FixtureBase
    {
        public IAssetService AssetService { get; set; }

        public NoAssetsFixture():base() 
        {
            AssetService = new AssetService(Mapper, AssetRepository, UnitOfWork);
        }
    }
}
