using System;
using System.Collections.Generic;
using System.Text;

namespace Hahn.ApplicatonProcess.February2021.Domain.Interfaces
{
    public interface IRawResponseParsing
    {
        IList<T> ParseRawResponse<T>(string responseText) where T : class;
    }
}
