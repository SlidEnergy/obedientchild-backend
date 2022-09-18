using System.Collections.Generic;

namespace ObedientChild.Infrastructure.SearchImages
{
    public class SearchImagesResult
    {
        public IEnumerable<ImageResult> Images_results { get; set; }
    }

    public class ImageResult
    {
        public string Thumbnail { get; set; }
    }
}
