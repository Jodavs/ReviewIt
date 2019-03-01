using System.IO;
using System.Threading.Tasks;
using BDSA.ReviewIt.Server.Logic.Utilities.Interfaces;

namespace BDSA.ReviewIt.Server.Logic.Utilities
{
    public class ImportLogic : IImportLogic
    {
        public Task<bool> ImportToStudy(int studyId, FileStream file)
        {
            throw new System.NotImplementedException();
        }
    }
}