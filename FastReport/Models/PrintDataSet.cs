using System.Collections.Generic;
using System.Linq;

namespace PrintService.Models
{
    public class PrintDataSet
    {
        public PrintDataSet(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
        public string RegisterName { get; set; }
        public List<PrintDataTable> Tables { get; set; } = new List<PrintDataTable>();
        public List<List<string>> Rows { get; set; }
        public PrintDataTable this[string key]
        {
            get => GetValue(key);
        }
        private PrintDataTable GetValue(string key)
        {
            return Tables.FirstOrDefault(e => e.Name == key);
        }
    }

    public class PrintDataTable
    {
        public PrintDataTable(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
        public List<string> Columns { get; set; } = new List<string>();
        public List<List<string>> Rows { get; set; } = new List<List<string>>();

    }
}