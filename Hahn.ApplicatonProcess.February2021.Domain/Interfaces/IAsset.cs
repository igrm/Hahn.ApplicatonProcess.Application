using Hahn.ApplicatonProcess.February2021.Domain.Models.Business;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hahn.ApplicatonProcess.February2021.Domain.Interfaces
{
    public interface IAsset
    {
        string AssetName { get; set; }

        Department Department { get; set; }

        string CountryOfDepartment { get; set; }

        string EMailAdressOfDepartment { get; set; }

        DateTime PurchaseDate { get; set; }

        bool Broken { get; set; }
    }
}
