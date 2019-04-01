using System;
using System.IO;
using System.Linq;
using System.Windows.Media;
using System.Xml.Linq;
using SharpVectors.Converters;
using SharpVectors.Renderers.Wpf;

namespace WindowChromes
{
    public class SvgConvertor
    {
        public static DrawingGroup SvgFileToWpfObject(string filepath)
        {
            var wpfDrawingSettings = new WpfDrawingSettings { IncludeRuntime = false, TextAsGeometry = false, OptimizePath = true };
            var reader = new FileSvgReader(wpfDrawingSettings);

            //workaround: error when Id starts with a number
            var doc = XDocument.Load(filepath);
            FixIds(doc.Root); //id="3d-view-icon" -> id="_3d-view-icon"
            using (var ms = new MemoryStream())
            {
                doc.Save(ms);
                ms.Position = 0;
                reader.Read(ms);
                return reader.Drawing;
            }
        }

        private static void FixIds(XElement root)
        {
            var idAttributesStartingWithDigit = root.DescendantsAndSelf()
                .SelectMany(d => d.Attributes())
                .Where(a => string.Equals(a.Name.LocalName, "Id", StringComparison.InvariantCultureIgnoreCase));
            foreach (var attr in idAttributesStartingWithDigit)
            {
                if (char.IsDigit(attr.Value.FirstOrDefault()))
                {
                    attr.Value = "_" + attr.Value;
                }

                attr.Value = attr.Value.Replace("/", "_");
            }
        }
    }
}
