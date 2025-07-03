using backend.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class MailingController : ControllerBase
{
    private readonly GmailEmailService _emailService;

    public MailingController(GmailEmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendMail([FromForm] string toEmail, [FromForm] string subject, [FromForm] string body)
    {
        await _emailService.SendEmailAsync(toEmail, subject, body);
        return Ok("Email Sent Successfully");
    }
}
