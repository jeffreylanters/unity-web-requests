using System;

namespace JeffreyLanters.WebRequests {

  /// <summary>
  /// Request exceptions can be thrown when getting the response of a request
  /// which did error or when providing middleware which implements the
  /// onRequestDidCatch method.
  /// </summary>
  public class WebRequestException : Exception {

    /// <summary>
    /// The status code of the servers response.
    /// </summary>
    public int httpStatusCode { get; } = -1;

    /// <summary>
    /// The raw response data of the catched request, may contain information.
    /// </summary>
    public string rawResponseData { get; } = "";

    /// <summary>
    /// The URL of which the request was trying to reach.
    /// </summary>
    public string url { get; } = "";

    /// <summary>
    /// The definition of a status code contains a human readable notation of 
    /// the servers response status code.
    /// </summary>
    public HttpStatus httpStatus { get; } = HttpStatus.Undefined;

    /// <summary>
    /// Instanciates a new request exception which may be thrown.
    /// </summary>
    /// <param name="httpStatusCode">The status code of the servers response.</param>
    /// <param name="rawResponseData">The raw response data of the catched request, may contain information.</param>
    /// <param name="url">The URL of which the request was trying to reach.</param>
    public WebRequestException (int httpStatusCode, string rawResponseData, string url) {
      this.httpStatusCode = httpStatusCode;
      if (HttpStatus.IsDefined (typeof (HttpStatus), this.httpStatusCode))
        this.httpStatus = (HttpStatus)httpStatusCode;
      this.rawResponseData = rawResponseData;
      this.url = url;
    }

    /// <summary>
    /// Turns the request exception into a readable error.
    /// </summary>
    /// <returns>A human readable version of the webrequest.</returns>
    public override string ToString () {
      return $"Request Exception server responded {this.httpStatus} while sending to {this.url}\n{this.rawResponseData}";
    }
  }
}