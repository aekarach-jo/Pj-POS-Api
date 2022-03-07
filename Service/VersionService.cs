using MongoDB.Driver;
using _3MeePOSapi.Models;
using System.Collections.Generic;
using System.Linq;

namespace _3MeePOSapi.Services
{
    public class VersionService
    {
        private readonly IMongoCollection<Version> _version;

        public VersionService(DatabaseSetting settings)
        {
            var Client = new MongoClient(settings.ConnectionString);
            var database = Client.GetDatabase(settings.DatabaseName);

            _version = database.GetCollection<Version>(settings.VersionCollection);
        }

        public Version GetVersions() => _version.Find(vs => true).FirstOrDefault();
        public Version GetVersionsById(string id) => _version.Find(vs => vs.VersionId == id).FirstOrDefault();
        public Version CreateVersion(Version version)
        {
            _version.InsertOne(version);
            return version;
        }
        public void UpdateVersion(string id, Version version) => _version.ReplaceOne(ver => ver.VersionId == id, version);
    }
}