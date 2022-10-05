using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace startdemos_plus.Utils
{
    public static class Utils
    {
        public static string GetByteArray<T>(T ptr) where T : struct
        {
            byte[] bytes = (byte[])typeof(BitConverter).GetMethod("GetBytes", new[] { typeof(T) })
                .Invoke(null, new[] { (object)ptr });

            string d = "";
            foreach (byte b in bytes)
                d += $"{b:X02} ";

            return d;
        }

        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> property)
        {
            return items.GroupBy(property).Select(x => x.First());
        }

        public static void ThreadAction(this Control control, Action action)
        {
            try
            {
                if (control.InvokeRequired)
                    control.Invoke(action);
                else
                    action();
            }
            catch (InvalidAsynchronousStateException) {; }
        }

        public static T GetValueFromDescription<T>(string description) where T : Enum
        {
            foreach (var field in typeof(T).GetFields())
            {
                if (Attribute.GetCustomAttribute(field,
                typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }

            return default(T);
        }

        public static string GetDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }

        public static string PadNumbers(this string input)
        {
            return Regex.Replace(input, "[0-9]+", match => match.Value.PadLeft(10, '0'));
        }

        public enum MessageType
        {
            Message,
            Warning,
            Error
        }
        public static void Message(string message, MessageType type)
        {
            switch (type)
            {
                case MessageType.Message:
                    MessageBox.Show(message, "startdemos+ | Message");
                    break;
                case MessageType.Warning:
                    MessageBox.Show(null, message, "startdemos+ | Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case MessageType.Error:
                    MessageBox.Show(null, message, "startdemos+ | Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }
        public static void Warning(Exception e, string msg = "startdemos+ has encountered an error")
        {
            Message($"{msg}:\n\nStacktrace:\n{e}", MessageType.Error);
        }

        static ColorMatrix grayMatrix = new ColorMatrix(new float[][]
        {
            new float[] { .2126f, .2126f, .2126f, 0, 0 },
            new float[] { .7152f, .7152f, .7152f, 0, 0 },
            new float[] { .0722f, .0722f, .0722f, 0, 0 },
            new float[] { 0, 0, 0, .5f, 0 },
            new float[] { 0, 0, 0, 0, .5f }
        });

        public static Bitmap ToGrayScale(this Image source)
        {
            var grayImage = new Bitmap(source.Width, source.Height, source.PixelFormat);
            grayImage.SetResolution(source.HorizontalResolution, source.VerticalResolution);

            using (var g = Graphics.FromImage(grayImage))
            using (var attributes = new ImageAttributes())
            {
                attributes.SetColorMatrix(grayMatrix);
                g.DrawImage(source, new Rectangle(0, 0, source.Width, source.Height),
                            0, 0, source.Width, source.Height, GraphicsUnit.Pixel, attributes);
                return grayImage;
            }
        }

        public static void SetProgressNoAnimation(this ProgressBar pb, int value)
        {
            // To get around the progressive animation, we need to move the 
            // progress bar backwards.
            if (value == pb.Maximum)
            {
                // Special case as value can't be set greater than Maximum.
                pb.Maximum = value + 1;     // Temporarily Increase Maximum
                pb.Value = value + 1;       // Move past
                pb.Maximum = value;         // Reset maximum
            }
            else
            {
                pb.Value = value + 1;       // Move past
            }
            pb.Value = value;               // Move to correct value
        }

        public static void Swap<T>(this List<T> list, int src, int dest)
        {
            if (src == dest)
                return;

            T old = list[src];
            list[src] = list[dest];
            list[dest] = old;
        }

        public static void RemapElements<T>(this List<T> list, List<int> newind)
        {
            if (newind.Max() > list.Count - 1)
            {
                Message("remap array has index too high!", MessageType.Error);
                throw new ArgumentException();
            }
            var pairs = new List<(int from, int to)>();

            List<T> newList = new List<T>(list.Count);
            for (int i = 0; i < list.Count; i++)
                newList.Add(list[newind[i]]);

            list.Clear(); newList.ForEach(x => list.Add(x));
        }

        public static List<int> GenerateUpToN(int n)
        {
            List<int> list = new List<int>();
            for (int i = 0; i <= n; i++)
                list.Add(i);

            return list;
        }

        public static List<int> ShiftElements(int maxIndex, List<int> moved, bool down = true)
        {
            var indicies = GenerateUpToN(maxIndex);

            var blocks = new List<(List<int> indicies, bool moved)>();
            blocks.Add((new List<int>() { indicies[0] }, moved.Contains(indicies[0])));
            for (int i = 1; i < indicies.Count; i++)
            {
                var ind = indicies[i];
                bool con = moved.Contains(indicies[i]);

                if (con)
                {
                    if (!blocks.Last().moved)
                        blocks.Add((new List<int>() { ind }, con));
                    else blocks.Last().indicies.Add(i);
                }
                else blocks.Add((new List<int>() { i }, false));
            }

            int add = down ? +1 : -1;
            for (int i = 0; i < blocks.Count; i++)
            {
                if (i + add < 0 || i + add > blocks.Count - 1)
                    continue;

                if (!blocks[i].moved || (blocks[i + add]).moved)
                    continue;

                blocks[i] = (blocks[i].indicies, false);
                blocks.Swap(i, i + add);
            }

            var e = blocks.SelectMany(x => x.indicies).ToList();
            return e;
        }
    }

    public class CommonEventArgs : EventArgs
    {
        public Dictionary<string, object> Data = new Dictionary<string, object>();

        public object this[string thing]
        {
            get { return Data[thing]; }
        }

        public CommonEventArgs(params object[] pairs)
        {
            if (pairs.Length % 2 != 0)
                throw new ArgumentException("Uneven pairs!");

            for (int i = 0; i < pairs.Length; i += 2)
            {
                if (!(pairs[i] is string))
                    throw new ArgumentException("First argument must be name");

                Data.Add((string)pairs[i], pairs[i + 1]);
            }

        }
    }
}
