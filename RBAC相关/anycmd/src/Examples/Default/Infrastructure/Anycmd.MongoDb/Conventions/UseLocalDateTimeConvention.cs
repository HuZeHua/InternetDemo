
namespace Anycmd.MongoDb.Conventions
{
    using MongoDB.Bson.Serialization;
    using MongoDB.Bson.Serialization.Conventions;
    using MongoDB.Bson.Serialization.Serializers;
    using System;
    using System.Reflection;

    /// <summary>
    /// Represents the Bson serialization convention that serializes the <see cref="System.DateTime"/> value
    /// by using the local date/time kind.
    /// </summary>
    public class UseLocalDateTimeConvention : IMemberMapConvention
    {
        #region IMemberMapConvention Members
        /// <summary>
        /// Applies the specified member map convention.
        /// </summary>
        /// <param name="memberMap">The member map convention.</param>
        public void Apply(BsonMemberMap memberMap)
        {
            Func<Type, IBsonSerializer> converter = t =>
            {
                if (t == typeof(DateTime))
                    return new DateTimeSerializer(DateTimeKind.Local);
                else if (t == typeof(DateTime?))
                    return new NullableSerializer<DateTime>(new DateTimeSerializer(DateTimeKind.Local));
                return null;
            };

            IBsonSerializer serializer = null;
            switch (memberMap.MemberInfo.MemberType)
            {
                case MemberTypes.Property:
                    PropertyInfo propertyInfo = (PropertyInfo)memberMap.MemberInfo;
                    serializer = converter(propertyInfo.PropertyType);
                    break;
                case MemberTypes.Field:
                    FieldInfo fieldInfo = (FieldInfo)memberMap.MemberInfo;
                    serializer = converter(fieldInfo.FieldType);
                    break;
                default:
                    break;
            }

            if (serializer != null)
                memberMap.SetSerializer(serializer);
        }

        #endregion

        #region IConvention Members
        /// <summary>
        /// Gets the name of the convention.
        /// </summary>
        public string Name
        {
            get { return this.GetType().Name; }
        }

        #endregion
    }
}
