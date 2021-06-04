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

    public RequestMethod method = RequestMethod.Get;

    public ContentType contentType = ContentType.TextPlain;

    /// <summary>
    /// The raw body of the web request, this value will be encoded. Use the
    /// contet type to define the type of the body, this is plain text by default.
    /// </summary>
    public string body = "";

    public Header[] headers = new Header[0];

    private UnityWebRequest unityWebRequest = null;

    /// <summary>
    /// Creates a new web request.
    /// </summary>
    /// <param name="url">The URL of the web request.</param>
    public WebRequest (string url) {
      this.url = url;
    }

    public async Task<ResponseDataType> Send () {
      var _didComplete = false;
      RoutineTicker.StartCompletableCoroutine (
        this.CreateAndSendUnityWebRequest (),
        () => _didComplete = true);
      while (_didComplete == false)
        await Task.Yield ();
      if (this.unityWebRequest.result != UnityWebRequest.Result.Success)
        throw new WebRequestException (
          (int)this.unityWebRequest.responseCode,
          this.unityWebRequest.downloadHandler.text,
          this.url
        );

      var _responseText = this.unityWebRequest.downloadHandler.text;
      var _hasResponseText = _responseText.Trim ().Length > 0;
      if (_hasResponseText == true) {
        switch (ContentTypeExtension.Parse (this.unityWebRequest)) {
          default:
          case ContentType.Unsupported:
            return default (ResponseDataType);
          case ContentType.TextPlain:
            return _responseText as ResponseDataType;
          case ContentType.ApplicationJson:
            return typeof (ResponseDataType).IsArray ?
              JsonUtility.FromJson<JsonArrayWrapper<ResponseDataType>> ($"{{\"array\":{_responseText}}}").array :
              JsonUtility.FromJson<ResponseDataType> (_responseText);
        }
      }
      return default (ResponseDataType);
    }

    private IEnumerator CreateAndSendUnityWebRequest () {
      this.unityWebRequest = new UnityWebRequest (this.url);
      this.unityWebRequest.method = this.method.ToString ().ToUpper ();
      this.unityWebRequest.SetRequestHeader ("X-HTTP-Method-Override", this.unityWebRequest.method);
      foreach (var _header in this.headers)
        this.unityWebRequest.SetRequestHeader (_header.name, _header.value);
      if (this.body.Length > 0) {
        this.unityWebRequest.uploadHandler = new UploadHandlerRaw (Encoding.ASCII.GetBytes (this.body));
        this.unityWebRequest.uploadHandler.contentType = this.contentType.Stringify ();
      }
      this.unityWebRequest.downloadHandler = new DownloadHandlerBuffer ();
      yield return this.unityWebRequest.SendWebRequest ();
    }
  }
}