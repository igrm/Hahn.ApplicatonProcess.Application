using Hahn.ApplicatonProcess.February2021.Data.Services;
using Hahn.ApplicatonProcess.February2021.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hahn.ApplicatonProcess.February2021.Tests.UnitTests.Fixtures
{
    public class ListOfValuesFixture: FixtureBase
    {
        public IListOfValuesService ListOfValuesService { get; set; }
        
        public ListOfValuesFixture() : base()
        {
            ListOfValuesService = new ListOfValuesService(Mapper, CountryRepository, SettingsService, TopLevelDomainRepository, MemoryCache, GenericLogger);
        }
    }
}
