﻿using Easy.Extend;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Easy.Storage
{
    public class LocalStorage : IStorage
    {
        protected virtual string MapPath(string path)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), path.TrimStart('~').ToFilePath());
        }
        protected virtual string GenerateDictionary(string fileName)
        {
            return Path.Combine("Files", fileName);
        }
        public virtual void AppendFile(Stream stream, string filePath)
        {
            using (FileStream fileStream = new FileStream(MapPath(filePath), FileMode.Append))
            {
                stream.CopyTo(fileStream);
            }
        }

        public virtual void Delete(string filePath)
        {
            File.Delete(MapPath(filePath));
        }

        public virtual string SaveFile(Stream stream, string fileName)
        {
            return SaveFile(stream, GenerateDictionary(fileName), fileName);
        }

        public virtual string SaveFile(Stream stream, string directory, string fileName)
        {
            string filePath = Path.Combine(MapPath(directory), fileName);
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                stream.CopyTo(fileStream);
            }
            return filePath;
        }

        public virtual void DeleteDirectory(string path)
        {
            Directory.Delete(MapPath(path), true);
        }
    }
}
