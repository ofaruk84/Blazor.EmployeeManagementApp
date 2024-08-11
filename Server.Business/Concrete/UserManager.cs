using Server.Business.Abstract;
using Server.DataAccess.Abstract;
using Shared.Lib.DTOs;
using Shared.Lib.Entities;
using Shared.Lib.Responses;
using Shared.Lib.Utilities;


namespace Server.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IUserRoleDal _userRoleDal;
        private readonly ISystemRoleDal _systemRoleDal;


        public UserManager(IUserDal userDal, ISystemRoleDal systemRoleDal, IUserRoleDal userRoleDal)
        {
            _userDal = userDal;
            _userRoleDal = userRoleDal;
            _systemRoleDal = systemRoleDal;
        }


        public async Task<GeneralResponse> CreateUser(RegisterDto registerDto)
        {
            if (registerDto is null) return new GeneralResponse(false, "Model is empty");

            var user = _userDal.GetAsync(x => x.Email!.Equals(registerDto.Email));

            if(user is not null) return new GeneralResponse(false, "User Registered Already");

            var applicationUser = new ApplicationUser {
                Email = registerDto.Email,
                Name = registerDto.FullName,
                Password = BcryptHasher.HashPassword(registerDto.Password!)
            };

        
            
            await _userDal.AddAsync(applicationUser);

            await AddUserRole(registerDto.Email!, registerDto.IsAdmin);


            return new GeneralResponse(true, "User Created");

        }

        private async Task AddUserRole(string email,bool isAdmin)
        {
            var user = _userDal.GetAsync(x => x.Email!.Equals(email));
            var addedUserRole = new UserRole
            {
                UserId = user.Id
            };

            if (isAdmin)
            {
                var adminRole = await _systemRoleDal.GetAsync(x => x.Name!.Equals(Constans.AdminRole));
                addedUserRole.RoleId = adminRole!.Id;
            }
            else
            {
                var userRole = await _systemRoleDal.GetAsync(x => x.Name!.Equals(Constans.UserRole));
                addedUserRole.RoleId = userRole!.Id;
            }

            await _userRoleDal.AddAsync(addedUserRole);
        }

        public Task<LoginResponse> SignIn(LoginDto loginDto)
        {
            throw new NotImplementedException();
        }
    }
}
