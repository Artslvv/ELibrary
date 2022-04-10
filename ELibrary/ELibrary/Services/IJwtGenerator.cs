namespace ELibrary.Services
{
    public interface IJwtGenerator
    {
        public string Create(string login, string role, string age, string id);
    }
}