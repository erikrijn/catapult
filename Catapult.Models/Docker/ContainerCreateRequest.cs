namespace Catapult.Models.Docker
{
    public class ContainerCreateRequest
    {
        public string Hostname { get; set; }
        public string Domainname { get; set; }
        public string User { get; set; }
        public bool AttachStdin { get; set; }
        public bool AttachStdout { get; set; }
        public bool AttachStderr { get; set; }
        public bool Tty { get; set; }
        public bool OpenStdin { get; set; }
        public bool StdinOnce { get; set; }
        public string[] Env { get; set; }
        public string[] Cmd { get; set; }
        public string Entrypoint { get; set; }
        public string Image { get; set; }
        public Labels Labels { get; set; }
        public string WorkingDir { get; set; }
        public bool NetworkDisabled { get; set; }
        public string MacAddress { get; set; }
        public string StopSignal { get; set; }
        public Hostconfig HostConfig { get; set; }
    }

    public class Labels
    {
        public string created_by { get; set; }
    }
}
