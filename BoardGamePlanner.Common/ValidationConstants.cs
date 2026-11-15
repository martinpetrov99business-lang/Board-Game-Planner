namespace BoardGamePlanner.Common
{
    public static class ValidationConstants
    {
        public static class Venue
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 80;
            public const int AddressMinLength = 5;
            public const int AddressMaxLength = 200;
            public const int CapacityMin = 2;
            public const int CapacityMax = 500;


        }
        public static class Game
        {
            public const int TitleMinLength = 2;
            public const int TitleMaxLength = 100;
            public const int DescriptionMaxLength = 1000;
            public const int PlayersMin = 1;
            public const int PlayersMax = 20;
            public const int PlayTimeMin = 5;
            public const int PlayTimeMax = 600;
            public const int ImageUrlMaxLength = 2048;
        }
        public static class GameNight
        {
            public const int TitleMinLength = 3;
            public const int TitleMaxLength = 100;
            public const int NotesMaxLength = 500;
            public const int MaxGuestsMin = 2;
            public const int MaxGuestsMax = 50;
        }
    }
    public enum Complexity
    {
        Light = 1,
        Medium = 2,
        Heavy = 3
    }
}
