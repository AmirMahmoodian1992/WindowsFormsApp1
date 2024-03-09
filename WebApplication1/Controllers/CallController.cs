using Microsoft.AspNetCore.Mvc;
using WindowsFormsApp1;


[ApiController]
[Route("[controller]")]
public class CallController : ControllerBase
{
    private readonly Form1 _frm;

    public CallController(Form1 frm)
    {
        _frm = frm;
    }

    [HttpGet(Name = "Call")]
    public IActionResult Call([FromQuery] string callId)
    {
        if (string.IsNullOrEmpty(callId))
        {
            return BadRequest("Call ID is required.");
        }

        // Make a call to your web forms application
        //_frm.CreatCallFromApi(callId);

        return Ok(default);
    }
}
