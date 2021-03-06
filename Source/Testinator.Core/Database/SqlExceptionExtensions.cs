﻿using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Linq;

namespace Testinator.Core
{
    /// <summary>
    /// Base class for basic SQL-related error handling
    /// </summary>
    public static class SqlExceptionExtensions
    {
        public static bool ForeginKeyViolation(this SqlException ex)
        {
            return ex.Errors.OfType<SqlError>()
                .Any(sq => sq.Number == 574);
        }

        public static bool ForeginKeyViolation(this DbUpdateException ex)
        {
            if (ex.InnerException is DbUpdateException uEx)
            {
                return uEx.InnerException is SqlException sqlEx && sqlEx.ForeginKeyViolation();
            }

            return false;
        }
    }
}
