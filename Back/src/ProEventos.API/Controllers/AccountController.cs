using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Api.Helpers;
using ProEventos.API.Extensions;
using ProEventos.Application.Contratos;
using ProEventos.Application.Dtos;

namespace ProEventos.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;
        private readonly IUtil _util;
        private readonly string _destino = "Perfil";


        public AccountController(IAccountService accountService,
                                 ITokenService tokenService,
                                 IUtil util)
        {
            _tokenService = tokenService;
            _util = util;
            _accountService = accountService;
        }

        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var userName = User.GetUserName();
                var user = await _accountService.GetUserByUserNameAsync(userName);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar recuperar usuário. Erro: {ex.Message}");
            }
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            try
            {
                if (await _accountService.UserExists(userDto.UserName))
                    return BadRequest("Usuário já existe.");

                var user = await _accountService.CreateAccountAsync(userDto);

                if (user != null)
                    return Ok(new
                    {
                        userName = user.UserName,
                        primeiroNome = user.PrimeiroNome,
                        token = _tokenService.CreateToken(user).Result
                    });

                return BadRequest("Usuário não foi criado.");

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar registrar usuário. Erro: {ex.Message}");
            }
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto userLogin)
        {
            try
            {
                var user = await _accountService.GetUserByUserNameAsync(userLogin.UserName);
                if (user == null) return Unauthorized("Usuário ou senha inválidos.");

                var result = await _accountService.CheckUserPasswordAsync(user, userLogin.Password);
                if (!result.Succeeded) return Unauthorized();

                return Ok(new
                {
                    userName = user.UserName,
                    primeiroNome = user.PrimeiroNome,
                    token = _tokenService.CreateToken(user).Result
                });
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar realizar login. Erro: {ex.Message}");
            }
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UserUpdateDto userUpdateDto)
        {
            try
            {
                if (userUpdateDto.UserName != User.GetUserName())
                {
                    return Unauthorized("Deve ser o usuário logado para atualizar.");
                }
                var user = await _accountService.GetUserByUserNameAsync(User.GetUserName());
                if (user == null) return Unauthorized("Deve ser o usuário logado para atualizar.");

                var userReturn = await _accountService.UpdateAccount(userUpdateDto);
                if (userReturn == null) return NoContent();

                return Ok(new
                {
                    userName = user.UserName,
                    primeiroNome = user.PrimeiroNome,
                    token = _tokenService.CreateToken(user).Result
                });

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar atualizar usuário. Erro: {ex.Message}");
            }
        }
        
        [HttpPost("upload-image")]

        public async Task<IActionResult> UploadImage()
        {
            try
            {
                var user = await _accountService.GetUserByUserNameAsync(User.GetUserName());
                if (user == null) return NoContent();

                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    _util.DeleteImage(user.ImagemURL, _destino);
                    user.ImagemURL = await _util.SaveImage(file, _destino);
                }
                var userRetorno = await _accountService.UpdateAccount(user);

                return Ok(userRetorno);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar realizar upload da foto do usuário. Erro: {ex.Message}");
            }
        }

    }
}