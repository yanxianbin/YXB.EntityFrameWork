using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YXB.EntityFrameWork.Core.Configurations
{
    public class ManifestTokenResolver : IManifestTokenResolver
    {
        private readonly IManifestTokenResolver _re = new DefaultManifestTokenResolver();
        public string ResolveManifestToken(DbConnection connection)
        {
            return _re.ResolveManifestToken(connection);
        }
    }
}
