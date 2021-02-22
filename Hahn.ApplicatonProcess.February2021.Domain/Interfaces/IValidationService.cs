using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using FluentValidation.Results;
using Hahn.ApplicatonProcess.February2021.Domain.Models.DTO;
using Hahn.ApplicatonProcess.February2021.Domain.Utils;

namespace Hahn.ApplicatonProcess.February2021.Domain.Interfaces
{
    public interface IValidationService
    {
        ValidationResult Validate<U, V>(U item) where V : AbstractValidator<WithMemoryCache<AssetDto>>, new();
    }
}
