namespace CommonMi.Structures
{
    public record VideoController
    {
        public string Caption { get; init; }
        public string DriverVersion { get; init; }
        public string Status { get; init; }
        public string InfFilename { get; init; }
        public string PNPDeviceID { get; init; }

        public VideoController(string caption, string driverVersion, string status, string infFilename,
            string pnpDeviceId)
        {
            Caption = caption;
            DriverVersion = driverVersion;
            Status = status;
            InfFilename = infFilename;
            PNPDeviceID = pnpDeviceId;
        }
        
    }
}