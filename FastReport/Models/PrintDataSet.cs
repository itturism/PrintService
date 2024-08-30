using System.Collections.Generic;
using System.Data;
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
        public DataSet ConvertToDataSet()
        {
            var result = new DataSet(Name);
            foreach (var table in Tables)
            {
                result.Tables.Add(new DataTable(table.Name));
                foreach (var column in table.Columns)
                {
                    result.Tables[table.Name].Columns.Add(column, typeof(string));
                }
                foreach (var row in table.Rows)
                {
                    result.Tables[table.Name].Rows.Add(row.ToArray());
                }
            }
            return result;
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