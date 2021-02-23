using AutoMapper;
using Hahn.ApplicatonProcess.February2021.Domain.Exceptions;
using Hahn.ApplicatonProcess.February2021.Domain.Interfaces;
using Hahn.ApplicatonProcess.February2021.Domain.Models.Business;
using Hahn.ApplicatonProcess.February2021.Domain.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hahn.ApplicatonProcess.February2021.Domain.Utils;

namespace Hahn.ApplicatonProcess.February2021.Data.Services
{
    public class AssetService : IAssetService
    {
        private readonly IMapper _mapper;
        private readonly IAssetRepository _assetRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AssetService(IMapper mapper, IAssetRepository assetRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _assetRepository = assetRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<int> CreateAsync(AssetDto asset)
        {
            var toInsert = _mapper.Map<Asset>(asset);
            _assetRepository.Add(toInsert);
            await _unitOfWork.CommitAsync();
            return toInsert.ID;
        }

        public async Task DeleteAsync(int id)
        {
            var toDelete = await _assetRepository.GetAsync(x => x.ID == id);
            if (toDelete is null)
            {
                throw new AssetNotFoundException(id.ToString());
            }
            _assetRepository.Delete(toDelete);
            await _unitOfWork.CommitAsync();
        }

        public async Task<AssetDto?> GetAsync(int id)
        {
            var result = await _assetRepository.GetAsync(x => x.ID == id);
            if (result is null)
            {
                throw new AssetNotFoundException($"{id}");
            }
            return _mapper.Map<AssetDto>(result);
        }

        public async Task UpdateAsync(AssetDto asset)
        {
            var existing = await _assetRepository.GetAsync(x => x.ID == asset.ID);
            if (asset.ID.HasValue && existing is not null)
            {
                var toUpdate = _mapper.Map<Asset>(asset);
                existing.MatchPropertiesFrom(toUpdate);
                _assetRepository.Update(existing);
                await _unitOfWork.CommitAsync();
            }
            else
            {
                throw new AssetNotFoundException($"{asset.ID}");
            }
        }
    }
}
