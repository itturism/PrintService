using FastReport.Export.Pdf;
using FastReport.Models;
using FastReport.Web;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace FastReport.Controllers
{
    public class PrintController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Post([FromBody] PrintModel request)
        {
            var report = new WebReport();
            report.Report.Load(new MemoryStream(request.Template));
            foreach (var param in request.Parameters ?? new List<TagParam>())
            {
                report.Report.SetParameterValue(param.Key, param.Value);
            }
            report.Report.Prepare();
            var pdfStream = new MemoryStream();
            report.Report.Export(new PDFExport(), pdfStream);
            pdfStream.Seek(0, SeekOrigin.Begin);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StreamContent(pdfStream);
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = "document";
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return response;
        }
    }
}