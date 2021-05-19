namespace JeffreyLanters.WebRequests {

  /// <summary>
  /// The Content-Type entity header is used to indicate the media type of the 
  /// resource. In responses and requests, a Content-Type header tells the 
  /// client what the content type of the returned content actually is.
  /// </summary>
  public enum ContentType {

    /// <summary>
    /// Plain text.
    /// </summary>
    TextPlain = 0,

    /// <summary>
    /// JavaScript Object Notation.
    /// </summary>
    ApplicationJson = 1,
  }

  /// <summary>
  /// Extension class for Content Type enum.
  /// </summary>
  public static class ContentTypeExtension {

    /// <summary>
    /// Converts the content type into a http valid string value.
    /// </summary>
    /// <param name="contentType">The content type.</param>
    /// <returns>Http valid value.</returns>
    public static string ToHttpContentTypeString (this ContentType contentType) {
      switch (contentType) {
        default:
        case ContentType.TextPlain:
          return "text/plain";
        case ContentType.ApplicationJson:
          return "application/json";
      }
    }
  }
}