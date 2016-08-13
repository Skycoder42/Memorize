using System;
using Newtonsoft.Json;
using System.Reflection;

namespace Memorize.Core.Models
{
    internal class AlarmSerializer : JsonConverter
    {
        private const string TypeName = nameof(TypeName);
        private const string Data = nameof(Data);

        public override bool CanConvert(Type objectType)
        {
            return typeof(IAlarm).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var alarm = value as IAlarm;
            if (alarm != null) {
                writer.WriteStartObject();
                writer.WritePropertyName(TypeName);
                serializer.Serialize(writer, alarm.GetType());
                writer.WritePropertyName(Data);
                serializer.Serialize(writer, alarm);
                writer.WriteEndObject();
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            IAlarm alarm = null;
            Type type = null;

            while (reader.Read() && reader.TokenType == JsonToken.PropertyName) {
                switch (reader.Value as string) {
                case TypeName:
                    if (!reader.Read())
                        return null;
                    type = serializer.Deserialize<Type>(reader);
                    break;
                case Data:
                    if (!reader.Read())
                        return null;
                    var tValue = reader.Value;
                    var tType = reader.ValueType;
                    alarm = serializer.Deserialize(reader, type) as IAlarm;
                    break;
                default:
                    return null;
                }
            }

            return alarm;
        }
    }
}
