namespace Mobalyz.Data
{
    public interface IEntity
    {
        int Id { get; set; }

        bool IsActive { get; set; }
    }
}
