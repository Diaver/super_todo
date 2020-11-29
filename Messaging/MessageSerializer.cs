using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Messaging
{
    /// <summary>
    /// Serializer used for all messages.
    /// </summary>
    public static class MessageSerializer
    {
        private static readonly JsonSerializerSettings SerializerSettings;

        /// <summary>
        /// Constructor.
        /// </summary>
        static MessageSerializer()
        {
            SerializerSettings = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat
            };
            SerializerSettings.Converters.Add(new StringEnumConverter 
            { 
                NamingStrategy = new CamelCaseNamingStrategy()
            });
        }

        /// <summary>
        /// Serialize an object to a JSON string.
        /// </summary>
        /// <param name="value">The value to serialize.</param>
        public static string Serialize(object value)
        {
            return JsonConvert.SerializeObject(value, SerializerSettings);
        }

        /// <summary>
        /// Deserialize JSON to an object.
        /// </summary>
        /// <param name="value">The JSON data to deserialize.</param>
        public static JObject Deserialize(string value)
        {
            return JsonConvert.DeserializeObject<JObject>(value, SerializerSettings);
        }
    }
}
