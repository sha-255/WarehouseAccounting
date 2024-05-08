using System.IO;

namespace WarehouseAccounting.Common
{
    public static class Settings
    {
        public static string SqlConnection { get; private set; }
        private static string Path = Environment.CurrentDirectory;
        private static string SubPath = @"appSettings.cfg";
        private static string FullPath = Path + @$"\{SubPath}";
        private static bool Exists = File.Exists(FullPath);

        public static void Apply()
        {
            if (!Exists)
                File.Create(SubPath);
            SqlConnection = new StreamReader(FullPath).ReadToEnd();
        }
    }
}
