using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YXB.EntityFrameWork.Core.Map
{
    public class EntityBaseMap<TEntity> :EntityTypeConfiguration<TEntity> where TEntity : class,new()
    {
        public EntityBaseMap()
        {
            
        }
    }
}
