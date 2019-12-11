using System.Collections.Generic;

namespace AQASeleniumSample
{
    public sealed class MenuItem
    {
        public string Url { get; set; }
        public IReadOnlyList<string> SubCategoryUrls { get; set; }
    }
}
