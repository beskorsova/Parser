using System.Collections.Generic;

namespace Parser.BLL.Services.Interfaces
{
    public interface ILogService
    {
        string[] ReadLog(string filePath);
    }
}
