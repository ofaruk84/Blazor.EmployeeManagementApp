using Shared.Lib.DataAccess;
using Shared.Lib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DataAccess.Abstract
{
    public interface ISystemRoleDal : IEntityRepository<SystemRole>
    {
    }
}
