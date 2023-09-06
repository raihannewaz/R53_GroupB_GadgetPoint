using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using R53_GroupB_GadgetPoint.DAL.JWTService;
using R53_GroupB_GadgetPoint.DTOs;
using R53_GroupB_GadgetPoint.Models;
using System.Security.Claims;

namespace R53_GroupB_GadgetPoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userMgr;
        private readonly SignInManager<AppUser> _signinMgr;
        private readonly ITokenService _token;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userMgr, SignInManager<AppUser> signinMgr, ITokenService token, IMapper mapper)
        {
            this._userMgr = userMgr;
            this._signinMgr = signinMgr;
            this._token = token;
            this._mapper = mapper;
        }


        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(a => a.Type == ClaimTypes.Email)?.Value;

            var user = await _userMgr.Users.SingleOrDefaultAsync(a => a.Email == email);

            return new UserDTO()
            {
                Email = user.Email,
                Token = _token.CreateToken(user),
                DisplayName = user.DisplayName,

            };
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _userMgr.FindByEmailAsync(email)!=null;
        }

        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDTO>> GetUserAddress()
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(a => a.Type == ClaimTypes.Email)?.Value;

            var user = await _userMgr.Users.Include(a => a.Address).SingleOrDefaultAsync(a => a.Email == email);

            return _mapper.Map<Address, AddressDTO>(user.Address);
        }


        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDTO>> UpdateUserAddress(AddressDTO addressDTO)
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(a => a.Type == ClaimTypes.Email)?.Value;
            
            var user =  await _userMgr.Users.SingleOrDefaultAsync(a => a.Email == email);

            user.Address = _mapper.Map<AddressDTO, Address>(addressDTO);

            var result = await _userMgr.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Ok(_mapper.Map<Address, AddressDTO>(user.Address));
            }
            return BadRequest("Update not done!");

        }




        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDto)
        {
            var user = await _userMgr.FindByEmailAsync(loginDto.Email);

            if (user == null)
            {
                return Unauthorized("User Not Found");
            }

            var result = await _signinMgr.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
            {
                return Unauthorized("Wrong Password");
            }

            return new UserDTO()
            {
                Email = user.Email,
                Token = _token.CreateToken(user),
                DisplayName = user.DisplayName,
                
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            var user = new AppUser()
            {
                DisplayName = registerDTO.DisplayName,
                Email = registerDTO.Email,
                UserName = registerDTO.Email,
            };

            var result = await _userMgr.CreateAsync(user, registerDTO.Password);

            if (!result.Succeeded)
            {
                return BadRequest();
            }

            return new UserDTO()
            {
                DisplayName= registerDTO.DisplayName,
                Token = _token.CreateToken(user),
                Email = registerDTO.Email,
            };
        }

        
    }
}
