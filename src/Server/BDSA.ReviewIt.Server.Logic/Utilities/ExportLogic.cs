using System.IO;
using System.Threading.Tasks;
using BDSA.ReviewIt.Server.Logic.Utilities.Interfaces;

namespace BDSA.ReviewIt.Server.Logic.Utilities
{
    public class ExportLogic : IExportLogic
    {
        public Task<FileStream> ExportStudy(int studyId)
        {
            throw new System.NotImplementedException();
        }
    }
}