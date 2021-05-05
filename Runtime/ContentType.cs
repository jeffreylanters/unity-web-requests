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
}