﻿

using System.Collections.Generic;

namespace CommandLineTools.Contracts
{
    public interface IFileService
    {
        string ReadAllText(string path);
        string[] ReadAllLines(string path);
        IEnumerable<string> ReadLinesLazily(string path);
        void WriteAllText(string path, string content);
        void WriteAllLines(string path, string[] lines);
    }
}