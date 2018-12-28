using System;

namespace Catapult.Models.Docker
{
    public class Container
    {
        public string Id { get; set; }
        public string[] Names { get; set; }
        public string Image { get; set; }
        public string ImageID { get; set; }
        public string Command { get; set; }
        public ulong Created { get; set; }
        public Port[] Ports { get; set; }
        public ContainerLabels Labels { get; set; }
        public string State { get; set; }
        public string Status { get; set; }
        public Hostconfig HostConfig { get; set; }
        public Networksettings NetworkSettings { get; set; }
        public Mount[] Mounts { get; set; }

        public string CreatedFormatted => new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).AddSeconds(Created).ToLocalTime().ToString();

        public string Url => $"http://{Names[0].Replace("/", "")}/NAV";
    }

    public class ContainerLabels
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

    public class Hostconfig
    {
        public string NetworkMode { get; set; }
        public string Isolation { get; set; }
        public ulong Memory { get; set; }
    }

    public class Networksettings
    {
        public Networks Networks { get; set; }
    }

    public class Networks
    {
        public Transparentnet TransparentNet { get; set; }
    }

    public class Transparentnet
    {
        public string NetworkID { get; set; }
        public string EndpointID { get; set; }
        public string Gateway { get; set; }
        public string IPAddress { get; set; }
        public int IPPrefixLen { get; set; }
        public string IPv6Gateway { get; set; }
        public string GlobalIPv6Address { get; set; }
        public int GlobalIPv6PrefixLen { get; set; }
        public string MacAddress { get; set; }
    }

    public class Port
    {
        public int PrivatePort { get; set; }
        public string Type { get; set; }
    }

    public class Mount
    {
        public string Type { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public string Mode { get; set; }
        public bool RW { get; set; }
        public string Propagation { get; set; }
    }

}
