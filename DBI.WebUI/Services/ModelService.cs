using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System.Reflection;
using Python.Runtime;
using IronPython.Runtime;
using System.Text.RegularExpressions;

namespace DBI.WebUI.Services
{
    public class ModelService
    {
        public ModelService()
        {
            Runtime.PythonDLL = "C:\\Python311\\python311.dll";
            PythonEngine.Initialize();
        }
        public int Identify(string base64)
        {
            var filePath = @"E:\Codes\DogBreedIdentification\Server\DBI.WebUI\script";
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
