namespace ApartmentManagement.Data.Interfaces
{
    public interface IHasSoftDelete
    {
        bool IsDeleted { get; set; }
    }
}
