using System;
using System.Collections.Generic;
using System.Text;

namespace Hahn.ApplicatonProcess.February2021.Domain.Exceptions
{
    public class AssetNotFoundException : Exception
    {
        public AssetNotFoundException(string message): base(message)  {  }
    }
}
