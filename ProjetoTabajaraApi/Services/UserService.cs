using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ProjetoTabajaraApi.Data.Dtos.User;
using ProjetoTabajaraApi.Models;

namespace ProjetoTabajaraApi.Services;

public class UserService
{
    private IMapper _mapper;
    private UserManager<User> _userManager;
    private SignInManager<User> _signInManager;
    private TokenService _tokenService;

    public UserService(UserManager<User> userManager, IMapper mapper, SignInManager<User> signInManager, TokenService tokenService)
    {
        _userManager = userManager;
        _mapper = mapper;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task CreateUser(CreateUserDto userDto)
    {
        User user = _mapper.Map<User>(userDto);

        IdentityResult result = await _userManager.CreateAsync(user, userDto.Password);

        if (!result.Succeeded)
        {
            throw new ApplicationException("Falha ao cadastrar o usuário.");
        }
    }

    public async Task<string> Login(LoginUserDto loginDto)
    {
        var result = await _signInManager.PasswordSignInAsync(loginDto.UserName, loginDto.Password, false, false);

        if (!result.Succeeded)
        {
            throw new ApplicationException("Usuário não autenticado");
        }

        var user = _signInManager
            .UserManager
            .Users
            .FirstOrDefault
            (user => 
                user.NormalizedUserName ==  loginDto.UserName.ToUpper()
            );

        if (user == null) return "";

        var token = _tokenService.GenerateToken(user);

        return token;
    }
}
