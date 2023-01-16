using System.Collections.Generic;

namespace JeffreyLanters.WebRequests {

  /// <summary>
  /// HTTP headers let the client and the server pass additional information with
  /// an HTTP request or response. An HTTP header consists of its case-insensitive 
  /// name followed by its value. Whitespace before the value is ignored.
  /// </summary>
  public class Header {

    /// <summary>
    /// The name of header.
    /// </summary>
    public string name { get; } = "";

    /// <summary>
    /// The value of the header.
    /// </summary>
    public string value { get; } = "";

    /// <summary>
    /// Creates a new header.
    /// </summary>
    /// <param name="name">The name of the header.</param>
    /// <param name="value">The value of the header.</param>
    public Header (string name, object value) {
      this.name = name;
      this.value = value.ToString ();
    }

    /// <summary>
    /// Returns an array of headers from a dictionary.
    /// </summary>
    /// <param name="dictionary">The dictionary to convert.</param>
    /// <returns>An array of headers.</returns>
    public static Header[] FromDictionary (Dictionary<string, string> dictionary) {
      var headers = new Header[dictionary.Count];
      var index = 0;
      foreach (var key in dictionary.Keys) {
        headers[index] = new Header (key, dictionary[key]);
        index++;
      }
      return headers;
    }
  }
}