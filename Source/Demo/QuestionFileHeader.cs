using System.IO;
using System.Text;

namespace Demo
{
    public class QuestionFileHeader : IFileHeader
    {
        public byte Version { get; set; }

        public string Author { get; set; }

        public string[] Categories { get; set; }

        public string AbsolutePath { get; set; }

        public byte[] GetBytes()
        {
            byte[] output;
            using (var ms = new MemoryStream())
            {
                ms.Write(new byte[] { Version }, 0, 1);
                var buffer = Encoding.UTF8.GetBytes(Author);
                ms.Write(buffer, 0, buffer.Length);
                ms.Write(new byte[] { 0xff }, 0, 1);
                foreach (var item in Categories)
                {
                    buffer = Encoding.UTF8.GetBytes(item);
                    ms.Write(buffer, 0, buffer.Length);
                    ms.Write(new byte[] { 0xff }, 0, 1);
                }

                output = ms.ToArray();
            }
            return output;
        }
    }
}
