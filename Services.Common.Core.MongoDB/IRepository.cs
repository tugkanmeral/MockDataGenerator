using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Common.Core.MongoDB
{
    public interface IRepository<TDocument>
        where TDocument : IDocument
    {
        IMongoCollection<TDocument> Collection { get; }

        void InsertOneAsync(TDocument document);

        void Insert(TDocument document);

        void InsertManyAsync(IEnumerable<TDocument> documents);

        void InsertMany(IEnumerable<TDocument> documents);

        Task<TDocument> GetAsync(Expression<Func<TDocument, bool>> filterExpression);

        TDocument Get(Expression<Func<TDocument, bool>> filterExpression);

        Task<IEnumerable<TDocument>> GetListAsync(Expression<Func<TDocument, bool>> filterExpression);

        IEnumerable<TDocument> GetList(Expression<Func<TDocument, bool>> filterExpression);

        void ReplaceOneAsync(Expression<Func<TDocument, bool>> filterExpression, TDocument document);

        void ReplaceOne(Expression<Func<TDocument, bool>> filterExpression, TDocument document);

        void UpdateOneAsync(Expression<Func<TDocument, bool>> filterExpression, Dictionary<string, string> updateExpression, TDocument document);

        void UpdateOne(Expression<Func<TDocument, bool>> filterExpression, Dictionary<string, string> updateExpression, TDocument document);

        void DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression);

        void DeleteOne(Expression<Func<TDocument, bool>> filterExpression);

        void DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression);

        void DeleteMany(Expression<Func<TDocument, bool>> filterExpression);
    }
}
