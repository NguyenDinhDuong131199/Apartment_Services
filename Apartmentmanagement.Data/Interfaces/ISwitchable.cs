using ApartmentManagement.Data.Enums;

namespace ApartmentManagement.Data.Interfaces
{
    public interface ISwitchable
    {
        Status Status { get; set; }
    }
}
