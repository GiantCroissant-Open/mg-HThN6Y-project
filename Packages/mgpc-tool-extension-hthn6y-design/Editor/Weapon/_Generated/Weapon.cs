﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using F3Fes2021.Game.Extension.MG.Fishing.Weapon.EditorPart.Generated;
//
//    var weapon = Weapon.FromJson(jsonString);

namespace MGPC.Tool.Extension.HThN6Y.Design.Weapon.EditorPart.Generated
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Weapon
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("artAssetName")]
        public string ArtAssetName { get; set; }

        [JsonProperty("rank")]
        public long Rank { get; set; }

        [JsonProperty("normalProjectile")]
        public string NormalProjectile { get; set; }

        [JsonProperty("skillProjectile")]
        public string SkillProjectile { get; set; }
    }

    public partial class Weapon
    {
        public static Weapon FromJson(string json) => JsonConvert.DeserializeObject<Weapon>(json, MGPC.Tool.Extension.HThN6Y.Design.Weapon.EditorPart.Generated.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Weapon self) => JsonConvert.SerializeObject(self, MGPC.Tool.Extension.HThN6Y.Design.Weapon.EditorPart.Generated.Converter.Settings);
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
}
