using System;
using api.Dtos.Account;
using api.Models;

namespace api.Mappers
{
  public static class UserMappers
  {
    public static UserDto ToUserDto(this AppUser userModel)
    {
      return new UserDto
      {
        Id = userModel.Id,
        Username = userModel.UserName,
        Email = userModel.Email,
        TodoProfiles = userModel.TodoProfiles,
        ProjectProfiles = userModel.ProjectProfiles
      };
    }
  }
}