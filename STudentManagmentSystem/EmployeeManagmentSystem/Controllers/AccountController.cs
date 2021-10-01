using DAL.Model.VM;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagmentSystem.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        UserManager<IdentityUser> userManager;
        SignInManager<IdentityUser> signInManager;
        public AccountController(UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }

        [HttpPost("register")]
        public ActionResult Register(RegisterVM rgVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new IdentityUser()
                    {
                        UserName = rgVM.UserName,
                        Email = rgVM.Email
                    };
                    var userResult = userManager.CreateAsync(user, rgVM.Password);
                    if (userResult.Result.Succeeded)
                    {
                        var roleResult = userManager.AddToRoleAsync(user, "user");
                        if (roleResult.Result.Succeeded)
                        {
                            return Ok(user);
                        }
                            
                    }
                    else
                    {
                        foreach (var error in userResult.Result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                return BadRequest(ModelState.Values);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState.Values);
            }
        }

        [HttpPost("SignIn")]
        public ActionResult SignIn(SignInVM sgVM)
        {
            if (ModelState.IsValid)
            {
                var signInResult = signInManager.PasswordSignInAsync(sgVM.username, sgVM.password, false, false);
                if (signInResult.Result.Succeeded)
                {

                    #region JWT
                        var user = userManager.FindByNameAsync(sgVM.username);
                        var roles = userManager.GetRolesAsync(user.Result);

                             IdentityOptions identityOptions = new IdentityOptions();
                             var claims = new Claim[]
                             {
                                 new Claim(identityOptions.ClaimsIdentity.UserIdClaimType,user.Result.Id),
                                 new Claim(identityOptions.ClaimsIdentity.UserNameClaimType,user.Result.UserName),
                                 new Claim(identityOptions.ClaimsIdentity.RoleClaimType,roles.Result[0]),
                             };
                        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("xecretKeywqejane"));
                        var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(signingCredentials: signingCredentials,
                        claims:claims,
                        expires: DateTime.Now.AddMinutes(30));

                        return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                    #endregion


                    // return Ok();
                }
            }
            return BadRequest(ModelState);
        }
        [HttpPost("SignOut")]
        public ActionResult SignOut()
        {
            signInManager.SignOutAsync();
            return NoContent();
        }
    }
}
