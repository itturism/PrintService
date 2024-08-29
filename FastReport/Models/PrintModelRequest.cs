using PrintService.Models;
using System.Collections.Generic;

namespace FastReport.Models
{
    public class PrintModelRequest
    {
        public List<TagParam> ReplaceParameters { get; set; }
        public byte[] DocumentTemplate { get; set; }
        public string Name { get; set; }
        public List<PrintDataSet> PrintDataSet { get; set; }
    }
    public class TagParam
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}