namespace elections.Constants
{
    public static class Constant
    {
        public static class Election
        {
            public static class Type
            {
                public const string Ordinary = "Ordinária";
                public const string Supplementary = "Suplementar";
            }

            public static class Role
            {
                public const string President = "PRESIDENTE";
                public const string Governor = "GOVERNADOR";
                public const string Mayor = "PREFEITO";
                public const string Senator = "SENADOR";
                public const string HouseRepresentative = "DEPUTADO FEDERAL";
                public const string StateRepresentative = "DEPUTADO ESTADUAL";
                public const string CityCouncilor = "VEREADOR";
            }

        }

        public static class Color
        {
            public const string Blue = "Blue";
            public const string Red = "Red";
            public const string Green = "Green";
            public const string DarkBlue = "DarkBlue";
            public const string Yellow = "Yellow";
            public const string Orange = "Orange";
            public const string Purple = "Purple";
            public const string Grey = "Grey";

            public static readonly string[] All =
            [
                Blue,
                Red,
                Green,
                DarkBlue,
                Yellow,
                Orange,
                Purple,
                Grey
            ];
        }

        public static class MetropolitanAreaType
        {
            public const string Standard = "REGIÃO METROPOLITANA";
            public const string IntegratedAreaDevelopment = "REGIÃO INTEGRADA DE DESENVOLVIMENTO";
        }
    }
}
