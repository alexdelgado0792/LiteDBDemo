using LiteDB;
using System;

namespace LiteDbDemo
{
    public class LiteDbBase : IDisposable
    {
        private readonly string DbFile = "MyData.db";
        private LiteDatabase _liteDatabase;
        public LiteDatabase LiteDb
        {
            get
            {
                if (_liteDatabase == null)
                {
                    _liteDatabase = new LiteDatabase(DbFile);
                }

                return _liteDatabase;
            }
        }
       
        public void Dispose()
        {
            LiteDb.Dispose();
        }
    }
}
