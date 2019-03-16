using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YXB.EntityFrameWork.Core.Conventions
{
    public class DataPropertyConvention:Convention
    {
        public DataPropertyConvention()
        {
            Properties<string>().Configure(x=>x.HasMaxLength(100));
            Properties().Where(x => x.Name == "ID").Configure(x=> {
                x.IsKey();
                x.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            });
            //统一配置表名
            Types().Configure(x => x.ToTable("TB_"+x.ClrType.Name));
            
        }
    }
}
