using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.MVVM.Services
{
    public interface ILoginApi
    {
        [Post("/login")]
        Task<IApiResponse> Login(UserDto user);

        [Post("/register")]
        Task<IApiResponse> Register(UserDto user);
    }
}