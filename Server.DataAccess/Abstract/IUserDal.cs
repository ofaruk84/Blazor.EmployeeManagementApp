using Shared.Lib.DataAccess;
using Shared.Lib.Entities;


namespace Server.DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<ApplicationUser>
    {

    }
}
