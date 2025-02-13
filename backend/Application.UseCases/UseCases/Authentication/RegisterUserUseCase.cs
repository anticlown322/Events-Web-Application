using AutoMapper;
using Domain.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Application.Contracts.UseCases.Authentication;
using Shared.DTO.User;

namespace Application.UseCases.UseCases.Authentication;

public class RegisterUserUseCase(
    IMapper mapper, 
    UserManager<User> userManager) : IRegisterUserUseCase
{
    public async Task<IdentityResult> ExecuteAsync(UserForRegistrationDto userForRegistration)
    {
        var user = mapper.Map<User>(userForRegistration);
        var result = await userManager.CreateAsync(user, userForRegistration.Password);
        
        if (result.Succeeded)
            await userManager.AddToRolesAsync(user, userForRegistration.Roles);
        
        return result;
    }
}