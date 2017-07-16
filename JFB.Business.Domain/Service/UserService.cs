using JFB.Business.Domain.Info;
using JFB.Business.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViData;

namespace JFB.Business.Domain.Service
{
    public class UserService : Repository<UserInfo, User>
    {
        public int hasLinked(int userId)
        {
            var item = this.GetById(userId);
            if (item != null && !string.IsNullOrEmpty(item.RealName))
            {
                return 1;
            }
            return 0;
        }
    }
}
