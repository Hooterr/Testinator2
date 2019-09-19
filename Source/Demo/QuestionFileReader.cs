using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Demo
{
    public class QuestionFileReader
    {
        private string mFilePath;



        public QuestionInfo Read()
        {
            byte[] header;
            using (var fs = new FileStream(mFilePath, FileMode.Open))
            {
                UInt32 currentSequence = 0;
                while (currentSequence != FileHeader.StartSequence)
                {
                    currentSequence >>= 8;
                    currentSequence |= ((UInt32)(fs.ReadByte())) << 3 * 8;
                }
                var dataStartPos = fs.Position;

                currentSequence = 0;

                while (currentSequence != FileHeader.EndSequence)
                {
                    currentSequence >>= 8;
                    currentSequence |= ((UInt32)(fs.ReadByte())) << 3 * 8;
                }

                var length = (int)(fs.Position - dataStartPos - 4);

                header = new byte[length];
                fs.Position = dataStartPos;
                fs.Read(header, 0, length);


            }
            var qtnInfo = new QuestionInfo();
            
            var authorEndIndex = Array.FindIndex(header, 4, x => x == 0xff);
            var author = Encoding.UTF8.GetString(header.Skip(5).Take(authorEndIndex - 5).ToArray());
            var cats = new List<string>();
            var startSequenceIdx = authorEndIndex + 1;
            for(var i = authorEndIndex + 1; i < header.Length; i++)
            {
                if(header[i] == 0xff)
                {
                    cats.Add(Encoding.UTF8.GetString(header.Skip(startSequenceIdx).Take(i - startSequenceIdx).ToArray()));
                    startSequenceIdx = i + 1;
                }
            }

            qtnInfo.Author = author;
            qtnInfo.Categories = cats.ToArray();
            qtnInfo.Version = (byte)(header[4]);
            qtnInfo.AbsolutePath = mFilePath;

            return qtnInfo;
        }

        public QuestionFileReader(string filePath)
        {
            mFilePath = filePath;
        }
    }
}
