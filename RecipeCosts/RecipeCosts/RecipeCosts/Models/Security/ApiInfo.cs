using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeCosts.Model.Security
{
    public class ApiInfo
    {
        public string ApiName { get; set; }
        public byte[] AesKey { get; set; }
        public byte[] AesIV { get; set; }
        public string ApiKeyEncrypted { get; set; }
    }
}
