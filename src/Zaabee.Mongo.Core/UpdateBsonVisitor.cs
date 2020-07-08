using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Bson;

namespace Zaabee.Mongo.Core
{
    internal class UpdateBsonVisitor<T> : MongoDB.Bson.Serialization.ExpressionVisitor
    {
        private readonly BsonDocument _updateItems = new BsonDocument();
        private readonly BsonDocument _updateDocument = new BsonDocument();

        private string _fieldName;

        public BsonDocument GetUpdateDefinition(Expression<Func<T>> expression)
        {
            Visit(expression);
            _updateDocument.AddRange(new BsonDocument("$set", _updateItems));
            return _updateDocument;
        }

        protected override Expression VisitMemberInit(MemberInitExpression node)
        {
            foreach (var item in node.Bindings)
            {
                var memberAssignment = (MemberAssignment) item;
                _fieldName = item.Member.Name;

                if (memberAssignment.Expression.NodeType == ExpressionType.MemberInit)
                {
                    var lambda = Expression
                        .Lambda<Func<object>>(Expression.Convert(memberAssignment.Expression, typeof(object)));
                    var value = lambda.Compile().Invoke();
                    _updateItems.AddRange(new BsonDocument(_fieldName, BsonValue.Create(value)));
                }
                else
                    Visit(memberAssignment.Expression);
            }

            return node;
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            var value = ((ConstantExpression) node.Right).Value;

            if (node.NodeType == ExpressionType.Decrement)
            {
                var nodeTypeCode = Type.GetTypeCode(node.Type);
                value = nodeTypeCode switch
                {
                    TypeCode.SByte => -(sbyte) value,
                    TypeCode.Int16 => -(short) value,
                    TypeCode.Int32 => -(int) value,
                    TypeCode.Int64 => -(long) value,
                    TypeCode.Single => -(float) value,
                    TypeCode.Double => -(double) value,
                    TypeCode.Decimal => -(decimal) value,
                    _ => throw new NotSupportedException(
                        $"Not support type {node.Type} of field named \"{_fieldName}\"")
                };
            }

            _updateItems.AddRange(new BsonDocument(_fieldName, BsonValue.Create(value)));

            return node;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            _updateItems.AddRange(new BsonDocument(_fieldName, BsonValue.Create(node.Value)));

            return node;
        }

        protected override Expression VisitNewArray(NewArrayExpression node)
        {
            SetList(node);

            return node;
        }

        protected override Expression VisitListInit(ListInitExpression node)
        {
            SetList(node);

            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Type.GetInterfaces().Any(a => a == typeof(IEnumerable)))
                SetList(node);
            else
            {
                var lambda = Expression.Lambda<Func<object>>(Expression.Convert(node, typeof(object)));
                var value = lambda.Compile().Invoke();
                _updateItems.AddRange(new BsonDocument(_fieldName, value.ToBsonDocument()));
            }

            return node;
        }

        private void SetList(Expression node)
        {
            var lambda = Expression.Lambda(node);
            var value = lambda.Compile().DynamicInvoke();
            _updateItems.AddRange(new BsonDocument(_fieldName, value.ToBsonDocument()));
        }
    }
}