using Hahn.ApplicatonProcess.February2021.Data.Services;
using Hahn.ApplicatonProcess.February2021.Domain.Interfaces;
using Hahn.ApplicatonProcess.February2021.Domain.Models.Business;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hahn.ApplicatonProcess.February2021.Tests.UnitTests.Fixtures
{
    public class ExistSomeAssetsFixture : FixtureBase
    {
        public IAssetService AssetService { get; set; }

        public ExistSomeAssetsFixture()
        {
            AssetContext.Add<Asset>(new Asset()
            {
               ID = 1,
                AssetName = "Test asset",
                Department = Department.HQ,
                Broken = false, CountryOfDepartment = "LVA",
                EMailAdressOfDepartment = "test@test.com",
                PurchaseDate = DateTime.UtcNow
            });

            AssetContext.SaveChanges();

            AssetService = new AssetService(Mapper, AssetRepository, UnitOfWork);
        }
    }
}
