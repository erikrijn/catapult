using System;

namespace Catapult.Models.Docker
{
    public class Image
    {
        public int Containers { get; set; }
        public int Created { get; set; }
        public string Id { get; set; }
        public ImageLabels Labels { get; set; }
        public string ParentId { get; set; }
        public string[] RepoDigests { get; set; }
        public string[] RepoTags { get; set; }
        public int SharedSize { get; set; }
        public long Size { get; set; }
        public long VirtualSize { get; set; }

        public string CreatedFormatted => new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).AddSeconds(Created).ToLocalTime().ToString();
    }

    public class ImageLabels
    {
        public string country { get; set; }
        public string created { get; set; }
        public string cu { get; set; }
        public string eula { get; set; }
        public string legal { get; set; }
        public string maintainer { get; set; }
        public string nav { get; set; }
        public string osversion { get; set; }
        public string platform { get; set; }
        public string tag { get; set; }
        public string version { get; set; }
    }

}
