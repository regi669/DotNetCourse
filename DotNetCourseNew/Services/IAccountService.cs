using DotNetCourseNew.Models;

namespace DotNetCourseNew.Services;

public interface IAccountService
{
    public void Register (RegisterUserDTO dto);
    public string Login (LoginUserDto dto);
}