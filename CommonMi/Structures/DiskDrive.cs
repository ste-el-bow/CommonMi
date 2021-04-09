namespace CommonMi.Structures
{
    public record DiskDrive
    {
        public int Index { get; init; }
        public string Model { get; init; }
        public string SerialNumber { get; init; }
        public int Capacity { get; init; }
        public string HumanReadableCapacity { get; init; }
    }
}