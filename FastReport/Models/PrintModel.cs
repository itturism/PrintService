using System.Collections.Generic;

namespace FastReport.Models
{
    public class PrintModel
    {
        public List<TagParam> Parameters { get; set; }
        public byte[] Template { get; set; }
    }
    public class TagParam
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}