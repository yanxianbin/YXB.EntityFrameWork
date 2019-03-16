using MySql.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using YXB.EntityFrameWork.Core.Configurations;
using YXB.EntityFrameWork.Core.Conventions;
using YXB.EntityFrameWork.Core.Map;

namespace YXB.EntityFrameWork.Core
{
   [DbConfigurationType(typeof(EFConfiguration))]
    public class DBConnection:DbContext
    {
        public DBConnection() : base("name=EFConnection")
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<DBConnection>());
            Configuration.AutoDetectChangesEnabled = true;
            Configuration.EnsureTransactionsForFunctionsAndCommands = true;
            Configuration.UseDatabaseNullSemantics = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(new DataPropertyConvention());
            var configTypes = Assembly.GetExecutingAssembly().GetTypes().Where(x=> {
                var baseType = x.BaseType;
                if (baseType == null)
                {
                    return false;
                }
                if (!baseType.IsGenericType)
                {
                    return false;
                }
                if (baseType.GetGenericTypeDefinition() == typeof(EntityBaseMap<>))
                {
                    return true;
                }
                return false;
            });
            foreach (var t in configTypes)
            {
                dynamic config =Activator.CreateInstance(t);
                modelBuilder.Configurations.Add(config);
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
