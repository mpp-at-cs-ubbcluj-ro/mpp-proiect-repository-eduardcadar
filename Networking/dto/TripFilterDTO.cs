using System;

namespace Networking.dto
{
    [Serializable]
    public record TripFilterDTO
    {
        public string Destination { get; init; }
        public TimeSpan StartTime { get; init; }
        public TimeSpan EndTime { get; init; }
    }
}
