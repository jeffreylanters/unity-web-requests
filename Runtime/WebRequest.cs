using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Threading.Tasks;
using JeffreyLanters.WebRequests.Core;
using System.Text;

namespace JeffreyLanters.WebRequests {

  /// <summary>
  /// Wrapper class for creating and sending web requests to external servers.
  /// </summary>
  public class WebRequest {

    /// <summary>
    /// The URL of the web request.
    /// </summary>
    public string url { get; private set; } = "";

    /// <summary>
    /// The Method of the web request.
    /// </summary>
    public RequestMethod method = RequestMethod.Get;

    /// <summary>
    /// The request content type will be used to parse the body.
    /// </summary>
    public ContentType contentType = ContentType.TextPlain;

    /// <summary>
    /// The character set to use when encoding the body.
    /// </summary>
    public string characterSet = "utf-8";

    /// <summary>
    /// The raw body of the web request, this value will be stringified based on
    /// the content type and send as an encoded value. When no content type is
    /// specified, the body will be send as a string using plain/text.
    /// </summary>
    public object body = null;

    /// <summary>
    /// The headers of the web request. These request headers will be appended
    /// when the request will be sent over to the server.
    /// </summary>
    public Header[] headers = new Header[0];

    /// <summary>
    /// Creates a new web request.
    /// </summary>
    /// <param name="url">The URL of the web request.</param>
    public WebRequest (string url) {
      this.url = url;
    }

    /// <summary>
    /// Sends the web request.
    /// </summary>
    /// <returns>A task which will return the response.</returns>
    public async Task<WebRequestResponse> Send () {
      var _didComplete = false;
      var _webRequestHandler = this.ToWebRequestHandler ();
      // The Routine Ticker will instanciate a coroutine which will yield the
      // sending of the actual web requesting using the web request handler.
      // An async routine will await this completion and will halt the method.
      RoutineTicker.StartCompletableCoroutine (
        this.SendWebRequestHandler (_webRequestHandler),
        () => _didComplete = true);
      while (_didComplete == false) {
        await Task.Yield ();
      }
      // When the web request did not result in a successfull flight, an
      // specific web request exception will be thrown. This exception contains
      // data in order to debug the HTTP problem.
      if (_webRequestHandler.result != WebRequestHandler.Result.Success) {
        throw new WebRequestException (_webRequestHandler);
      }
      // When everything went right, a web request response object will be
      // returned, containing the web request's response data allowing for the
      // application to extract as various data types.
      return new WebRequestResponse (_webRequestHandler);
    }

    /// <summary>
    /// Invokes the send method on web request handler.
    /// </summary>
    /// <returns>An enumerator yielding the web request.</returns>
    private IEnumerator SendWebRequestHandler (WebRequestHandler webRequestHandler) {
      yield return webRequestHandler.SendWebRequest ();
    }

    /// <summary>
    /// Converts the configuration into a web request handler model which allows
    /// the web request to be sendt.
    /// </summary>
    /// <returns>A web request handler.</returns>
    private WebRequestHandler ToWebRequestHandler () {
      // Initializes a new web request handler which will eventually be sent and
      // sets its meta-data such as the url and method.
      var _webRequestHandler = new WebRequestHandler ();
      _webRequestHandler.url = this.url;
      _webRequestHandler.method = this.method.ToString ().ToUpper ();
      // Sets all of the headers of the request handler. Some Unity builds will
      // incorrectly set the HTTP Method, so an alternative override value will
      // be passed along as well. Then the custom headers will be appended.
      _webRequestHandler.SetRequestHeader ("X-HTTP-Method-Override", _webRequestHandler.method);
      foreach (var _header in this.headers) {
        _webRequestHandler.SetRequestHeader (_header.name, _header.value);
      }
      // When the web request should post a body, it will be converted to a
      // string allowing any type of data, and then converting it into an
      // encoded byte array which will be set as the upload handlers data.
      if (this.method == RequestMethod.Post && this.body != null) {
        var _encodedBody = Encoding.ASCII.GetBytes (this.body.ToString ());
        var _contentType = this.contentType.Stringify ();
        // We'll set the chararacter set the UTF-8 encoding.
        _contentType += $"; charset={this.characterSet}";
        // When the web request is posting multipart form data, we'll add the
        // boundary to the content type. This header will be used on the server
        // side to deconstruct the request into its various parts.
        if (this.contentType == ContentType.MultipartFormData) {
          _contentType += $"; boundary={FormDataUtility.boundary}";
        }
        _webRequestHandler.uploadHandler = new UploadHandlerRaw (_encodedBody);
        _webRequestHandler.uploadHandler.contentType = _contentType;
      }
      // Add a new download handler to the web request handler allowing for a
      // reponse to come in.
      _webRequestHandler.downloadHandler = new DownloadHandlerBuffer ();
      return _webRequestHandler;
    }
  }
}