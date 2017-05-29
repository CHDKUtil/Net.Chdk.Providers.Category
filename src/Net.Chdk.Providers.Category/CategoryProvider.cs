using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Net.Chdk.Providers.Category
{
    sealed class CategoryProvider : ICategoryProvider
    {
        #region Constants

        private const string DataPath = "Data";
        private const string DataFileName = "categories.json";

        #endregion
        
        #region Constructor

        public CategoryProvider(ILoggerFactory loggerFactory)
        {
            Logger = loggerFactory.CreateLogger<CategoryProvider>();

            data = new Lazy<string[]>(GetData);
        }

        #endregion

        #region Fields

        private ILogger<CategoryProvider> Logger { get; }

        #endregion

        #region ICategoryProvider Members

        public string[] GetCategories()
        {
            return Data;
        }

        #endregion

        #region Serializer

        private static readonly Lazy<JsonSerializer> serializer = new Lazy<JsonSerializer>(GetSerializer);

        private static JsonSerializer Serializer => serializer.Value;

        private static JsonSerializer GetSerializer()
        {
            return JsonSerializer.CreateDefault();
        }

        #endregion

        #region Data

        private readonly Lazy<string[]> data;

        private string[] Data => data.Value;

        private string[] GetData()
        {
            var filePath = Path.Combine(DataPath, DataFileName);
            using (var reader = File.OpenText(filePath))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var data = Serializer.Deserialize<string[]>(jsonReader);
                Logger.LogInformation("Categories: {0}", JsonConvert.SerializeObject(data));
                return data;
            }
        }

        #endregion
    }
}
