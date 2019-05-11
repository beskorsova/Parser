using System.Collections.Generic;

namespace Parser.BLL.Services.Interfaces
{
    public interface ILogService
    {
        IEnumerable<string> ReadLog(string filePath);
    }
}
