using FluentValidation;
using Hahn.ApplicatonProcess.February2021.Domain.Models.Business;
using Hahn.ApplicatonProcess.February2021.Domain.Models.DTO;
using Hahn.ApplicatonProcess.February2021.Domain.Utils;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;

namespace Hahn.ApplicatonProcess.February2021.Domain.Validators
{
    public class AssetValidator : AbstractValidator<WithMemoryCache<AssetDto>>
    {
        public AssetValidator()
        {
            var culture = Thread.CurrentThread.CurrentCulture;

            RuleFor(x => x.Instance.AssetName).NotEmpty()
                .OverridePropertyName("assetName");

            RuleFor(x => x.Instance.AssetName).MinimumLength(5)
                .OverridePropertyName("assetName");

            RuleFor(x => x.Instance.Department).NotEmpty()
                .OverridePropertyName("department");

            Transform(x => x.Instance.Department, x => x.HasValue ? (Department)x.Value : (Department)int.MinValue).IsInEnum()
                .OverridePropertyName("department");

            RuleFor(x => x.Instance.Country).NotEmpty()
                .OverridePropertyName("country"); 
            
            RuleFor(x => x).Must(x =>
            {
                if (x.MemoryCache.TryGetValue(Constants.ALL_COUNTRIES_CACHE_KEY, out List<CountryDto> countries))
                {
                    return countries.Exists(y => y.Alpha3Code == x.Instance.Country);
                }
                return false;
            }).WithMessage("Wrong country code.")
            .OverridePropertyName("country");

            RuleFor(x=>x.Instance.PurchaseDate).NotEmpty()
                .OverridePropertyName("purchaseDate"); 

            RuleFor(x => x.Instance.PurchaseDate).Must(x => DateTime.TryParse(x, culture, DateTimeStyles.AssumeUniversal, out DateTime result))
                .WithMessage("Wrong date format.")
                .OverridePropertyName("purchaseDate");

            Transform(x => x.Instance.PurchaseDate, x => String.IsNullOrEmpty(x) ? DateTime.MinValue : DateTime.Parse(x, culture))
                .InclusiveBetween(DateTime.UtcNow.Date.AddYears(-1), DateTime.UtcNow.Date)
                .OverridePropertyName("purchaseDate");

            RuleFor(x => x.Instance.Email).NotEmpty().OverridePropertyName("email");

            RuleFor(x => x).Must(x =>
            {
                if (x.MemoryCache.TryGetValue(Constants.ALL_TOP_LEVEL_DOMAINS_CACHE_KEY, out List<string> topLevelDomains))
                {
                    return topLevelDomains.Exists(y => !string.IsNullOrEmpty(x.Instance.Email) && x.Instance.Email.EndsWith(y));
                }
                return false;
            }).WithMessage("Wrong top level domain in email address.").OverridePropertyName("email");


            RuleFor(x => x.Instance.Broken).Must(x => string.IsNullOrEmpty(x) || Boolean.TryParse(x, out Boolean result))
                .WithMessage("true or false are expected.")
                .OverridePropertyName("broken");
        }

    }
}
