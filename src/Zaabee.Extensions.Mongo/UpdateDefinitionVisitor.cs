using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Driver;

namespace Zaabee.Extensions.Mongo
{
    internal class UpdateExpressionVisitor<T> : MongoDB.Bson.Serialization.ExpressionVisitor
    {
        private readonly List<UpdateDefinition<T>> _updateDefinitionList = new();
        private string _fieldName;

        public UpdateDefinition<T> GetUpdateDefinition(Expression<Func<T>> expression)
        {
            Visit(expression);
            return new UpdateDefinitionBuilder<T>().Combine(_updateDefinitionList);
        }

        protected override Expression VisitMemberInit(MemberInitExpression node)
        {
            foreach (var item in node.Bindings)
            {
                var memberAssignment = (MemberAssignment) item;
                _fieldName = item.Member.Name;

                if (memberAssignment.Expression.NodeType == ExpressionType.MemberInit)
                {
                    var lambda =
                        Expression.Lambda<Func<object>>(Expression.Convert(memberAssignment.Expression,
                            typeof(object)));
                    var value = lambda.Compile().Invoke();
                    _updateDefinitionList.Add(Builders<T>.Update.Set(_fieldName, value));
                }
                else
                    Visit(memberAssignment.Expression);
            }

            return node;
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            var value = ((ConstantExpression) node.Right).Value;

            if (node.NodeType is ExpressionType.Decrement)
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

            var updateDefinition = Builders<T>.Update.Inc(_fieldName, value);

            _updateDefinitionList.Add(updateDefinition);

            return node;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            _updateDefinitionList.Add(Builders<T>.Update.Set(_fieldName, node.Value));

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

                _updateDefinitionList.Add(Builders<T>.Update.Set(_fieldName, value));
            }

            return node;
        }

        private void SetList(Expression node)
        {
            var lambda = Expression.Lambda(node);
            var value = lambda.Compile().DynamicInvoke();
            _updateDefinitionList.Add(Builders<T>.Update.Set(_fieldName, value));
        }
    }
}