using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.AspNetCore.Mvc;
using ProjetoTabajaraApi.Controllers;
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

    [HttpPost("create")]
    [Authorize]
    public async Task<User> CreateUser(CreateUserDto userDto)
    {
        try
        {
            User user = _mapper.Map<User>(userDto);

            IdentityResult result = await _userManager.CreateAsync(user, userDto.Password);

            if (!result.Succeeded)
            {
                throw new ApplicationException("Falha ao cadastrar o usuário.");
            }

            return user;

        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Erro interno do servidor ao cadastrar o usuário. Erro: {ex.Message}");
        }
    }

    [HttpPost("login")]
    public async Task<string> Login(LoginUserDto loginDto)
    {
        try
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
        catch (Exception ex)
        {
            return $"Erro grave ao obter o token. Erro: {ex.Message}";
        }
    }

    [HttpGet("")]
    [Authorize]
    public List<ReadUserDto> GetUsers(int skip, int take)
    {
        try
        {
            List<User>? users = _context!.Users?
                .OrderByDescending(user => user.UserName)
                .Skip(skip)
                .Take(take)
                .ToList();

            if (users == null) return new List<ReadUserDto>();

            var usersDto = _mapper.Map<List<ReadUserDto>>(users);

            return usersDto;
        } catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new List<ReadUserDto>();
        }
    }

    [HttpPatch("{id}")]
    [Authorize]
    public IActionResult PatchUser(string id, JsonPatchDocument<UpdateUserDto> patch)
    {
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest("O ID do usuário é inválido.");
        }

        User? user = _context.Users.FirstOrDefault(user => user.Id == id);

        if (user == null)
        {
            return NotFound("Usuário não encontrado.");
        }

        UpdateUserDto userToUpdate = _mapper.Map<UpdateUserDto>(user);

        patch.ApplyTo(userToUpdate, ModelState);

        if (!TryValidateModel(userToUpdate))
        {
            return ValidationProblem(ModelState);
        }

        try
        {
            // Mapeamento reverso após a validação do modelo.
            _mapper.Map(userToUpdate, user);
            _context.SaveChanges();
            return NoContent(); // Retorna um status 204 (No Content) em caso de sucesso.
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ocorreu um erro ao atualizar o usuário: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public IActionResult DeleteUser(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest("O ID do usuário é inválido.");
        }

        var user = _context.Users.FirstOrDefault(u => u.Id == id);

        if (user == null)
        {
            return NotFound("Usuário não encontrado.");
        }

        // Verificar se o usuário autenticado tem permissão para excluir
        // o usuário em questão aqui, se aplicável.
        try
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
            return NoContent(); // Retorna um status 204 (No Content) em caso de sucesso.
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ocorreu um erro ao excluir o usuário: {ex.Message}");
        }
    }

    public ReadUserDto? GetUser(string id)
    {
        try
        {
            User? user = _context.Users.FirstOrDefault(user => user.Id == id);

            if (user == null) return null;

            var usersDto = _mapper.Map<ReadUserDto>(user);

            return usersDto;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }
}
