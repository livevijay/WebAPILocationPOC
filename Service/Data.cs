using LocationAPI.Common;
using LocationAPI.Model;
using LocationAPI.Service.Interface;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
namespace LocationAPI.Service
{
    public class Data : IData
    {
        private List<LocationDTO> GetFromCSVString(string csvString)
        {
            List<LocationDTO> lstLocation = new List<LocationDTO>();
            string[] rows = csvString.Split("\r\n");
            rows = rows.Skip(1).ToArray();
            if (rows.Length > 0)
            {
                foreach (string row in rows)
                {
                    string[] cols = row.Split(',');
                    if (cols.Length > 0 && cols.Length > 4)
                    {
                        //LocationDTO? location = lstLocation.Where(x => x.Name == cols[0]).FirstOrDefault();
                        BusinessDTO business = new BusinessDTO()
                        {
                            Name = cols[2],
                            Type = cols[1],
                            Opening = Utility.ToTime(cols[3]),
                            Closing = Utility.ToTime(cols[4])
                        };
                        LocationDTO? location = new LocationDTO() { Name = cols[0], Business = business };
                        lstLocation.Add(location);
                    }
                }
            }
            return lstLocation;
        }
        private async Task<bool> SaveData(IList<LocationDTO> locations)
        {
            try
            {
                var path = GetFilePath();
                string strresult = string.Empty;
                using (var writer = new StreamWriter(path, false))
                {
                    await writer.WriteLineAsync(JsonConvert.SerializeObject(locations, Formatting.None));
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private async Task<IList<LocationDTO>?> GetData(TimeSpan? fromTime = null, TimeSpan? toTime = null)
        {
            var path = GetFilePath();
            string strresult = string.Empty;
            using (var reader = new StreamReader(path))
            {
                strresult = await reader.ReadToEndAsync();
            }
            var _locations = JsonConvert.DeserializeObject<IList<LocationDTO>>(strresult);



            if ((fromTime != null || toTime != null) && _locations != null)
            {
                Func<BusinessDTO?, bool> whereBusiness = b => b != null;
                if (fromTime != null && toTime != null) { whereBusiness = b => b != null && b.Opening >= fromTime && b.Closing <= toTime; }
                else if (fromTime != null) { whereBusiness = b => b != null && b.Opening >= fromTime; }
                else if (toTime != null) { whereBusiness = b => b != null && b.Closing <= toTime; }

                _locations = _locations?.Where(x => x.Businesses != null && x.Businesses.Any(whereBusiness)).ToList();
                if (_locations != null)
                {
                    foreach (var loc in _locations)
                    {
                        loc.Businesses = loc?.Businesses?.Where(whereBusiness).ToList();
                    }
                }

            }
            return _locations;
        }


        public string GetFilePath()
        {
            return Path.Combine(Path.GetFullPath("Data"), "data.json");
        }
        public async Task<ResultDTO<IList<LocationDTO>?>> List(TimeSpan? fromTime = null, TimeSpan? toTime = null)
        {
            var result = new ResultDTO<IList<LocationDTO>?>();
            try
            {
                var _data = await GetData(fromTime, toTime);
                result.Success = true;
                result.Payload = _data;
            }
            catch(Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }        

        public async Task<ResultDTO<object>> Import(List<IFormFile> files)
        {
            var result = new ResultDTO<object>();
            if (files != null && files.Count > 0 && Path.GetExtension(files[0].FileName) == ".csv")
            {
                var formFile = files[0];
                List<LocationDTO> lstLocaton;
                if (formFile.Length > 0)
                {
                    //var filePath = Path.Combine(Path.GetFullPath("Data"),"data.csv");
                    using (var stream = formFile.OpenReadStream())
                    using (var reader = new StreamReader(stream))
                    {
                        string fileContents = await reader.ReadToEndAsync();
                        lstLocaton = GetFromCSVString(fileContents);
                    }
                    if (lstLocaton != null)
                    {
                        result = await Save(lstLocaton);
                    }
                }
                if (result.Success)
                {
                    result.Success = true;
                    result.Message = "Successfully saved";
                }
            }
            else
            {
                result.Success = false;
                result.Message = "Invalid File to save / upload";
            }
            return result;
        }
        public async Task<ResultDTO<object>> Save(LocationDTO location)
        {
            return await Save(new List<LocationDTO>() { location });
        }
        public async Task<ResultDTO<object>> Save(List<LocationDTO> locations)
        {
            var result = new ResultDTO<object>();
            try
            {
                IList<LocationDTO>? existingLocations = await GetData();
                if (existingLocations == null) { existingLocations = new List<LocationDTO>(); }
                foreach (var location in locations)
                {
                    var _location = existingLocations.Where(x => x.Name == location.Name).FirstOrDefault();
                    if (_location == null)
                    {
                        _location = new LocationDTO() { Name = location.Name, Businesses = new List<BusinessDTO?>() };
                        existingLocations.Add(_location);
                    }
                    var _business = _location?.Businesses?.Where(x => x.Type == location?.Business?.Type && x.Name == location?.Business?.Name).FirstOrDefault();
                    if (_business == null) { _location?.Businesses?.Add(location.Business); }
                    else
                    {
                        _business.Opening = location.Business?.Opening;
                        _business.Closing = location.Business?.Closing;
                    }
                }
                await SaveData(existingLocations);
                result.Success = true;
                result.Message = "Successfully saved";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                //TODO Log
            }
            return result;
        }
    }
}
