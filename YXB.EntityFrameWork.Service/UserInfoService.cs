using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YXB.EntityFrameWork.Core;
using YXB.EntityFrameWork.Repository;

namespace YXB.EntityFrameWork.Service
{
    public class UserInfoService
    {
        private Repository<UserInfo> repository;
        public UserInfoService(Repository<UserInfo> _repository)
        {
            repository = _repository;
        }

        public List<UserInfo> GetUsers()
        {
            var data= Repository<UserInfo>.Instance.Find(x => x.IsLock == "否").ToList();
            return data;
        }
    }
}
