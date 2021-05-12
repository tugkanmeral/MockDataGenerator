using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Common.Core.MongoDB
{
    public abstract class MongoRepositoryBase<TDocument> : IRepository<TDocument>
        where TDocument : IDocument
    {
        protected IMongoCollection<TDocument> _collection;
        public IMongoCollection<TDocument> Collection => _collection;
        public MongoRepositoryBase(string collectionName)
        {
            string connectionString = "mongodb://localhost:27017/";
            string dbName = "mock_data_generator";
            var client = new MongoClient(connectionString);
            _collection = client?.GetDatabase(dbName)?.GetCollection<TDocument>(collectionName);
        }

        public MongoRepositoryBase()
        {
            string connectionString = "mongodb://localhost:27017/";
            string dbName = "mock_data_generator";
            var client = new MongoClient(connectionString);
            var collection = client?.GetDatabase(dbName)?.GetCollection<TDocument>(typeof(TDocument).Name);

            _collection = collection;
        }

        public MongoRepositoryBase(IMongoClient client)
        {
            string dbName = "mock_data_generator";
            var collection = client?.GetDatabase(dbName)?.GetCollection<TDocument>(typeof(TDocument).Name);
            _collection = collection;
        }

        #region CREATE
        public virtual async void InsertOneAsync(TDocument document)
        {
            await _collection.InsertOneAsync(document);
        }

        public virtual void Insert(TDocument document)
        {
            _collection.InsertOne(document);
        }

        public virtual async void InsertManyAsync(IEnumerable<TDocument> documents)
        {
            await _collection.InsertManyAsync(documents);
        }

        public virtual void InsertMany(IEnumerable<TDocument> documents)
        {
            _collection.InsertMany(documents);
        }
        #endregion CREATE

        #region READ
        public virtual async Task<TDocument> GetAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return await _collection.Find(filterExpression).FirstOrDefaultAsync();
        }

        public virtual TDocument Get(Expression<Func<TDocument, bool>> filterExpression)
        {
            return _collection.Find(filterExpression).FirstOrDefault();
        }

        public virtual async Task<IEnumerable<TDocument>> GetListAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            var resultList = await _collection.FindAsync(filterExpression);
            return resultList.ToEnumerable();
        }

        public virtual IEnumerable<TDocument> GetList(Expression<Func<TDocument, bool>> filterExpression)
        {
            return _collection.Find(filterExpression).ToEnumerable();
        }
        #endregion READ

        #region UPDATE
        public virtual async void ReplaceOneAsync(Expression<Func<TDocument, bool>> filterExpression, TDocument document)
        {
            await _collection.FindOneAndReplaceAsync(filterExpression, document);
        }

        public virtual void ReplaceOne(Expression<Func<TDocument, bool>> filterExpression, TDocument document)
        {
            _collection.FindOneAndReplace(filterExpression, document);
        }

        public virtual async void UpdateOneAsync(Expression<Func<TDocument, bool>> filterExpression, Dictionary<string, string> updateExpression, TDocument document)
        {
            var filterBuilder = Builders<TDocument>.Filter.Where(filterExpression);
            var updateDefinition = Builders<TDocument>.Update.Combine();
            foreach (KeyValuePair<string, string> expression in updateExpression)
            {
                updateDefinition.AddToSet(expression.Key, expression.Value);
            }
            await _collection.UpdateOneAsync(filterBuilder, updateDefinition);
        }

        public virtual void UpdateOne(Expression<Func<TDocument, bool>> filterExpression, Dictionary<string, string> updateExpression, TDocument document)
        {
            var filterBuilder = Builders<TDocument>.Filter.Where(filterExpression);
            var updateDefinition = Builders<TDocument>.Update.Combine();
            foreach (KeyValuePair<string, string> expression in updateExpression)
            {
                updateDefinition.AddToSet(expression.Key, expression.Value);
            }
            _collection.UpdateOne(filterBuilder, updateDefinition);
        }
        #endregion UPDATE

        #region DELETE
        public virtual async void DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            await _collection.DeleteOneAsync(filterExpression);
        }

        public virtual void DeleteOne(Expression<Func<TDocument, bool>> filterExpression)
        {
            _collection.DeleteOne(filterExpression);
        }

        public virtual async void DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            await _collection.DeleteManyAsync(filterExpression);
        }

        public virtual void DeleteMany(Expression<Func<TDocument, bool>> filterExpression)
        {
            _collection.DeleteMany(filterExpression);
        }
        #endregion DELETE

    }
}
