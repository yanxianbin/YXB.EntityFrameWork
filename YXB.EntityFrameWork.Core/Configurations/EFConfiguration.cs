using MySql.Data.MySqlClient;
using Polly;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.SqlServer;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YXB.EntityFrameWork.Core.Configurations
{
    public class EFConfiguration:DbConfiguration
    {
        public Policy _policy;
        public EFConfiguration()
        {
            SetModelStore(new DefaultDbModelStore(Directory.GetCurrentDirectory()));
            SetManifestTokenResolver(new ManifestTokenResolver());

            _policy = Policy.Handle<Exception>().CircuitBreaker(3, TimeSpan.FromSeconds(60));
            //执行策略
            SetExecutionStrategy("MySql.Data.MySqlClient", ()=>new BreakExecuteStrategy(_policy));
            DbInterception.Add(new EFDbInterceptor());
        }
    }
}
