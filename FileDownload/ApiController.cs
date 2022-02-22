using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Net.Mime;

namespace FileDownload
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IHostEnvironment hostEnvironment;

        public DocumentController(IHostEnvironment hostEnvironment)
        {
            this.hostEnvironment = hostEnvironment;
        }
        [HttpGet("download")]
        public IActionResult Download()
        {
            var result =  DownloadFile(false);

            return result;
        }

        [HttpGet("view")]
        public IActionResult View()
        {
            var result = DownloadFile(true);
            return result;
        }

        private FileResult DownloadFile(bool inline)
        {
            var content = new FileStream(Path.Combine(this.hostEnvironment.ContentRootPath, "pdf-sample.pdf"), FileMode.Open);


            Response.Headers.ContentDisposition = new ContentDisposition
            {
                FileName = "sample.pdf",
                Inline = inline  // false = prompt the user for downloading;  true = browser to try to show the file inline
            }.ToString();

            var response = File(content, "application/pdf");
            return response;
        }
    }
}
