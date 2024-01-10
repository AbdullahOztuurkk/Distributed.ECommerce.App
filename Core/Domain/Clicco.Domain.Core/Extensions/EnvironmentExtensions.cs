using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clicco.Domain.Core.Extensions
{
    public static class EnvironmentExtensions
    {
        private static readonly string env = "." + Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        public static string Env => env;
    }
}
