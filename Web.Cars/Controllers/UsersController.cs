﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.Cars.Data;
using Web.Cars.Exceptions;
using Web.Cars.Models;

namespace Web.Cars.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppEFContext _context;
        private readonly IMapper _mapper;
        public UsersController(AppEFContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [Route("all")]
        [HttpGet]
        public IActionResult GetUsers()
        {
            Thread.Sleep(2000);
            var list = _context.Users
                .Select(x=>_mapper.Map<UserItemViewModel>(x))
                .ToList();
            return Ok(list);
        }

        [Route("delete/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            Thread.Sleep(2000);
            try
            {
                var user = _context.Users.SingleOrDefault(x=>x.Id==id);
                if(user==null)
                    return NotFound();

                if (user.Photo != null)
                {
                    var directory = Path.Combine(Directory.GetCurrentDirectory(), "images");
                    var FilePath = Path.Combine(directory, user.Photo);
                    System.IO.File.Delete(FilePath);
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { invalid = "Something went wrong on server " + ex.Message });
            }
        }

        [Route("edit/{id}")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Thread.Sleep(2000);
            var user = _context.Users
                .SingleOrDefault(x => x.Id == id);
            return Ok(_mapper.Map<UserEditViewModel>(user));
        }

        [HttpPost("save")]
        public async Task<IActionResult> Save([FromForm] UserSaveViewModel user)
        {
            return BadRequest();
            try
            {
                
            }
            catch (AccountException aex)
            {
                return BadRequest(aex.AccountError);
            }
            catch (Exception ex)
            {
                return BadRequest(new AccountError("Щось пішло не так! " + ex.Message));
            }
        }
    }
}
