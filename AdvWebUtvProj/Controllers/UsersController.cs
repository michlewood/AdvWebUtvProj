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

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var listOfUsers = context.GetAllUsers;
            foreach (var userVM in listOfUsers)
            {
                var user = await userManager.FindByEmailAsync(userVM.Email);
                userVM.Role = await userManager.GetRolesAsync(user);
            }

            return Ok(listOfUsers);
        }

        [AllowAnonymous]
        [HttpGet, Route("signin/{email}")]
        public async Task<IActionResult> SignIn(string email)
        {
            if (User.Identity.IsAuthenticated)
                return BadRequest("already logged in: " + User.Identity.Name);

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

        [HttpGet, Route("TempAdminAdd/{id}")]
        public async Task<IActionResult> AddAdmin(int id)
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

        [Authorize]
        [HttpPost, Route("signout")]
        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return Ok("Signed out");
        }

        [Authorize]
        [HttpGet, Route("signout")]
        public async Task<IActionResult> SignOut2()
        {
            await signInManager.SignOutAsync();
            return Ok("Signed out");
        }

        [HttpGet, Route("seed")]
        public async Task<IActionResult> Seed()
        {
            var users = context.Users;

            foreach (var userToRemove in users)
            {
                context.Remove(userToRemove);
            }
            await context.SaveChangesAsync();

            await roleManager.CreateAsync(new IdentityRole("Admin"));
            await roleManager.CreateAsync(new IdentityRole("Member"));

            var user = new User
            {
                Email = "admin@gmail.com",
                UserName = "admin"
            };

            var userResult = await userManager.CreateAsync(user);
            if (!userResult.Succeeded) return BadRequest("Email is not valid");

            var staffResult = await userManager.AddToRoleAsync(user, "Admin");
            if (!staffResult.Succeeded) return BadRequest("Role does not exist");

            user = new User
            {
                Email = "member@gmail.com",
                UserName = "member"
            };

            userResult = await userManager.CreateAsync(user);
            if (!userResult.Succeeded) return BadRequest("Email is not valid");

            staffResult = await userManager.AddToRoleAsync(user, "Member");
            if (!staffResult.Succeeded) return BadRequest("Role does not exist");

            await context.SaveChangesAsync();

            return Ok("Things seeded");
        }

        [HttpGet, Route("getrole")]
        public async Task<IActionResult> GetCurrentRole()
        {
            if (User.Identity.IsAuthenticated)
            {
                string userId = userManager.GetUserId(HttpContext.User);
                var loggedInUser = await userManager.FindByIdAsync(userId);
                var role = await userManager.GetRolesAsync(loggedInUser);
                var name = await GetCurrentName();
                List<string> roleAndName = new List<string>();
                foreach (var item in role)
                {
                    roleAndName.Add(item);
                }
                roleAndName.Add(name);
                return Ok(roleAndName.ToArray());
            }
            else
            {
                return Ok("Anonymous");
            }
        }

        public async Task<string> GetCurrentName()
        {
            if (User.Identity.IsAuthenticated)
            {
                string userId = userManager.GetUserId(HttpContext.User);
                var loggedInUser = await userManager.FindByIdAsync(userId);
                var username = await userManager.GetUserNameAsync(loggedInUser);
                return username;
            }
            else
            {
                return "Anonymous";
            }
        }

        [AllowAnonymous]
        [HttpPost, Route("adduser/{email}")]
        public async Task<IActionResult> CreateUser(string email)
        {
            if (String.IsNullOrEmpty(email))
            {
                return BadRequest("Emailadress field can not be empty");
            }
            try
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
                await roleManager.CreateAsync(new IdentityRole("Member"));
                var user = new User
                {
                    Email = email,
                    UserName = email.Split('@')[0]

                };

                var userResult = await userManager.CreateAsync(user);
                if (!userResult.Succeeded) return BadRequest("Email is not valid");

                var staffResult = await userManager.AddToRoleAsync(user, "Member");
                if (!staffResult.Succeeded) return BadRequest("Role does not exist");

                return Ok($"User {email} added");

            }
            catch
            {
                return BadRequest($"{email} is not a valid emailadress");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, Route("editRole/{email}")]
        public async Task<IActionResult> EditRole(string email)
        {
            if (String.IsNullOrEmpty(email))
            {
                return BadRequest("Emailadress field can not be empty");
            }

            string roleToLose;
            string roleToGain;

            var user = await userManager.FindByEmailAsync(email);

            if ((await userManager.GetRolesAsync(user))[0] == "Admin")
            {
                roleToGain = "Member";
                roleToLose = "Admin";
            }
            else
            {
                roleToGain = "Admin";
                roleToLose = "Member";
            }
            await userManager.RemoveFromRoleAsync(user, roleToLose);
            await userManager.AddToRoleAsync(user, roleToGain);

            return Ok("Role changed");
        }
    }
}
