using Hahn.ApplicatonProcess.February2021.Domain.Exceptions;
using Hahn.ApplicatonProcess.February2021.Domain.Models.DTO;
using Hahn.ApplicatonProcess.February2021.Tests.UnitTests.Fixtures;
using Hahn.ApplicatonProcess.February2021.Tests.UnitTests.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;

namespace Hahn.ApplicatonProcess.February2021.Tests.UnitTests.Facts
{
    public class AssetServiceFacts
    {
        public class CreateAsyncMethod : MethodBase<NoAssetsFixture>
        {
            public CreateAsyncMethod(NoAssetsFixture fixture) : base(fixture)  {  }

            [Fact]
            public async void WhenAssetDataProvided_AssetIsCreated()
            {
                await fixture.AssetService.CreateAsync(new AssetDto() { AssetName = "Test asset", 
                                                                        Department = 1, Broken = "false", 
                                                                        Country = "LVA", Email = "test@test.com", 
                                                                        PurchaseDate = "01/01/2021" });
                Assert.NotEmpty(fixture.AssetContext.Asset);
            }
        }

        public class DeleteAsyncMethod : MethodBase<ExistSomeAssetsFixture>
        {
            public DeleteAsyncMethod(ExistSomeAssetsFixture fixture) : base(fixture) { }

            [Fact]
            public async void WhenIdOfAssetProvided_AssetIsDeleted()
            {
                await fixture.AssetService.DeleteAsync(1);
                Assert.Empty(fixture.AssetContext.Asset);
            }

            [Fact]
            public async void WhenNonExistingIdProvided_NotFoundExceptionThrown()
            {
                await Assert.ThrowsAsync<AssetNotFoundException>(() => fixture.AssetService.DeleteAsync(2));
            }
        }

        public class GetAsyncMethod : MethodBase<ExistSomeAssetsFixture>
        {
            public GetAsyncMethod(ExistSomeAssetsFixture fixture) : base(fixture) { }

            [Fact]
            public async void WhenIdOfAssetProvided_AssetIsReturned()
            {
                var asset = await fixture.AssetService.GetAsync(1);
                Assert.NotNull(asset);
            }

            [Fact]
            public async void WhenNonExistingIdProvided_NotFoundExceptionThrown()
            {
                await Assert.ThrowsAsync<AssetNotFoundException>( () => fixture.AssetService.GetAsync(2) );
            }
        }

        public class UpdateAsyncMethod : MethodBase<ExistSomeAssetsFixture>
        {
            public UpdateAsyncMethod(ExistSomeAssetsFixture fixture) : base(fixture) { }

            [Fact]
            public async void WhenAssetDataProvided_AssetIsUpdated()
            {
                await fixture.AssetService.UpdateAsync(new AssetDto()
                {
                    AssetName = "Test asset updated",
                    Department = 1,
                    Broken = "false",
                    Country = "LVA",
                    Email = "test@test.com",
                    PurchaseDate = "01/01/2021",
                    ID = 1
                });
                Assert.True(fixture.AssetContext.Asset.First().AssetName == "Test asset updated");
            }
            [Fact]
            public async void WhenNonExistingIdProvided_NotFoundExceptionThrown()
            {
                await Assert.ThrowsAsync<AssetNotFoundException>(() => fixture.AssetService.UpdateAsync(new AssetDto()
                {
                    AssetName = "Test asset updated",
                    Department = 1,
                    Broken = "false",
                    Country = "LVA",
                    Email = "test@test.com",
                    PurchaseDate = "01/01/2021",
                    ID = 2
                }));
            }
        }
    }
}
