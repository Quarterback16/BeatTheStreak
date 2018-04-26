

namespace BeatTheStreak
{
    public class RequestBuilder
    {
        public string Url { get; set; }

        public RequestBuilder()
        {
            Url = "unknown";
        }

        public RequestBuilder(string url)
        {
            Url = url;
        }

        public RequestBuilder WithUrl( string url)
        {
            Url = url;
            return this;
        }

        public RequestBuilder Build()
        {
            return new RequestBuilder(Url);
        }
    }
}
