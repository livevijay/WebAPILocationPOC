using LocationAPI.Model;
using Microsoft.Extensions.FileProviders;

namespace LocationAPI.Service.Interface
{
    public interface IData
    {
        /// <summary>
        /// File Import
        /// </summary>
        /// <param name="files">List of Form File</param>
        /// <returns>Result Data Transfer Object</returns>
        Task<ResultDTO<object>> Import(List<IFormFile> files);
        /// <summary>
        /// Get data file path
        /// </summary>
        /// <returns>String path</returns>
        string GetFilePath();
        /// <summary>
        /// Get list location Data from device
        /// </summary>
        /// <param name="fromTime">Optional from time (will be greater than or equal to opening)</param>
        /// <param name="toTime">Optinal to time (will be less than or equal to closing)</param>
        /// <returns>Result DTO with payload</returns>
        Task<ResultDTO<IList<LocationDTO>?>> List(TimeSpan? fromTime = null, TimeSpan? toTime = null);
        /// <summary>
        /// Save single location object
        /// </summary>
        /// <param name="location">Location Data Transfer Object</param>
        /// <returns>Result DTO</returns>
        Task<ResultDTO<object>> Save(LocationDTO location);
        /// <summary>
        /// Save Multi location object
        /// </summary>
        /// <param name="locations"> List of location dto</param>
        /// <returns>result DTO</returns>
        Task<ResultDTO<object>> Save(IList<LocationDTO> location);
        /// <summary>
        /// Delete existing record based on location name, business name and type
        /// </summary>
        /// <param name="location">Location DTO</param>
        /// <returns>Result DTO</returns>
        Task<ResultDTO<object>> Delete(LocationDTO location);
    }
}
