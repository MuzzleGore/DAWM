using Core.Dtos;
using Core.Services;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserService userService { get;}

        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        public IActionResult Register(RegisterDto payload)
        {
            userService.Register(payload);
            
            return Ok();
        }
        [HttpGet("test-auth")]
        public IActionResult TestLogin()
        {

            ClaimsPrincipal user = User;

            var result = "";

            foreach (var claim in user.Claims)
            {
                result += claim.Type + " : " + claim.Value + "\n";
            }



            var hasRole_user = user.IsInRole("User");
            var hasRole_teacher = user.IsInRole("Teacher");

            return Ok(result);
        }


        [HttpPost("login")]
        public IActionResult Login(LoginDto payload)
        {
            var jwtToken = userService.Validate(payload);
            return Ok( new {token = jwtToken});
        }

        [HttpGet("StudentGrades")]
        [Authorize(Roles = "Student")]
        public ActionResult<List<Grade>> GetStudentGrades()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var user = userService.GetById(userId);

            var grades = userService.GetGrades(user);

            return Ok(grades);

        }

        [HttpGet("AllGrades")]
        [Authorize(Roles = "Professor")]
        public ActionResult<List<Grade>> GetAllGrades()
        {
            var grades = userService.GetAllGrades();
            return Ok(grades);
        }





    }
}
