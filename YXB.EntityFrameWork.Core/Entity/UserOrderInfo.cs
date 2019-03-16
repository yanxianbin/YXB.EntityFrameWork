using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YXB.EntityFrameWork.Core.Entity
{
    public class UserOrderInfo:BaseEntity
    {
        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNumber { get; set; }

        /// <summary>
        /// 订单描述
        /// </summary>
        public int OrderDesc { get; set; }

        /// <summary>
        /// 寄送地址
        /// </summary>
        public string SendAddress { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary>
        public virtual UserInfo UserInfo { get; set; }
    }
}
