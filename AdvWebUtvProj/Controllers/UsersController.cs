using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvWebUtvProj.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdvWebUtvProj.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ApplicationDbContext context;

        public UsersController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.context = context;

        }

        [HttpGet, Route("admintest")]
        [Authorize(Roles = "Admin")]
        public IActionResult IsAdmin()
        {
            return Ok("You are admin");
        }

        [HttpGet, Route("TempAdminAdd")]
        public async Task<IActionResult> AddAdmin()
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            var user = new User()
            {
                Email = "admin@gmail.com",
                UserName = "admin@gmail.com"
            };
            var result = await userManager.CreateAsync(user);
            if (!result.Succeeded) return BadRequest("Did not add user");

            var roleResult = await userManager.AddToRoleAsync(user, "Admin");
            if (!roleResult.Succeeded) return BadRequest("Did not add to role");
            return Ok($"Admin with email {user.Email} created");
        }

        //[HttpGet, Route("getall")]
        //public async Task<IActionResult> GetAll()
        //{
        //    //var listOfUsers = applicationDbContext.AllUsers();
        //    //foreach (var item in listOfUsers)
        //    //{
        //    //    var user = await userManager.FindByEmailAsync(item.Email);
        //    //    item.Role = await userManager.GetRolesAsync(user);

        //    //}

        //    //return Ok(listOfUsers);
        //}

        [AllowAnonymous]
        [HttpPost, Route("signin")]
        public async Task<IActionResult> SignIn(string email)
        {
            if (User.Identity.IsAuthenticated)
                return BadRequest("already logged in");

            try
            {
                var user = await userManager.FindByEmailAsync(email);


                await signInManager.SignInAsync(user, true);
            }
            catch (Exception)
            {

                return BadRequest($"User with email {email} does not exist");
            }



            return Ok($"User with email {email} is signed in");
        }

        [Authorize]
        [HttpGet, Route("signout")]
        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return Ok("Signed out");
        }
    }
}
