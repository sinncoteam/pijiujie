using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViData;

namespace JFB.Business.Domain.Model
{
    public class UserPhotoMap : DMClassMap<UserPhoto>
    {
        public UserPhotoMap()
        {
            Table("t_d_user_photo");
            Id(a => a.ID, "ID").Identity();
            Map(a => a.UserId, "user_id");
            Map(a => a.FatherPhoto, "fatherphoto");
            Map(a => a.ChildPhoto, "childphoto");
            Map(a => a.PerValue, "pervalue");
            Map(a => a.CreateTime, "createtime");
            Map(a => a.PerValueTime, "pervaluetime");
            Map(a => a.IsValid, "isvalid");
        }
    }
}
