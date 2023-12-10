using DBI.Infrastructure.Services;
using System.Text.RegularExpressions;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System.Reflection;
using Python.Runtime;
using IronPython.Runtime;
using System.Text.RegularExpressions;

namespace DBI.Application.Services
{
    public class AiModelService : IAiModelService
    {
        public async Task<int> IdentifyAsync(string base64)
        {
            Runtime.PythonDLL = "C:\\Python311\\python311.dll";
            if (!PythonEngine.IsInitialized)
                PythonEngine.Initialize();

            var filePath = @"E:\Codes\DogBreedIdentification\Server\DBI.Application\AI\script";

            using (Py.GIL())
            {
                dynamic os = Py.Import("os");
                dynamic sys = Py.Import("sys");
                sys.path.append(os.path.dirname(os.path.expanduser(filePath)));
                var fromFile = Py.Import(Path.GetFileNameWithoutExtension(filePath));
                var result = fromFile.InvokeMethod("main", Py.kw("base64_string", base64)).ToString();
                string numericString = Regex.Replace(result, @"\D", "");
                return Int32.Parse(numericString.ToString());
            }
        }
    }
}
