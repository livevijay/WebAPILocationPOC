using LocationAPI.Model;
using Microsoft.Extensions.FileProviders;

namespace LocationAPI.Service.Interface
{
    public interface IData
    {
        Task<ResultDTO<object>> Import(List<IFormFile> files);
        string GetFilePath();
        Task<ResultDTO<IList<LocationDTO>?>> List(TimeSpan? fromTime = null, TimeSpan? toTime = null);
        Task<ResultDTO<object>> Save(LocationDTO location);
        Task<ResultDTO<object>> Save(List<LocationDTO> location);

    }
}
