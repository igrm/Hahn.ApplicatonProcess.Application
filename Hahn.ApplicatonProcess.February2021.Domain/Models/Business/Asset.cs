using Hahn.ApplicatonProcess.February2021.Domain.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using Hahn.ApplicatonProcess.February2021.Domain.Utils;

namespace Hahn.ApplicatonProcess.February2021.Domain.Models.Business
{
    public class Asset : IEntity, IAsset
    {
        public Asset()
        {
            AssetName = String.Empty;
            CountryOfDepartment = String.Empty;
            EMailAdressOfDepartment = String.Empty;
            PurchaseDate = DateTime.UtcNow;
        }

        [Key]
        public int ID { get; set; }

        [Required]
        [MatchParent("AssetName")]
        public string AssetName { get; set; }

        [Required]
        [MatchParent("Department")]
        public Department Department { get; set; }

        [Required]
        [MatchParent("CountryOfDepartment")]
        public string CountryOfDepartment { get; set; }

        [Required]
        [MatchParent("EMailAdressOfDepartment")]
        public string EMailAdressOfDepartment { get; set; }

        [Required]
        [MatchParent("PurchaseDate")]
        public DateTime PurchaseDate { get; set; }

        [MatchParent("Broken")]
        public bool Broken { get; set; }

    }
}
