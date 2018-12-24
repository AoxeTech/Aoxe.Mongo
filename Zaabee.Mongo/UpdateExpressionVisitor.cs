using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Driver;
using Zaabee.Mongo.Common;
using ExpressionVisitor = MongoDB.Bson.Serialization.ExpressionVisitor;

namespace Zaabee.Mongo
{
    internal class UpdateExpressionVisitor<T> : ExpressionVisitor
    {
        private readonly List<UpdateDefinition<T>> _updateDefinitionList = new List<UpdateDefinition<T>>();

        private string _fieldName;
        
        public List<UpdateDefinition<T>> GetUpdateDefinition(Expression<Func<T>> expression)
        {
            Visit(expression);
            return _updateDefinitionList;
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
                        Expression.Lambda<Func<object>>(Expression.Convert(memberAssignment.Expression, Types.Object));
                    var value = lambda.Compile().Invoke();
                    _updateDefinitionList.Add(Builders<T>.Update.Set(_fieldName, value));
                }
                else
                {
                    Visit(memberAssignment.Expression);
                }
            }

            return node;
        }
        
        protected override Expression VisitBinary(BinaryExpression node)
        {
            var value = ((ConstantExpression) node.Right).Value;

            if (node.NodeType == ExpressionType.Decrement)
            {
                if (node.Type == Types.Int32)
                    value = -(int) value;
                else if (node.Type == Types.Int64)
                    value = -(long) value;
                else if (node.Type == Types.Double)
                    value = -(double) value;
                else if (node.Type == Types.Decimal)
                    value = -(decimal) value;
                else if (node.Type == Types.Float)
                    value = -(float) value;
                else
                    throw new Exception($"Not support type {node.Type} of field named \"{_fieldName}\"");
            }

            var updateDefinition = Builders<T>.Update.Inc(_fieldName, value);

            _updateDefinitionList.Add(updateDefinition);

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

        private void SetList(Expression node)
        {
            var lambda = Expression.Lambda(node);
            var value = lambda.Compile().DynamicInvoke();
            if (node.Type.IsArray)
            {
                switch (node.Type.Name)
                {
                    case "String[]":
                        _updateDefinitionList.Add(Builders<T>.Update.Set(_fieldName, (string[]) value));
                        break;
                    case "Int32[]":
                        _updateDefinitionList.Add(Builders<T>.Update.Set(_fieldName, (int[]) value));
                        break;
                    case "Int64[]":
                        _updateDefinitionList.Add(Builders<T>.Update.Set(_fieldName, (long[]) value));
                        break;
                    default: throw new Exception("This array type is not supported");
                }
            }
            else
            {
                switch (node.Type.GenericTypeArguments[0].Name)
                {
                    case "String":
                        _updateDefinitionList.Add(Builders<T>.Update.Set(_fieldName, (List<string>) value));
                        break;
                    case "Int32":
                        _updateDefinitionList.Add(Builders<T>.Update.Set(_fieldName, (List<int>) value));
                        break;
                    default:
                        _updateDefinitionList.Add(Builders<T>.Update.Set(_fieldName, (IList) value));
                        break;
                }
            }
        }
        
        protected override Expression VisitConstant(ConstantExpression node)
        {
            var value = node.Type.IsEnum ? (int) node.Value : node.Value;

            _updateDefinitionList.Add(Builders<T>.Update.Set(_fieldName, value));

            return node;
        }
        
        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Type.GetInterfaces().Any(a => a == typeof(IList)))
            {
                SetList(node);
            }
            else
            {
                var lambda = Expression.Lambda<Func<object>>(Expression.Convert(node, Types.Object));
                var value = lambda.Compile().Invoke();

                if (node.Type.IsEnum)
                    value = (int) value;

                _updateDefinitionList.Add(Builders<T>.Update.Set(_fieldName, value));
            }

            return node;
        }
    }
}