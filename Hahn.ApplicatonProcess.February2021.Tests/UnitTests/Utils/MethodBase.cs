using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Hahn.ApplicatonProcess.February2021.Tests.UnitTests.Utils
{
    public class MethodBase<T>: IClassFixture<T> where T: class
    {
        protected T fixture;

        public MethodBase(T parameter)
        {
            fixture = parameter;
        }
    }
}
