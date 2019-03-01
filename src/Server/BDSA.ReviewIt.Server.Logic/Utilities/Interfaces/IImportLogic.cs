using System.IO;
using System.Threading.Tasks;

namespace BDSA.ReviewIt.Server.Logic.Utilities.Interfaces
{
    public interface IImportLogic
    {
        Task<bool> ImportToStudy(int studyId, FileStream file);
    }
}