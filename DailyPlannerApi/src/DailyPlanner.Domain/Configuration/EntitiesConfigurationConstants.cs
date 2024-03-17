namespace DailyPlanner.Domain.Configuration;

public static class EntitiesConfigurationConstants
{
    public const string DateTimeFormat = "smalldatetime";

    public static class BoardConstants
    {
        public const int MaxTitleLength = 125;
    }

    public static class ColumnConstants
    {
        public const int MaxTitleLength = 125;
    }

    public static class CardConstants
    {
        public const int MaxTitleLength = 250;
        public const int MaxDescriptionLength = 500;
    }

    public static class ToDoListConstants
    {
        public const int MaxTitleLength = 150;
    }

    public static class ToDoItemConstants
    {
        public const int MaxTitleLength = 350;
    }
}