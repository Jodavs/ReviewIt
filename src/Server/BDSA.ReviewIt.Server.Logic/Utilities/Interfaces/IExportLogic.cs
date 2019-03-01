using System.IO;
using System.Threading.Tasks;

namespace BDSA.ReviewIt.Server.Logic.Utilities.Interfaces
{
    public interface IExportLogic
    {
        Task<FileStream> ExportStudy(int studyId);
    }
}