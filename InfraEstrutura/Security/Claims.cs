using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduManager.InfraEstrutura.Security
{
    public class Claims
    {
        public const string CanManageUsers = "CanManageUsers";
        public const string CanViewReports = "CanViewReports";
        public const string CanManageCourses = "CanManageCourses";
        public const string CanManageGrades = "CanManageGrades";
        public const string CanViewOwngrades = "CanViewOwngrades";
    }
}