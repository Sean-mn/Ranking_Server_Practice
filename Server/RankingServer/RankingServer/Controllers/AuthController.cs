using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RankingServer.DBContexts;
using RankingServer.Managers;
using RankingServer.Models;
using RankingServer.Requests;
using RankingServer.Sessions;

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

        // 회원가입 기능 (api/auth/signup)
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] UserSignUpRequest request)
        {
            _logger.LogInformation($"회원가입 요청: {request.Username}");

            if (await _context.Users.AnyAsync(u => u.Username == request.Username))
            {
                string checkExistingUserName = $"이미 존재하는 유저 이름: {request.Username}";
                
                _logger.LogWarning(checkExistingUserName);
                return Conflict(checkExistingUserName);
            }
            
            var password = BCrypt.Net.BCrypt.HashPassword(request.Password);

            User newUser = new User
            {
                Username = request.Username,
                Password = password
            };
            
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            string completedSignUpTxt = $"{request.Username}, 회원가입 완료!";
            
            _logger.LogInformation(completedSignUpTxt);
            return Ok(completedSignUpTxt);
        }

        // 로그인 기능 (api/auth/login)
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            _logger.LogInformation($"로그인 요청: {request.Username}");

            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
                if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                {
                    _logger.LogWarning($"알맞지 않은 유저 정보: {request.Username}");
                    return Unauthorized("알맞지 않은 유저 정보");
                }

                _logger.LogInformation($"유저 {request.Username} 로그인 성공!");

                var loginSession = new LoginSession
                {
                    UserId = user.Id,
                    Username = user.Username,
                };
                
                SessionManager.SetLoginSession(HttpContext.Session, loginSession);

                return Ok(new { username = user.Username, id = user.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("서버 오류...");
            }
        }
    }
}
