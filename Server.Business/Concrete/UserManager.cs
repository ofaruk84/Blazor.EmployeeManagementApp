using Server.Business.Abstract;
using Server.Business.Security.JWT;
using Server.DataAccess.Abstract;
using Shared.Lib.DTOs;
using Shared.Lib.Entities;
using Shared.Lib.Responses;
using Server.Business.Utilities;
using Server.Business.Security;

namespace Server.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IUserRoleDal _userRoleDal;
        private readonly ISystemRoleDal _systemRoleDal;
        private readonly IRefreshTokenDal _refreshTokenDal;
        private readonly JWTHandler  _jWTHandler;


        public UserManager(IUserDal userDal, ISystemRoleDal systemRoleDal, IUserRoleDal userRoleDal, JWTHandler jWTHandler, IRefreshTokenDal refreshTokenDal)
        {
            _userDal = userDal;
            _userRoleDal = userRoleDal;
            _systemRoleDal = systemRoleDal;
            _jWTHandler = jWTHandler;
            _refreshTokenDal = refreshTokenDal;
        }


        public async Task<GeneralResponse> CreateUser(RegisterDto registerDto)
        {
            if (registerDto is null) return new GeneralResponse(false, Message:Messages.EmptyModal);

            var user = await FindUserByEmail(registerDto.Email!);

            if (user is not null) return new GeneralResponse(false, Message:Messages.ExistingUser);

            var applicationUser = new ApplicationUser {
                Email = registerDto.Email,
                Name = registerDto.FullName,
                Password = BcryptHasher.HashPassword(registerDto.Password!)
            };

        
            
            await _userDal.AddAsync(applicationUser);

            await AddUserRole(registerDto.Email!, registerDto.IsAdmin);


            return new GeneralResponse(true, Message:Messages.UserCreated);

        }
        private async Task<ApplicationUser?> FindUserByEmail(string email)
        {
            var user = await _userDal.GetAsync(x => x.Email!.Equals(email));

            return user;
        }
        private async Task<ApplicationUser?> FindUserById(int userId)
        {
            var user = await _userDal.GetAsync(x => x.Id!.Equals(userId));

            return user;
        }
        private async Task AddUserRole(string email,bool isAdmin)
        {
            var user = await _userDal.GetAsync(x => x.Email!.Equals(email));
            if (user == null) throw new Exception(Messages.UserNotFound);

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
            if (loginDto is null) return new LoginResponse(false,Message:Messages.EmptyModal);


            var user = await FindUserByEmail(loginDto.Email!);
            if (user is null) return new LoginResponse(false, Message: Messages.UserNotFound);

            var isValidPassword = BcryptHasher.VerifyPassword(loginDto.Password!,user.Password!);
            if(!isValidPassword) return new LoginResponse(false, Message: Messages.InvalidCred);

            var userRole = await _userRoleDal.GetAsync(x=>x.UserId.Equals(user.Id));
            if (userRole is null) return new LoginResponse(false, Message: Messages.UserRoleNotFound);

            var userRoleName = await _systemRoleDal.GetAsync(x=>x.Id == userRole.RoleId);

            var token = _jWTHandler.GenerateToken(user, userRoleName!.Name!);
            var refreshToken = _jWTHandler.GenerateRefreshToken();

            await _refreshTokenDal.AddAsync(new AppRefreshToken
            {
                Token = refreshToken,
                UserId = user.Id,
            });

            return new  LoginResponse(true, Message: Messages.UserLoggedIn, token, refreshToken);
        }
        private async Task<UserRole?> FindUserRoleByUserId(int userId)
        {
            return await _userRoleDal.GetAsync(x => x.UserId.Equals(userId));

        }

        private async Task<SystemRole?> FindSystemRoleByRoleId(int roleId)
        {
           return await _systemRoleDal.GetAsync(x => x.Id == roleId);
        }
        public async Task<LoginResponse> RefreshToken(AppRefreshToken refreshToken)
        {
            if (refreshToken is null) return new LoginResponse(false, Message: Messages.EmptyModal);

            var userRefreshToken = await _refreshTokenDal.GetAsync(x => x.Token!.Equals(refreshToken.Token));
            if (userRefreshToken is null) return new LoginResponse(false, Messages.RefreshTokenNotFound);


            var user = await FindUserById(userRefreshToken.UserId);
            if (user is null) return new LoginResponse(false,Message: Messages.RefreshTokenNotFound);

            var userRole = await FindUserRoleByUserId(user.Id);
            if (userRole is null) return new LoginResponse(false, Message: Messages.UserNotFound);

            var systemRole = await FindSystemRoleByRoleId(userRole.RoleId);
            if (refreshToken is null) return new LoginResponse(false, Message: Messages.SystemRoleNotFound);

            var jwt = _jWTHandler.GenerateToken(user, systemRole!.Name!);
            var newRefreshToken = _jWTHandler.GenerateRefreshToken();

            userRefreshToken.Token = newRefreshToken;

            await _refreshTokenDal.UpdateAsync(userRefreshToken);

            return new LoginResponse(true,Message:"",jwt,newRefreshToken);

        }
    }
}
