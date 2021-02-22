using Hahn.ApplicatonProcess.February2021.Domain.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

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
        public string AssetName { get; set; }

        [Required]
        public Department Department { get; set; }

        [Required]
        public string CountryOfDepartment { get; set; }

        [Required]
        public string EMailAdressOfDepartment { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; }

        public bool Broken { get; set; }

    }
}
