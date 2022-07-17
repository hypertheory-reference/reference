using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using UsersApi.Adapters;
using UsersApi.Models;

namespace UsersApi.Controllers;

[Route("users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly AuthApiAdapter _adapter;

    public UsersController(AuthApiAdapter adapter)
    {
        _adapter = adapter;
    }

    [HttpPost]
    public async Task<ActionResult> CreateUser([FromBody] NewUserCreateRequest request)
    {

        var response = await _adapter.CreateUserAsync(request);
   
        /*
         * Todo - 
         *  Send create request to management API, create the user.
         *  Save the stuff we need here (everything but the CC?) in a MongoDB Database.0
         *  Create a keyed document message with the user information and publish it.
         *  // Future: Handle the CC Stuff.
         */
        return StatusCode(201, response);
    }
}
