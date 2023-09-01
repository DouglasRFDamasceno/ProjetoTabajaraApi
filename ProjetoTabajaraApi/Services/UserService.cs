using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProjetoTabajaraApi.Data;
using ProjetoTabajaraApi.Data.Dtos.User;
using ProjetoTabajaraApi.Models;

namespace ProjetoTabajaraApi.Services;

public class UserService : ControllerBase
{
    private IMapper _mapper;
    private UserManager<User> _userManager;
    private SignInManager<User> _signInManager;
    private TokenService _tokenService;
    private readonly appDbContext _context;

    public UserService(
        UserManager<User> userManager,
        IMapper mapper,
        SignInManager<User> signInManager,
        TokenService tokenService,
        appDbContext context
    )
    {
        _userManager = userManager;
        _mapper = mapper;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _context = context;
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

    public List<ReadUserDto> GetUsers(int skip, int take)
    {
        try
        {
            List<User>? users= _context!.Users?
                .OrderByDescending(user => user.UserName)
                .Skip(skip)
                .Take(take)
                .ToList();

            if (users == null) return new List<ReadUserDto>();

            var usersDto = _mapper.Map<List<ReadUserDto>>(users);

            return usersDto;
        } catch (Exception ex)
        {
            Console.WriteLine(ex);
            return new List<ReadUserDto>();
        }
    }

    public async Task<IActionResult> PatchUser(string id, JsonPatchDocument<UpdateUserDto> patch)
    {
        var user = _context.Users.FirstOrDefault(user => user.Id == id);

        if (user == null) return NotFound();

        var userToUpdate = _mapper.Map<UpdateUserDto>(user);

        patch.ApplyTo(userToUpdate, ModelState);

        if (!TryValidateModel(userToUpdate))
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(userToUpdate, user);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
