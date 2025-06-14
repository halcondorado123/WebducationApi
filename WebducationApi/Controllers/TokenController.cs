using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebducationApi.Domain.Entities.Token;
using WebducationApi.Infraestructure.Data;
using WebducationApi.Modules;

namespace WebducationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : Controller
    {
        private readonly JWTExtension _jwtservice;
        private readonly ApiDbContext _dbContext;

        public TokenController(JWTExtension jwtservice, ApiDbContext dbContext)
        {
            _jwtservice = jwtservice;
            _dbContext = dbContext;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<LoginResponseModel>> Login(LoginRequest request)
        {
            var result = await _jwtservice.Authenticate(request);

            if (result == null)
            {
                return Unauthorized();
            }
           
            return Ok(result);
        }
    }
}