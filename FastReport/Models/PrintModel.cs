using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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