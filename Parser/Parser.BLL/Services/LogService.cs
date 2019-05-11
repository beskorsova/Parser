using Parser.BLL.Services.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace Parser.BLL.Services
{
    public class LogService: ILogService
    {
        public IEnumerable<string> ReadLog(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    yield return reader.ReadLine();
                }
            }
        }
    }
}
