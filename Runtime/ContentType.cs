using UnityEngine.Networking;

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

    /// <summary>
    /// A not supported content type.
    /// </summary>
    Unsupported = 999,
  }

  /// <summary>
  /// Extension class for Content Type enum.
  /// </summary>
  public static class ContentTypeExtension {

    /// <summary>
    /// Stringifies the content type into a http valid string value.
    /// </summary>
    /// <param name="contentType">The content type.</param>
    /// <returns>Stringified content type.</returns>
    public static string Stringify (this ContentType contentType) {
      switch (contentType) {
        default:
        case ContentType.TextPlain:
          return "text/plain";
        case ContentType.ApplicationJson:
          return "application/json";
      }
    }

    /// <summary>
    /// Parses a http valid string value into a content type.
    /// </summary>
    /// <param name="stringifiedContentType">The stringified content type.</param>
    /// <returns>The Content type.</returns>
    public static ContentType Parse (string stringifiedContentType) {
      switch (stringifiedContentType) {
        default:
          return ContentType.Unsupported;
        case "text/plain":
          return ContentType.TextPlain;
        case "application/json":
          return ContentType.ApplicationJson;
      }
    }

    /// <summary>
    /// Parses the headers of a unity web request value into a content type.
    /// </summary>
    /// <param name="unityWebRequest"></param>
    /// <returns>The Content type.</returns>
    public static ContentType Parse (UnityWebRequest unityWebRequest) {
      return ContentTypeExtension.Parse (unityWebRequest.GetResponseHeader ("Content-Type").Split (';')[0].ToLower ());
    }
  }
}