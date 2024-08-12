using Server.Business.Abstract;
using Server.Business.Security.JWT;
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
        private readonly JWTHandler  _jWTHandler;


        public UserManager(IUserDal userDal, ISystemRoleDal systemRoleDal, IUserRoleDal userRoleDal, JWTHandler jWTHandler)
        {
            _userDal = userDal;
            _userRoleDal = userRoleDal;
            _systemRoleDal = systemRoleDal;
            _jWTHandler = jWTHandler;
        }


        public async Task<GeneralResponse> CreateUser(RegisterDto registerDto)
        {
            if (registerDto is null) return new GeneralResponse(false, "Model is empty");

            var user = await FindByEmail(registerDto.Email!);

            if (user is not null) return new GeneralResponse(false, "User Registered Already");

            var applicationUser = new ApplicationUser {
                Email = registerDto.Email,
                Name = registerDto.FullName,
                Password = BcryptHasher.HashPassword(registerDto.Password!)
            };

        
            
            await _userDal.AddAsync(applicationUser);

            await AddUserRole(registerDto.Email!, registerDto.IsAdmin);


            return new GeneralResponse(true, "User Created");

        }
        private async Task<ApplicationUser?> FindByEmail(string email)
        {
            var user = await _userDal.GetAsync(x => x.Email!.Equals(email));

            return user;
        }
        private async Task AddUserRole(string email,bool isAdmin)
        {
            var user = await _userDal.GetAsync(x => x.Email!.Equals(email));
            if (user == null) throw new Exception("User not found");

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

        public async Task<LoginResponse> SignIn(LoginDto loginDto)
        {
            if (loginDto is null) return new LoginResponse(false,Message:"Modal is empty");


            var user = await FindByEmail(loginDto.Email!);
            if (user is null) return new LoginResponse(false, Message: "UserNotFound");

            var isValidPassword = BcryptHasher.VerifyPassword(loginDto.Password!,user.Password!);
            if(!isValidPassword) return new LoginResponse(false, Message: "Email/Password not valid");

            var userRole = await _userRoleDal.GetAsync(x=>x.UserId.Equals(user.Id));
            if (userRole is null) return new LoginResponse(false, Message: "User Role Not Found");

            var userRoleName = await _systemRoleDal.GetAsync(x=>x.Id == userRole.RoleId);

            var token = _jWTHandler.GenerateToken(user, userRoleName!.Name!);
            var refreshToken = _jWTHandler.GenerateRefreshToken();

            return new  LoginResponse(true, Message: "User successfully logged in", token, refreshToken);

        }
    }
}
