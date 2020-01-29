using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace ZipAndExtract
{
    class StartUp
    {
        static void Main()
        {
            ZipFile.CreateFromDirectory(@"D:\SoftUni\Files\", @"D:\SoftUni\myArchive.zip");

            ZipFile.ExtractToDirectory(@"D:\SoftUni\myArchive.zip", @"C:\Riot Games\Riot Client\Plugins\rcp-be-agent");
        }
    }
}
  