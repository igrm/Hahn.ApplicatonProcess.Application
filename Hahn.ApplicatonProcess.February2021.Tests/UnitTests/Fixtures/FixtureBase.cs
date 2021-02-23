using AutoMapper;
using Hahn.ApplicatonProcess.February2021.Data.Infrastructure;
using Hahn.ApplicatonProcess.February2021.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Moq;
using Microsoft.EntityFrameworkCore;
using Hahn.ApplicatonProcess.February2021.Domain.Models.Business;
using Hahn.ApplicatonProcess.February2021.Domain.Models.DTO;
using Hahn.ApplicatonProcess.February2021.Data.Services;
using Hahn.ApplicatonProcess.February2021.Data.Repositories;
using Moq.Protected;
using System.Dynamic;
using System.Globalization;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.February2021.Tests.UnitTests.Fixtures
{
    public abstract class FixtureBase
    {
        public AssetContext AssetContext { get; set; }
        public IAssetRepository AssetRepository { get; set; }
        public ITopLevelDomainRepository TopLevelDomainRepository { get; set; }
        public ICountryRepository CountryRepository { get; set; }
        public ISettingsService SettingsService { get; set; }
        public ILogger<ListOfValuesService> GenericLogger { get; set; }
        public IMemoryCache MemoryCache { get; set; }
        public IMapper Mapper { get; set; }
        public HttpClient HttpClient { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }

        public FixtureBase()
        {
            var options = new DbContextOptionsBuilder<AssetContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            AssetContext = new AssetContext(options);
            UnitOfWork = new UnitOfWork(AssetContext);
            MemoryCache = new MemoryCache(new MemoryCacheOptions());

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<Asset>(It.IsAny<AssetDto>())).Returns(new Asset()
            {
                ID = 1,
                AssetName = "Test asset updated",
                Department = Department.HQ,
                Broken = false,
                CountryOfDepartment = "LVA",
                EMailAdressOfDepartment = "test@test.com",
                PurchaseDate = DateTime.UtcNow
            });
            mapperMock.Setup(x => x.Map<AssetDto>(It.IsAny<Asset>())).Returns(new AssetDto()
            {
                AssetName = "Test asset",
                Department = 1,
                Broken = "false",
                Country = "LVA",
                Email = "test@test.com",
                PurchaseDate = "01/01/2021"
            });

            mapperMock.Setup(x => x.Map<DepartmentDto>(It.IsAny<Department>())).Returns(new DepartmentDto());
            mapperMock.Setup(x => x.Map<CountryDto>(It.IsAny<ExpandoObject>())).Returns(new CountryDto());

            Mapper = mapperMock.Object;
            HttpClient = new HttpClient();
            GenericLogger = new Mock<ILogger<ListOfValuesService>>().Object;

            var settingsServiceMock = new Mock<ISettingsService>();
            settingsServiceMock.Setup(x => x.GetRestCountriesUri()).Returns(new Uri("http://localhost/countries-test"));
            settingsServiceMock.Setup(x => x.GetTopLevelDomainsUri()).Returns(new Uri("http://localhost/domains-test"));
            settingsServiceMock.Setup(x => x.GetExcludeCountries()).Returns(new string[1] { "GBR" });
            settingsServiceMock.Setup(x => x.GetCurrentCulture()).Returns(new CultureInfo("en-US"));

            SettingsService = settingsServiceMock.Object;

            var countryRepositoryMock = new Mock<CountryRepository>(MemoryCache, HttpClient, Mapper);
            countryRepositoryMock.Protected().Setup<Task<HttpResponseMessage>>("Fetch", new Uri("http://localhost/countries-test"))
                .Returns((Uri uri) =>
                {
                    return Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                    {
                        Content = new StringContent(String.Empty)
                    });
                });
            countryRepositoryMock.Protected().As<IRawResponseParsing>().Setup<IList<CountryDto>>(x => x.ParseRawResponse<CountryDto>(String.Empty))
                .Returns((String x) =>
                {
                    return new List<CountryDto>() { new CountryDto() };
                });
            CountryRepository = countryRepositoryMock.Object;

            var topLevelDomainRepositoryMock = new Mock<TopLevelDomainRepository>(MemoryCache, HttpClient, Mapper);
            topLevelDomainRepositoryMock.Protected().Setup<Task<HttpResponseMessage>>("Fetch", new Uri("http://localhost/domains-test"))
                  .Returns((Uri uri) =>
                    {
                        return Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                        {
                            Content = new StringContent(String.Empty)
                        });
                    });
            topLevelDomainRepositoryMock.Protected().As<IRawResponseParsing>().Setup<IList<string>>(x => x.ParseRawResponse<string>(String.Empty))
                .Returns((String x) =>
                {
                    return new List<string>() { String.Empty };
                });
            TopLevelDomainRepository = topLevelDomainRepositoryMock.Object;

            AssetRepository = new AssetRepository(AssetContext);

        }
    }
}
