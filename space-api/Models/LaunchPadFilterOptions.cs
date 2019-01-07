namespace SpaceApi.Models
{
    public class LaunchPadFilterOptions
    {
        public string FullName { get; set; }
        public string Status { get; set; }

        public bool HasFilters()
        {
            return !string.IsNullOrWhiteSpace(FullName) ||
                    !string.IsNullOrWhiteSpace(Status);
        }
    }
}