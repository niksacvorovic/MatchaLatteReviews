using MatchaLatteReviews.Domain.Enums;
using Newtonsoft.Json;
using System;

namespace MatchaLatteReviews.Domain.Model
{
    public class Version
    {
        private string versionName;
        private Format format;
        private DateTime releaseDate;

        public string VersionName { get => versionName; set => versionName = value; }
        public Format Format { get => format; set => format = value; }
        public DateTime ReleaseDate { get => releaseDate; set => releaseDate = value; }

        [JsonConstructor]
        public Version(string versionName, Format format, DateTime releaseDate)
        {
            VersionName = versionName;
            Format = format;
            ReleaseDate = releaseDate;
        }
    }
}
