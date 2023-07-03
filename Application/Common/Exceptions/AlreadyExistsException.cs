namespace Application.Common.Exceptions
{
    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException(string name, object key) : base($"Сущность {name} с параметром \"{key}\" уже существует.") {}
    }
}
