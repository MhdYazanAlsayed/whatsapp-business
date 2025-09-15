namespace SprintBusiness.Domain.Base.Interfaces
{
    public interface ITrackableTime
    {
        public DateTime CreatedAt { get; }
        public DateTime? UpdatedAt { get; }
    }
}
