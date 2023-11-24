using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Showcase.Models;
using Showcase.Services;

namespace Showcase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMailService mailService;
        public MailController(IMailService mailService)
        {
            this.mailService = mailService;
        }
        [HttpPost("send")]
        public async Task<IActionResult> SendMail([FromForm] MailRequest request)
        {
            // Controleer de voorwaarden
            if (string.IsNullOrEmpty(request.Subject) || request.Subject.Length > 200)
            {
                return BadRequest("Onderwerp mag maximaal 200 tekens zijn.");
            }

            if (string.IsNullOrEmpty(request.Body) || request.Body.Length > 600)
            {
                return BadRequest("Bericht mag maximaal 600 tekens zijn.");
            }
            
            try
            {
                await mailService.SendEmailAsync(request);
                return Ok(new { success = true, message = "E-mail is verzonden" });
            }
            catch (Exception ex)
            {
                return BadRequest("E-mail is niet verzonden");
            }

        }
    }
}
