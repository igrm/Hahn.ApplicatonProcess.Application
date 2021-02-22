using System;
using System.Collections.Generic;
using System.Text;

namespace Hahn.ApplicatonProcess.February2021.Domain.Interfaces
{
    interface IAssetDto
    {
        string? AssetName { get; set; }
        int? Department { get; set; }
        string? Country { get; set; }
        string? Email { get; set; }
        string? PurchaseDate { get; set; }
        string? Broken { get; set; }
    }
}
