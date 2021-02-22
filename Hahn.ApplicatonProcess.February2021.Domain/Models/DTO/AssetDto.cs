using Hahn.ApplicatonProcess.February2021.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hahn.ApplicatonProcess.February2021.Domain.Models.DTO
{
    public class AssetDto : IIdentified, IAssetDto
    {
        /// <summary>The asset unique identifier, maps to Asset entity, ID property </summary>
        /// <example>1</example>
        public int? ID { get; set; }
        
        /// <summary>The name of the asset, maps to Asset entity, AssetName property </summary>
        /// <example>The test computer</example>
        public string? AssetName { get; set; }

        /// <summary>The department of the asset, maps to Asset entity, Department property </summary>
        /// <example>1</example>
        public int? Department { get; set; }

        /// <summary>The asset department country, maps to Asset entity, CountryOfDepartment property </summary>
        /// <example>BEL</example>
        public string? Country { get; set; }

        /// <summary>The asset department email, maps to Asset entity, EMailAdressOfDepartment property </summary>
        /// <example>info@department.eu</example>
        public string? Email { get; set; }

        /// <summary>The asset purchase date, maps to Asset entity, PurchaseDate property </summary>
        /// <example>01/01/2021</example>
        public string? PurchaseDate { get; set; }

        /// <summary>Indicates if asset is broken</summary>
        /// <example>false</example>
        public string? Broken { get; set; }
    }
}
