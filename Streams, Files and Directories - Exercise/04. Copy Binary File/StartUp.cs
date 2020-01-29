using System;
using System.IO;

namespace CopyBinaryFile
{
    class StartUp
    {
        static void Main()
        {
            const int DEF_SIZE = 4096;

            string inputPath = "./copyMe.png";
            string outputPath = "../../../image.png";

            using FileStream reader = new FileStream(inputPath, FileMode.Open);
            using FileStream writer = new FileStream(outputPath, FileMode.Create);

            byte[] buffer = new byte[DEF_SIZE];

            while (reader.CanRead)
            {
                int readBytes = reader.Read(buffer, 0, buffer.Length);

                if (readBytes == 0)
                {
                    break;
                }

                writer.Write(buffer, 0, buffer.Length);
            }
        }
    }
}
