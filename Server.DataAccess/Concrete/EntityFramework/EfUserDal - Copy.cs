using Server.DataAccess.Abstract;
using Server.DataAccess.EntityFramework;
using Shared.Lib.DataAccess.EntityFramework;
using Shared.Lib.Entities;

namespace Server.DataAccess.Concrete.EntityFramework
{
    public class EfRefreshTokenDal : EfEntityRepositoryBase<AppRefreshToken,AppDBContext> , IRefreshTokenDal
    {
        public EfRefreshTokenDal(AppDBContext context) : base(context)
        {
        }
    }
}
