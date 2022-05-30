namespace MyToDo.Api.Services;

public class LoginService : ILoginService
{
    private readonly ISugarUnitOfWork<MyDbContext> _sugarUnit;
    private readonly IMapper _mapper;

    public LoginService(ISugarUnitOfWork<MyDbContext> sugarUnit, IMapper mapper)
    {
        _sugarUnit = sugarUnit;
        _mapper = mapper;
    }

    public async Task<Result<UserDto>> LoginAsync(string Account, string Password)
    {
        try
        {
            using var unit = _sugarUnit.CreateContext();
            Password = Password.GetMD5();
            var model = await unit.Users.GetFirstAsync(x => x.Account.Equals(Account) && x.PassWord.Equals(Password));
            if (model == null)
                return Result.Fail<UserDto>("账号或密码错误,请重试！");

            var userDto = new UserDto()
            {
                Account = model.Account,
                UserName = model.UserName,
                Id = model.Id
            };
            unit.Commit();
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
            using var unit = _sugarUnit.CreateContext();

            var model = _mapper.Map<User>(user);
            var userModel = await unit.Users.GetFirstAsync(x => x.Account.Equals(model.Account));
            if (userModel != null)
                return Result.Fail($"当前账号:{model.Account}已存在,请重新注册！");
            model.CreateDate = DateTime.Now;
            model.PassWord = model.PassWord.GetMD5();
            await unit.Users.InsertAsync(model);
            unit.Commit();
            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail("注册账号失败！").WithError(ex.Message);
        }
    }
}