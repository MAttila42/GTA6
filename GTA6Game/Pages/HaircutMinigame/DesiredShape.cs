using GTA6Game.Languages;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTA6Game.Pages.HaircutMinigame
{
    public class DesiredShape
    {
        public static Dictionary<string, DesiredShape> Shapes { get; private set; }

        public static void LoadShapes()
        {
            Shapes = new Dictionary<string, DesiredShape>();

            var fileNames = App.ResourceAssembly.GetManifestResourceNames()
                                                .Select(fn => fn.Split('.'))
                                                .Where(fn => fn.Length >= 6 && fn[3] == "DesiredShapes")
                                                .GroupBy(fn => fn[4])
                                                .Select(group =>
                                                {
                                                    IEnumerable<string> filesInsideGroup = group.Select(fileName => string.Join(".", fileName));
                                                    return new KeyValuePair<string, IEnumerable<string>>(group.Key, filesInsideGroup);
                                                });

            foreach (var shapeData in fileNames)
            {

                string id = shapeData.Key;

                string metaFileName = shapeData.Value.Where(fn => fn.Split('.')[5] == "Meta").FirstOrDefault();
                string metaContent;
                using (Stream metaStream = App.ResourceAssembly.GetManifestResourceStream(metaFileName))
                {
                    using (StreamReader reader = new StreamReader(metaStream, Encoding.UTF8))
                    {
                        metaContent = reader.ReadToEnd();
                    }
                }
                string[] metaLines = metaContent.Split('\n');
                Dictionary<string, string> name = new Dictionary<string, string>
                {
                    { LanguageManager.AvailableCultures[0].IetfLanguageTag, metaLines[0] },
                    { LanguageManager.AvailableCultures[1].IetfLanguageTag, metaLines[1] }
                };

                string topFileName = shapeData.Value.Where(fn => fn.Split('.')[5] == "Top").FirstOrDefault();
                Bitmap top = new Bitmap(App.ResourceAssembly.GetManifestResourceStream(topFileName));

                string frontFileName = shapeData.Value.Where(fn => fn.Split('.')[5] == "Front").FirstOrDefault();
                Bitmap front = new Bitmap(App.ResourceAssembly.GetManifestResourceStream(frontFileName));

                string leftFileName = shapeData.Value.Where(fn => fn.Split('.')[5] == "Left").FirstOrDefault();
                Bitmap left = new Bitmap(App.ResourceAssembly.GetManifestResourceStream(leftFileName));

                string rearFileName = shapeData.Value.Where(fn => fn.Split('.')[5] == "Rear").FirstOrDefault();
                Bitmap rear = new Bitmap(App.ResourceAssembly.GetManifestResourceStream(rearFileName));

                string rightFileName = shapeData.Value.Where(fn => fn.Split('.')[5] == "Right").FirstOrDefault();
                Bitmap right = new Bitmap(App.ResourceAssembly.GetManifestResourceStream(rightFileName));

                Shapes.Add(id, new DesiredShape(top, front, left, rear, right, name));
            }
        }

        public Dictionary<string, string> LocalizedNames { get; }

        public string Name => LocalizedNames[LanguageManager.PreferredLanguage];

        public Bitmap Top { get; }

        public Bitmap Front { get; }

        public Bitmap Left { get; }

        public Bitmap Rear { get; }

        public Bitmap Right { get; }

        public DesiredShape(Bitmap top, Bitmap front, Bitmap left, Bitmap rear, Bitmap right, Dictionary<string, string> localizedNames)
        {
            Top = top;
            Front = front;
            Left = left;
            Rear = rear;
            Right = right;
            LocalizedNames = localizedNames;
        }
    }
}
