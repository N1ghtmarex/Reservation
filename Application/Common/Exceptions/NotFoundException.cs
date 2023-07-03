namespace Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key) : base($"Сущность {name} с параметром \"{key}\" не найдена.") {}
    }
}
