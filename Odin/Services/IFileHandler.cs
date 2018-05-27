﻿namespace Odin.Services
{
    public interface IFileHandler
    {
        string LoadText(string filename);
        void SaveText(string filename, string text);
        bool CheckExistenceOf(string filename);
    }
}