using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YXB.EntityFrameWork.Core
{
    public class BaseEntity
    {
        public long ID { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime LastUpdateTime { get; set; }

        public bool IsDel { get; set; }

        public string Remark { get; set; }

    }
}
