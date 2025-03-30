namespace CoreStartApp.Models.Db
{
    /// <summary>
    /// модель пользователя в блоге
    /// </summary>
    public class User
    {
        // Уникальный идентификатор сущности в базе
        public Guid Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTime JoinDate { get; set; }
    }
}