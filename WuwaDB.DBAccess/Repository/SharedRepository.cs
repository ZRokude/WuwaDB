﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace WuwaDB.DBAccess.Repository
{
    public class SharedRepository
    {
        public Expression<Func<T, bool>> GetObjectAsExpression<T>(object getProperty) where T:class
        {
            // Create the parameter expression for the entity type
            var parameter = Expression.Parameter(typeof(T), "x");

            // Build the composite predicate
            Expression? predicate = null;

            var properties = getProperty.GetType().GetProperties();
            foreach (var propertyInfo in properties)
            {
                var propertyName = propertyInfo.Name;
                var propertyValue = propertyInfo.GetValue(getProperty);

                var property = Expression.Property(parameter, propertyName);
                var constant = Expression.Constant(propertyValue);
                var convertedConstant = Expression.Convert(constant, property.Type);
                var equality = Expression.Equal(property, convertedConstant);

                if (predicate == null)
                {
                    predicate = equality;
                }
                else
                {
                    predicate = Expression.AndAlso(predicate, equality);
                }
            }

            // Create the lambda expression for the composite predicate
            var lambdaProperty = Expression.Lambda<Func<T, bool>>(predicate!, parameter);
            return lambdaProperty;
        }
    }
}
