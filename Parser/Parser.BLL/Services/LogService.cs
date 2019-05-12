using Parser.BLL.Services.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace Parser.BLL.Services
{
    public class LogService: ILogService
    {
        public string[] ReadLog(string filePath)
        {
            return File.ReadAllLines(filePath);
        }
    }
}
