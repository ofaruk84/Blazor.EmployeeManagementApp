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

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
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

            return new GeneralResponse(false, "User Created");

        }

        public Task<LoginResponse> SignIn(LoginDto loginDto)
        {
            throw new NotImplementedException();
        }
    }
}
