using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace InfraService.SQLServer
{
    public sealed class FileResource
    {
        public static async Task<string> GetFileResourceAsync(string resourceAssemblyBase, string resourceName, ScriptType scriptType)
        {
            var result = string.Empty;

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var fileName = GetAssemblyNameFull(resourceAssemblyBase, resourceName, scriptType);
            var resourceStream = assembly.GetManifestResourceStream(fileName);

            using (var reader = new StreamReader(resourceStream, Encoding.UTF8))
            {
                result = await reader.ReadToEndAsync(); 
            }

            return result;
        }

        private static string GetAssemblyNameFull(string resourceAssemblyBase, string resource, ScriptType scriptType)
        {
            var script = new Dictionary<ScriptType, string>
            {
                { ScriptType.Insert, $"{resourceAssemblyBase}Scripts.Insert.{resource}.sql" },
                { ScriptType.Delete, $"{resourceAssemblyBase}Scripts.Delete.{resource}.sql" },
                { ScriptType.Update, $"{resourceAssemblyBase}Scripts.Update.{resource}.sql" },
                { ScriptType.Select, $"{resourceAssemblyBase}Scripts.Select.{resource}.sql" }
            };

            return script[scriptType];
        }
    }
}
