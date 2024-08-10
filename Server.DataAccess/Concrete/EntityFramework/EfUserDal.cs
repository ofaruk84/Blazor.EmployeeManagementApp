﻿using Server.DataAccess.Abstract;
using Server.DataAccess.EntityFramework;
using Shared.Lib.DataAccess.EntityFramework;
using Shared.Lib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<ApplicationUser,AppDBContext> , IUserDal
    {
    }
}
