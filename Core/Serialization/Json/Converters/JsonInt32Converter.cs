using Newtonsoft.Json;
using System;

namespace Core.Serialization
{
    public class JsonInt32Converter : JsonConverter
    {
        public override bool CanWrite { get { return false; } }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Int32) ||
                    objectType == typeof(Int64) ||
                    // need this last one in case we "weren't given" the type and this will be
                    // accounted for by `ReadJson` checking tokentype
                    objectType == typeof(object)
                ;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return (reader.TokenType == JsonToken.Integer)
                ? Convert.ToInt32(reader.Value)     // convert to Int32 instead of Int64
                : serializer.Deserialize(reader);   // default to regular deserialization
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            // since CanWrite returns false, we don't need to implement this
            throw new NotImplementedException();
        }
    }
}