namespace ELibrary
{
    public class AppOptions
    {
        public LoggingOptions Logging { get; set; }
        public string DbConnection { get; set; }
        public string Secret { get; set; }
        
        public bool SwaggerEnabled { get; set; }
    }

    public class LoggingOptions
    {
        public LogLevel LogLevel { get; set; }
    }

    public class LogLevel
    {
        public string Default { get; set; }
        public string Microsoft { get; set; }
    }

}