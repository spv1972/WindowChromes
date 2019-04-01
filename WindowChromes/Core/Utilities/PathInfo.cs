using System.IO;
using System.Linq;

namespace WindowChromes
{
    /// <summary>
    /// Extensions Methods for System.IO.Path
    /// </summary>
    internal static class PathInfo
    {
        /// <summary>
        /// Get pzh from pack...components;/../../../
        /// </summary>
        /// <param name="uriPath"></param>
        /// <returns></returns>
        public static string GetComponentPath(string uriPath)
        {
            var path = uriPath.Replace("%20", " ");
            var items = path.Split(';');
            if (items.Length < 2) return path;
            var components = items[1].Split('/').Where(c => c != "component").ToArray();

            return Path.Combine(components);

        }


    }
}
