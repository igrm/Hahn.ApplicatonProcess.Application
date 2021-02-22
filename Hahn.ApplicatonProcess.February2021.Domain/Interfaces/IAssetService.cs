using Hahn.ApplicatonProcess.February2021.Domain.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.February2021.Domain.Interfaces
{
    public interface IAssetService
    {
        Task<int> CreateAsync(AssetDto asset);

        Task UpdateAsync(AssetDto asset);

        Task<AssetDto?> GetAsync(int id);

        Task DeleteAsync(int id);
    }
}
