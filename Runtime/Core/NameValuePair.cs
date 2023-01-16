using System.Collections.Generic;

namespace JeffreyLanters.WebRequests.Core {

  /// <summary>
  /// A serializable name value pair which can be used to create headers and
  /// query parameters.
  /// </summary>
  public class NameValuePair {

    /// <summary>
    /// The name of header.
    /// </summary>
    public string name { get; } = "";

    /// <summary>
    /// The value of the header.
    /// </summary>
    public string value { get; } = "";

    /// <summary>
    /// Creates a new name value pair.
    /// </summary>
    /// <param name="name">The name of the pair.</param>
    /// <param name="value">The value of the pair.</param>
    public NameValuePair (string name, object value) {
      this.name = name;
      this.value = value.ToString ();
    }
  }
}