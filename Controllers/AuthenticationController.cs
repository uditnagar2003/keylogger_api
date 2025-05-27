﻿using keylogger_lib.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using serverLibrary.Respositories.contract;

namespace keylogger_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController(IuserAccount accountInterface) : ControllerBase
    {
        [HttpPost("register")]

        public async Task<IActionResult> CreateAsync(Register user)
        {
            if (user == null)
            {
                return BadRequest("Model is Empty");
            }
            var result = await accountInterface.CreateAsync(user);
            return Ok(result);


        }
        [HttpPost("Login")]
        public async Task<IActionResult> SignInAsync(Login user)
        {
            if (user == null)
            {
                return BadRequest("Model is Empty");
            }
            var result = await accountInterface.SignInAsync(user);
            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshTokenAsync(RefreshToken token)
        {
            if (token == null) return BadRequest("Model is Empty");
            var result = await accountInterface.RefreshTokenAsync(token);
            return Ok(result);
        }
        [HttpGet("users")]
        public async Task<IActionResult> GetUsersAsync()
        {
            var users = await accountInterface.GetUsers();
            if (users == null) return NotFound();
            return Ok(users);
        }
       /* [HttpPut("Update-user")]
        public async Task<IActionResult> UpdateUser(ManageUsers manageUsers)
        {
            var result = await accountInterface.UpdateUser(manageUsers);
            return Ok(result);
        }
       /* [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            var users = await accountInterface.GetRoles();
            if (users == null) return NotFound();
            return Ok(users);
        }*/
        [HttpDelete("delete-user/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await accountInterface.DeleteUser(id);
            return Ok(result);
        }
    }
}
