using System.Net.Mime;
using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace _3MeePOSapi.Models
{
    public class Version
    {
        [BsonId]
        public string VersionId { get; set; }
        public string VersionNumber { get; set; }
        public string News { get; set; }
    }
}