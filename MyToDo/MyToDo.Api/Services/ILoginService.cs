using MyToDo.Shared.Dtos;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyToDo.Api.Services;

public interface ILoginService
{
    Task<Result<UserDto>> LoginAsync(string Account, string Password);

    Task<Result> Register(UserDto user);
}