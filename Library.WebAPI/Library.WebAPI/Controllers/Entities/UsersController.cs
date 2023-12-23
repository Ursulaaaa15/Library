using AutoMapper;
using Library.BL.User.Entites;
using Library.BL.User;
using Library.WebAPI.Controllers.Entities.Books;
using Library.WebAPI.Controllers.Entities.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Library.BL.Auth;
using Library.BL.Auth.Entities;
using System.IdentityModel;
using Microsoft.AspNetCore.Identity;

namespace Library.WebAPI.Controllers.Entities
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IAuthProvider _authProvider;
        private readonly IUsersManager _usersManager;
        private readonly IUsersProvider _usersProvider;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public UsersController(IAuthProvider authProvdider, IUsersManager usersManager, IUsersProvider usersProvider,
            IMapper mapper, ILogger<UsersController> logger)
        {
            _authProvider = authProvdider;
            _usersManager = usersManager;
            _usersProvider = usersProvider;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [Route("login")]
        public async Task<IActionResult> LoginUser([FromQuery] string email, [FromQuery] string password)
        {
            try
            {
                TokensResponse tokens = await _authProvider.AuthorizeUser(email, password);
                return Ok(tokens);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request)
        {
            try
            {
                await _authProvider.RegisterUser(_mapper.Map<RegisterUserModel>(request));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            IEnumerable<UserModel> users = _usersProvider.GetAllUsers();

            return Ok(new UsersListResponse()
            {
                Users = users.ToList()
            });
        }

        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetUser([FromRoute] Guid id)
        {
            try
            {
                UserModel user = _usersProvider.GetUser(id);
                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex.ToString());
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateUserInfo([FromRoute] Guid id, UpdateUserRequest request)
        {
            try
            {
                UserModel user = _usersManager.UpdateUser(id, _mapper.Map<UpdateUserModel>(request));
                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex.ToString());
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteUser([FromRoute] Guid id)
        {
            try
            {
                _usersManager.DeleteUser(id);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex.ToString());
                return NotFound(ex.Message);
            }
        }
    }
}