namespace SprintBusiness.Domain.Base.Interfaces;

public interface ITrackableCreator
{
    public int CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }
}


