using LiteDB;
using System;

namespace LiteDbDemo
{
    /// <summary>
    /// Lite Db API Helper
    /// </summary>
    /// <typeparam name="Entity">Model/Entity class</typeparam>
    public class LiteDbHelper<Entity> : LiteDbBase
    {
        /// <summary>
        /// Insert a new entity to this collection. Document Id must be a new value in collection
        /// </summary>
        /// <param name="record">Document/Entity to persist</param>
        /// <returns>Returns document Id</returns>
        public Guid Insert(Entity record)
        {
            LoadEntityMapper();

            return GetCollection().Insert(record);
        }

        /// <summary>
        /// Update an existing entity to this collection
        /// </summary>
        /// <param name="record">Document/Entity to persist</param>
        /// <returns>Returns document Id</returns>
        public bool Update(Entity record)
        {
            LoadEntityMapper();

            return GetCollection().Update(record);
        }

        /// <summary>
        /// Update / Insert a docuemnt to this collection
        /// </summary>
        /// <param name="record">Document/Entity to persist</param>
        /// <returns>Returns document Id</returns>
        public bool InsertUpdate(Entity record)
        {
            LoadEntityMapper();

            return GetCollection().Upsert(record);
        }

        /// <summary>
        /// Delete document by identifier.
        /// </summary>
        /// <typeparam name="T">Identifier type</typeparam>
        /// <param name="identifier">entity identifier</param>
        /// <returns>If deleted or not</returns>
        public bool DeleteById<T>(T identifier)
        {
            var collection = GetCollection();
            var toDelete = new BsonValue(identifier);

            if (!RecordExist(collection, "_id", toDelete))
            {
                throw new Exception("Record does not exists or query is invalid.");
            }

            return collection.Delete(toDelete);
        }

        /// <summary>
        ///  Override from original in order to simplify. 
        ///  Get a collection using a entity class as strong typed document. 
        ///  If collection does not exits, create a new one.
        /// </summary>
        /// <returns>Collection</returns>
        public LiteCollection<Entity> GetCollection()
        {
            return LiteDb.GetCollection<Entity>(name: $"{typeof(Entity).Name}s");
        }

        /// <summary>
        /// Drop a collection and all data + indexes
        /// </summary>
        public void DropCollection()
        {
            var deleted = LiteDb.DropCollection(name: $"{typeof(Entity).Name}s");

            if (!deleted)
            {
                throw new Exception("Collection can not be deleted because is already deleted or does not exist.");
            }
        }

        #region Private Methods

        /// <summary>
        /// Load Entity Mapper
        /// </summary>
        private void LoadEntityMapper()
        {
            Type type = Type.GetType($"LiteDbDemo.Mappers.{typeof(Entity).Name}Mapper");
            Activator.CreateInstance(type);
        }

        /// <summary>
        /// Verify if the record exist in the collection.
        /// </summary>
        /// <param name="collection">Document Collections</param>
        /// <param name="field">Field to be verified</param>
        /// <param name="value">Value to be compare</param>
        /// <returns></returns>
        private bool RecordExist(LiteCollection<Entity> collection, string field, BsonValue value)
        {
            return collection.Exists(Query.EQ(field, value));
        }

        #endregion
    }
}
