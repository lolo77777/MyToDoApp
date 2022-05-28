using MyToDo.Shared.Dtos;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyToDo.Api.Services;

public interface ILoginService
{
    Task<ApiResult<UserDto>> LoginAsync(string Account, string Password);

    Task<ApiResult> Resgiter(UserDto user);
}