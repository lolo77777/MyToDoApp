namespace MyToDo.Api.Services;

public class LoginService : ILoginService
{
    private readonly UnitOfWorkManager _unitOfWorkManager;
    private readonly IMapper _mapper;
    private readonly IBaseRepository<User, int> _usersRepository;

    public LoginService(UnitOfWorkManager unitOfWorkManager, IBaseRepository<User, int> userRepository, IMapper mapper)
    {
        _unitOfWorkManager = unitOfWorkManager;
        _mapper = mapper;
        _usersRepository = userRepository;
    }

    public async Task<Result<UserDto>> LoginAsync(string Account, string Password)
    {
        try
        {
            Password = Password.GetMD5();

            var model = await _usersRepository.Where(x => x.Account.Equals(Account) && x.PassWord.Equals(Password)).FirstAsync();
            if (model == null)
                return Result.Fail<UserDto>("账号或密码错误,请重试！");

            var userDto = new UserDto()
            {
                Account = model.Account,
                UserName = model.UserName,
                Id = model.Id
            };
            return Result.Ok(userDto);
        }
        catch (Exception ex)
        {
            return Result.Fail<UserDto>("登录失败！").WithError(ex.Message);
        }
    }

    public async Task<Result> Register(UserDto user)
    {
        try
        {
            using IUnitOfWork unit = _unitOfWorkManager.Begin();
            var model = _mapper.Map<User>(user);
            var userModel = await _usersRepository.Where(x => x.Account.Equals(model.Account)).FirstAsync();
            if (userModel != null)
                return Result.Fail($"当前账号:{model.Account}已存在,请重新注册！");

            model.CreateDate = DateTime.Now;
            model.PassWord = model.PassWord.GetMD5();
            await _usersRepository.InsertAsync(model);
            unit.Commit();

            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail("注册账号失败！").WithError(ex.Message);
        }
    }
}