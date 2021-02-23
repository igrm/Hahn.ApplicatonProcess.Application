using Hahn.ApplicatonProcess.February2021.Tests.UnitTests.Fixtures;
using Hahn.ApplicatonProcess.February2021.Tests.UnitTests.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Hahn.ApplicatonProcess.February2021.Tests.UnitTests.Facts
{
    public class ListOfValuesServiceFacts
    {
        public class GetAllCountriesAsyncMethod: MethodBase<ListOfValuesFixture>
        {
            public GetAllCountriesAsyncMethod(ListOfValuesFixture fixture):base(fixture) { }

            [Fact]
            public async void WhenListOfCountriesRequested_ListOfCountrtiesProvided()
            {
                var result = await fixture.ListOfValuesService.GetAllCountriesAsync();
                Assert.NotEmpty(result);
            }
        }

        public class GetAllDepartmentsAsyncMethod : MethodBase<ListOfValuesFixture>
        {
            public GetAllDepartmentsAsyncMethod(ListOfValuesFixture fixture) : base(fixture) { }

            [Fact]
            public async void WhenListOfDepartmentsRequested_ListOfDepartmentsProvided()
            {
                var result = await fixture.ListOfValuesService.GetAllDepartmentsAsync();
                Assert.NotEmpty(result);
            }
        }

        public class GetAllTopLevelDomainsAsyncMethod : MethodBase<ListOfValuesFixture>
        {
            public GetAllTopLevelDomainsAsyncMethod(ListOfValuesFixture fixture) : base(fixture) { }

            [Fact]
            public async void WhenListOfAllTopLevelDomainsRequested_ListOfAllTopLevelDomainsProvided()
            {
                var result = await fixture.ListOfValuesService.GetAllTopLevelDomainsAsync();
                Assert.NotEmpty(result);
            }
        }
    }
}
