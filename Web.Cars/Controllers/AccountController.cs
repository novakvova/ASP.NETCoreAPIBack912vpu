using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Web.Cars.Abstract;
using Web.Cars.Data.Identity;
using Web.Cars.Exceptions;
using Web.Cars.Models;
using Web.Cars.Services;

namespace Web.Cars.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm]RegisterViewModel model)
        {
            try
            {
                string token = await _userService.CreateUser(model);
                return Ok(new
                {
                    token
                });
            }
            catch (AccountException aex)
            {
                return BadRequest(aex.AccountError);
            }
            catch(Exception ex)
            {
                return BadRequest(new AccountError("Щось пішло не так! "+ ex.Message));
            }
            
        }
    }
}
