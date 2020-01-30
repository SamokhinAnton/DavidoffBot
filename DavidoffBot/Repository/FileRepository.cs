using DavidoffBot.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using DavidoffBot.Models;
using System.Linq;
using System.Threading.Tasks;

namespace DavidoffBot.Repository
{
    public class FileRepository : IBaseRepository
    {
        public async Task<string> Get(string keyword)
        {
            var path = string.Format($"{Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName}\\data.txt");
            string[] messages = await File.ReadAllLinesAsync(path, Encoding.UTF8);
            List<DataModel> readText = ConvertTODataModel(messages).ToList();
            var message = readText.FirstOrDefault(rt => rt.keywords.Contains(keyword.ToLowerInvariant()))?.Message;
            return message;
        }

        public IEnumerable<DataModel> ConvertTODataModel(string[] messages)
        {
            foreach (var message in messages)
            {
                yield return JsonConvert.DeserializeObject<DataModel>(message);
            }
        }
    }
}
