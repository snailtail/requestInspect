using Microsoft.AspNetCore.Mvc;
using requestInspect.Model;

namespace requestInspect.Controllers;

[ApiController]
[Route("[controller]")]
public class RequestInspectController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<RequestInspectController> _logger;
    private readonly IRequestLoggerService _requestLoggerService;

    public RequestInspectController(ILogger<RequestInspectController> logger, IRequestLoggerService requestLoggerService)
    {
        _logger = logger;
        _requestLoggerService = requestLoggerService;
    }

    [HttpPost("SaveRequest")]
    [HttpGet("SaveRequest")]
    public async Task<RequestEntity> AddRequestEntity()
    {

        RequestEntity ent = new();
        ent.Method = Request.Method;
        foreach (var header in Request.Headers)
        {
            ent.Headers.Add(header.Key, header.Value);
        }

        // Log request body
        using (var reader = new StreamReader(Request.Body, System.Text.Encoding.UTF8))
        {
            var requestBody = await reader.ReadToEndAsync();

            ent.Body = requestBody;
        }
        return await _requestLoggerService.AddRequestEntity(ent);
    }

    [HttpGet("Requests/")]
    public async Task<IEnumerable<RequestEntity>> GetRequests()
    {
        return await _requestLoggerService.GetAllRequestEntities();
    }

    [HttpGet("Requests/{ID}")]
    public async Task<ActionResult<RequestEntity>> GetRequest(string ID)
    {
        var req = await _requestLoggerService.GetRequestEntityByID(ID);
        if (req == null)
        {
            return NotFound();
        }
        return req;
    }

    /// <summary>
    /// Removes a Request entry from the list of entries
    /// </summary>
    /// <param name="ID">The ID (GUID string) for the Request entity to delete</param>
    /// <returns></returns>
    [HttpDelete("Requests/{ID}")]
    public async Task<ActionResult> DeleteRequest(string ID)
    {
        var result = await _requestLoggerService.RemoveRequestEntity(ID);
        if (result == null)
        {
            return NotFound();
        }
        return Ok();
    }
}
