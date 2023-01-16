using JeffreyLanters.WebRequests.Core;

namespace JeffreyLanters.WebRequests {

  /// <summary>
  /// HTTP query parameters let the client and the server pass additional
  /// information with an HTTP request or response. An HTTP query parameter
  /// consists of its case-insensitive name followed by its value. Whitespace
  /// before the value is ignored.
  /// </summary>
  public class QueryParameter : NameValuePair {

    /// <summary>
    /// Creates a new query parameter.
    /// </summary>
    /// <param name="name">The name of the query parameter.</param>
    /// <param name="value">The value of the query parameter.</param>
    public QueryParameter (string name, object value) : base (name, value) { }
  }
}