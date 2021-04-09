namespace CommonMi.Structures
{
    /// <summary>
    /// Record which stores information about operating system.
    /// Name, Architecture, Languages, Build, SystemDrive
    /// </summary>
    public record OsInfo
    {
        public string Name { get; init; }
        public string Architecture { get; init; }
        public string[] Languages { get; init; }
        public string Build { get; init; }
        public string SystemDrive { get; set; }

        public OsInfo(string name, string architecture, string[] languages, string build, string systemDrive)
        {
            Name = name;
            Architecture = architecture;
            Languages = languages;
            Build = build;
            SystemDrive = systemDrive;
        }
        
    }
}