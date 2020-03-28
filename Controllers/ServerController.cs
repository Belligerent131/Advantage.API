using Advantage.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Advantage.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ServerController : Controller
  {
    private readonly ApiContext _ctx;

    public ServerController(ApiContext ctx)
    {
      _ctx = ctx;
    }

    [HttpGet]
    public IActionResult Get()
    {
      var response = _ctx.Servers.OrderBy(s => s.Id).ToList();
      return Ok(response);
    }

    [HttpGet("{id}", Name = "GetServer")]
    public Server Get(int id)
    {
      return _ctx.Servers.Find(id);
    }

    [HttpPut("{id}")]
    public IActionResult Message(int id, [FromBody] ServerMessage msg)
    {

      var server = _ctx.Servers.FirstOrDefault(s => s.Id == id);

      if (server == null)
      {
        return NotFound();
      }

      // move update handling to a service, perhaps
      if (msg.Payload == "activate")
      {
        server.IsOnline = true;
        _ctx.SaveChanges();
      }

      if (msg.Payload == "deactivate")
      {
        server.IsOnline = false;
        _ctx.SaveChanges();
      }

      return new NoContentResult();
    }
  }
}
