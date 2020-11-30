using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("v1/[controller]/[action]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        [HttpPost]
        public ActionResult Upload(Payload obj)
        {
            FileManager.Program.UploadImage(obj.base64Image);

            return Ok();
        }

        public class Payload
        {
            public string base64Image { get; set; }
        }
    }
}
