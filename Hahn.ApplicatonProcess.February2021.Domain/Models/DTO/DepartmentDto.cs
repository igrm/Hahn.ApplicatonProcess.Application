using Hahn.ApplicatonProcess.February2021.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hahn.ApplicatonProcess.February2021.Domain.Models.DTO
{
    public class DepartmentDto : IIdentified
    {
        /// <summary>
        /// The department identifier, maps to Department enum
        /// </summary>
        /// <example>1</example>
        public int? ID { get; set; }
        /// <summary>
        /// The department translation key, maps to Department enum
        /// </summary>
        /// <example>keyHQ</example>
        public string? DepartmentName { get; set; }
    }
}
