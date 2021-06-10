using UnityEngine;
using System.Collections;
using System.Threading.Tasks;
using JeffreyLanters.WebRequests.Core;
using UnityEngine.Networking;
using System.Text;

namespace JeffreyLanters.WebRequests {

  /// <summary>
  /// Wrapper class for creating and sending responseless web requests.
  /// </summary>
  public class WebRequest : WebRequest<EmptyResponse> {

    /// <summary>
    /// Creates a new type-less web request.
    /// </summary>
    /// <param name="url">The URL of the web request.</param>
    public WebRequest (string url) : base (url) { }
  }

  /// <summary>
  /// Wrapper class for creating and sending web requests to external servers.
  /// </summary>
  /// <typeparam name="ResponseDataType">The type the response will be cased to.</typeparam>
  public class WebRequest<ResponseDataType> where ResponseDataType : class {

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
    /// The raw body of the web request, this value will be stringified based on
    /// the content type and send as an encoded value. When no content type is
    /// specified, the body will be send as a string using plain/text.
    /// </summary>
    public object body = null;

    /// <summary>
    /// 
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
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task<ResponseDataType> Send () {
      //
      var _didComplete = false;
      var _unityWebRequest = this.ToUnityWebRequest ();
      RoutineTicker.StartCompletableCoroutine (
        this.SendUnityWebRequest (_unityWebRequest),
        () => _didComplete = true);
      while (_didComplete == false)
        await Task.Yield ();
      //
      if (_unityWebRequest.result != UnityWebRequest.Result.Success)
        throw new WebRequestException (
          (int)_unityWebRequest.responseCode,
          _unityWebRequest.downloadHandler.text,
          this.url
        );
      //
      var _responseText = _unityWebRequest.downloadHandler.text;
      var _hasResponseText = _responseText.Trim ().Length > 0;
      if (_hasResponseText == true) {
        switch (ContentTypeExtension.Parse (_unityWebRequest)) {
          // 
          default:
          case ContentType.Unsupported:
            return default (ResponseDataType);
          //
          case ContentType.TextPlain:
            return _responseText as ResponseDataType;
          //
          case ContentType.ApplicationJson:
            return typeof (ResponseDataType).IsArray ?
              JsonUtility.FromJson<JsonArrayWrapper<ResponseDataType>> ($"{{\"array\":{_responseText}}}").array :
              JsonUtility.FromJson<ResponseDataType> (_responseText);
        }
      }
      //
      return default (ResponseDataType);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private IEnumerator SendUnityWebRequest (UnityWebRequest unityWebRequest) {
      yield return unityWebRequest.SendWebRequest ();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private UnityWebRequest ToUnityWebRequest () {
      //
      var _unityWebRequest = new UnityWebRequest (this.url);
      _unityWebRequest.method = this.method.ToString ().ToUpper ();
      _unityWebRequest.SetRequestHeader ("X-HTTP-Method-Override", _unityWebRequest.method);
      //
      foreach (var _header in this.headers)
        _unityWebRequest.SetRequestHeader (_header.name, _header.value);
      //
      if (this.body != null) {
        var _encodedBody = null as byte[];
        switch (this.contentType) {
          //
          default:
          case ContentType.Unsupported:
          case ContentType.TextPlain:
            _encodedBody = Encoding.ASCII.GetBytes (this.body.ToString ());
            break;
          //
          case ContentType.ApplicationJson:
            _encodedBody = Encoding.ASCII.GetBytes (JsonUtility.ToJson (this.body));
            break;
        }
        _unityWebRequest.uploadHandler = new UploadHandlerRaw (_encodedBody);
        _unityWebRequest.uploadHandler.contentType = this.contentType.Stringify ();
      }
      _unityWebRequest.downloadHandler = new DownloadHandlerBuffer ();
      return _unityWebRequest;
    }
  }
}