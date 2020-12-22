using System.Reflection;
using System.Web.Http;

namespace NextChapterWebApi.Controllers
{
    /// <summary>
    /// Products Endpoint.
    /// </summary>
    public class VersionController : ApiController
    {
        /// <summary>
        /// Gets current API Version.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public string GetVersion()
        {            
            Assembly assembly = Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;

            return string.Format("NextChapterWebApi v{0}", version);
        }
    }
}
