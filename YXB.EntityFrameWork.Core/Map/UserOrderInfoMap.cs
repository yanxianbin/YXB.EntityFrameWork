using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YXB.EntityFrameWork.Core.Entity;

namespace YXB.EntityFrameWork.Core.Map
{
    public class UserOrderInfoMap:EntityBaseMap<UserOrderInfo>
    {
        public UserOrderInfoMap()
        {
            HasIndex(x => x.ID);
        }
    }
}
