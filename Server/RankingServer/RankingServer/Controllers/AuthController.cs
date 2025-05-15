using Microsoft.AspNetCore.Mvc;
using RankingServer.DBContexts;
using RankingServer.Models;
using RankingServer.Requests;

namespace RankingServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserDBContext _context;
        private ILogger<AuthController> _logger;

        public AuthController(UserDBContext context, ILogger<AuthController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost("signup")]
        public IActionResult SignUp([FromBody] SignUpRequest request)
        {
            _logger.LogInformation($"회원가입 요청: {request.Username}");

            if (_context.Users.Any(u => u.Username == request.Username))
            {
                _logger.LogWarning($"이미 존재하는 유저 이름: {request.Username}");
                return Conflict($"이미 존재하는 유저 이음: {request.Username}");
            }
            
            var password = BCrypt.Net.BCrypt.HashPassword(request.Password);

            User newUser = new User
            {
                Username = request.Username,
                Password = password
            };
            
            _context.Users.Add(newUser);
            _context.SaveChanges();

            string completedSignUpTxt = $"{request.Username}, 회원가입 완료!";
            
            _logger.LogInformation(completedSignUpTxt);
            return Ok(completedSignUpTxt);
        }
        
        
    }
}
