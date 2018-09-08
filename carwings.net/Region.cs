using System;

namespace carwings.net
{
    public enum Region
    {
        Australia,
        Canada,
        Europe,
        Japan,
        USA,
    }

    public static class RegionExtensions
    {
        public static string ToRegionCode(this Region region)
        {
            switch (region)
            {
                case Region.Australia:
                    return "NMA";
                case Region.Canada:
                    return "NCI";
                case Region.Europe:
                    return "NE";
                case Region.Japan:
                    return "NML";
                case Region.USA:
                    return "NNA";
            }

            throw new ArgumentOutOfRangeException(nameof(region), "Unsupported region");
        }
    }
}
