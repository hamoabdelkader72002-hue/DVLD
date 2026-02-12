using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public static class clsDataValidation
    {
        public static bool HasData(this DataTable DT)
        {
            return DT != null && DT.Rows.Count > 0;
        }
    }
}
