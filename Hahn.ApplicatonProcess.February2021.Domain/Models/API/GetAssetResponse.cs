using Hahn.ApplicatonProcess.February2021.Domain.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hahn.ApplicatonProcess.February2021.Domain.Models.API
{
    public class GetAssetResponse
    {
        /// <summary> The asset data transfer object </summary>
        public AssetDto? Asset { get; set; }
    }
}
