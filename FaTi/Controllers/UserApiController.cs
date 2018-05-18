using System.Threading.Tasks;
using BVirtual.FaTi.Business.Routes;
using Microsoft.AspNetCore.Mvc;

namespace BVirtual.FaTi.Controllers
{
    public class UserApiController : ControllerBase
    {
        private readonly CreateUser _createUser;

        public UserApiController(CreateUser createUser)
        {
            _createUser = createUser;
        }

        [HttpPost]
        [Route("/internal/v1/validation/requests")]
        public async Task<IActionResult> CreateUser([FromBody]Models.UserModel user)
        {
            if (ModelState.IsValid)
            {
                return await _createUser.Execute();
            }

            return BadRequest(new { Code = 901, Message = "Invalid validation" });
        }

    }
}
