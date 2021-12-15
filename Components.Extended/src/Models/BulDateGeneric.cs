using System;

namespace StableCube.Bulzor.Components.Extended
{
    public struct BulDateGeneric
    {
        public int Day { get; private set; }
        public int Month { get; private set; }
        public int Year { get; private set; }

        public BulDateGeneric(int year, int month, int day)
        {
            Year = year;
            Month = month;
            Day = day;
        }

        public BulDateGeneric(DateOnly source)
        {
            Day = source.Day;
            Month = source.Month;
            Year = source.Year;
        }

        public BulDateGeneric(DateTime source)
        {
            Day = source.Day;
            Month = source.Month;
            Year = source.Year;
        }

        public BulDateGeneric(DateTimeOffset source)
        {
            Day = source.Day;
            Month = source.Month;
            Year = source.Year;
        }
    }
}
