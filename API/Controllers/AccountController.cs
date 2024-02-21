using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.DTOs;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseAPIController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signinManager;
        private readonly ITokenService _token;
        private readonly IMapper _maper;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signinManager, ITokenService token, IMapper mapper)
        {
            this._signinManager = signinManager;
            this._userManager = userManager;
            this._token = token;
            this._maper = mapper;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var user = await _userManager.FindByEmailFromClaims(User);
            return new UserDTO
            {
                Email = user.Email,
                Token = _token.CreateToken(user),
                DisplayName = user.DisplayName
            };
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }
        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDTO>> GetUserAddress()
        {
            var user = await _userManager.FindUserByClaimsPrincipalWithAddress(User);
            //you need to include the address, userManager does not know what props to include
            return _maper.Map<Address, AddressDTO>(user.Address);
        }
        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDTO>> UpdateUserAddress(AddressDTO addressDTO)
        {
            // if datatype is Task<AppUser> meaning we have not written await there.
            var user = await _userManager.FindUserByClaimsPrincipalWithAddress(User);
            user.Address = _maper.Map<AddressDTO, Address>(addressDTO);
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Ok(_maper.Map<Address, AddressDTO>(user.Address));
            }
            else
            {
                return BadRequest("Problem saving the user");
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            //check email is exist or not
            if (CheckEmailExistsAsync(registerDTO.Email).Result.Value)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = new[] { "Email is in use" } });
            }
            var user = new AppUser
            {
                DisplayName = registerDTO.DisplayName,
                Email = registerDTO.Email,
                UserName = registerDTO.Email
            };

            var result = await _userManager.CreateAsync(user, registerDTO.Password);
            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            return new UserDTO
            {
                Email = user.Email,
                Token = _token.CreateToken(user),
                DisplayName = user.DisplayName
            };
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null)
            {
                // need registeration
                return Unauthorized(new ApiResponse(401));
            }

            var result = await _signinManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);

            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

            return new UserDTO
            {
                Email = user.Email,
                Token = _token.CreateToken(user),
                DisplayName = user.DisplayName
            };
        }

    }
}