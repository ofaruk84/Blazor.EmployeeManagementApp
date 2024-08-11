using Server.DataAccess.Abstract;
using Server.DataAccess.EntityFramework;
using Shared.Lib.DataAccess.EntityFramework;
using Shared.Lib.Entities;

namespace Server.DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<ApplicationUser,AppDBContext> , IUserDal
    {
        public EfUserDal(AppDBContext context) : base(context)
        {
        }
    }
}
