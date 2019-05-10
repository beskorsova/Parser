using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.BLL
{
    public interface ILogService
    {
        IEnumerable<string> ReadLog(string filePath);
    }
}
