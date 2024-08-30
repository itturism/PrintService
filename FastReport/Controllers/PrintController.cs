using FastReport.Export.Pdf;
using FastReport.Models;
using FastReport.Web;
using PrintService.Models;
using System.Collections.Generic;
using System.Data;
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
        public HttpResponseMessage Post([FromBody] PrintModelRequest request)
        {
            using (var report = new WebReport())
            {
                var stream = new MemoryStream();

                report.Report.Load(new MemoryStream(request.DocumentTemplate));
                foreach (var param in request.ReplaceParameters ?? new List<TagParam>())
                {
                    report.Report.SetParameterValue(param.Key, param.Value);
                }
                foreach (var table in request.PrintDataSet ?? new List<PrintDataSet>())
                {
                    report.Report.RegisterData(table.ConvertToDataSet(), table.RegisterName);
                }

                report.Report.Prepare();
                report.Report.Export(new PDFExport(), stream);
                report.Report.Clear();
                stream.Seek(0, SeekOrigin.Begin);

                var response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StreamContent(stream);
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = request.Name;
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                return response;
            }
        }
    }
}