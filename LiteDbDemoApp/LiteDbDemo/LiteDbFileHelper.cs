using LiteDB;
using System;
using System.IO;

namespace LiteDbDemo
{
    public class LiteDbFileHelper : LiteDbBase
    {
        /// <summary>
        /// Upload a file from file system to database
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="filePath"></param>
        public void UploadFile(string identifier, string filePath)
        {
            LiteDb.FileStorage.Upload(identifier, filePath);
        }

        /// <summary>
        /// Get image stored in database
        /// </summary>
        /// <param name="identifier">Image identifier</param>
        /// <returns>Image in byte array</returns>
        public byte[] GetFileBytes(string identifier)
        {
            using (var stream = new System.IO.MemoryStream())
            {
                LiteFileInfo file = LiteDb.FileStorage.Download(identifier, stream);

                return stream.ToArray();
            }

        }

        /// <summary>
        /// Save image from database to file system
        /// </summary>
        /// <param name="identifier">Image identifier</param>
        /// <param name="fileName">Image filename</param>
        public void DownloadFile(string identifier, string fileName)
        {
            using (var stream = new MemoryStream())
            {
                LiteFileInfo file = LiteDb.FileStorage.Download(identifier, stream);

                var extension = file.MimeType.Split("/")[1];
                file.SaveAs($@"C:\Users\{Environment.UserName}\Downloads\{fileName}.{extension}");
            }
        }
    }
}
