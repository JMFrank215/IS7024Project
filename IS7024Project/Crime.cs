﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using QuickType;
//
//    var crime = Crime.FromJson(jsonString);

namespace QuickType
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Crime
    {
        [JsonProperty("COMMUNITY_COUNCIL_NEIGHBORHOOD")]
        public string CommunityCouncilNeighborhood { get; set; }

        [JsonProperty("CPD_NEIGHBORHOOD")]
        public string CpdNeighborhood { get; set; }

        [JsonProperty("DATE_FROM")]
        public string DateFrom { get; set; }

        [JsonProperty("DAYOFWEEK")]
        public string Dayofweek { get; set; }

        [JsonProperty("INCIDENT_NO")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long IncidentNo { get; set; }

        [JsonProperty("LATITUDE_X")]
        public string LatitudeX { get; set; }

        [JsonProperty("LOCATION")]
        public string Location { get; set; }

        [JsonProperty("LONGITUDE_X")]
        public string LongitudeX { get; set; }

        [JsonProperty("OFFENSE")]
        public string Offense { get; set; }

        [JsonProperty("ZIP")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Zip { get; set; }
    }

    public partial class Crime
    {
        public static Crime[] FromJson(string json) => JsonConvert.DeserializeObject<Crime[]>(json, QuickType.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Crime[] self) => JsonConvert.SerializeObject(self, QuickType.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}