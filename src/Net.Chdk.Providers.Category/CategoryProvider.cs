using Microsoft.Extensions.Logging;
using System.IO;

namespace Net.Chdk.Providers.Category
{
    sealed class CategoryProvider : DataProvider<string[]>, ICategoryProvider
    {
        #region Constants

        private const string DataFileName = "categories.json";

        #endregion
        
        #region Constructor

        public CategoryProvider(ILoggerFactory loggerFactory)
            : base(loggerFactory.CreateLogger<CategoryProvider>())
        {
        }

        #endregion

        #region ICategoryProvider Members

        public string[] GetCategories()
        {
            return Data;
        }

        #endregion

        #region Data

        protected override string GetFilePath()
        {
            return Path.Combine(Directories.Data, DataFileName);
        }

        protected override LogLevel LogLevel => LogLevel.Information;

        protected override string Format => "Categories: {0}";

        #endregion
    }
}
