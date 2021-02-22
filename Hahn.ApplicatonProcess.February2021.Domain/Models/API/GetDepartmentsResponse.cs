using Hahn.ApplicatonProcess.February2021.Domain.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hahn.ApplicatonProcess.February2021.Domain.Models.API
{
    public class GetDepartmentsResponse
    {
        public GetDepartmentsResponse()
        {
            Departments = new List<DepartmentDto>();
        }
        /// <summary>
        /// list of department data transfer object
        /// </summary>
        public IList<DepartmentDto> Departments { get; set; }
    }
}
