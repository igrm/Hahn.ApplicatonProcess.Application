﻿using Hahn.ApplicatonProcess.February2021.Domain.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hahn.ApplicatonProcess.February2021.Domain.Models.API
{
    public class GetCountriesResponse
    {
        public GetCountriesResponse()
        {
            Countries = new List<CountryDto>();
        }
        /// <summary>
        /// List of country data transfer object
        /// </summary>
        public IList<CountryDto> Countries { get; set; }
    }
}
