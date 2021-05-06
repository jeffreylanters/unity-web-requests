using UnityEngine;
using System.Collections;
using System.Threading.Tasks;
using JeffreyLanters.WebRequests.Core;
using UnityEngine.Networking;

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
    /// The URL of the webrequest.
    /// </summary>
    public string url { get; private set; } = "";

    public RequestMethod method = RequestMethod.Get;

    public ContentType contentType = ContentType.TextPlain;

    public string body = "";

    public Header[] headers = new Header[0];

    private UnityWebRequest unityWebRequest = new UnityWebRequest ();

    /// <summary>
    /// Creates a new web request.
    /// </summary>
    /// <param name="url">The URL of the web request.</param>
    public WebRequest (string url) {
      this.url = url;
    }

    public async Task<ResponseDataType> Send () {
      var _didComplete = false;
      RoutineTicker.StartCompletableCoroutine (this.CreateUnityWebRequest (), () => _didComplete = true);
      while (_didComplete == false)
        await Task.Delay (1);
      // how the reponse will be parsed is based on the reponses's content type
      return JsonUtility.FromJson<ResponseDataType> ("{ \"token\":\"123abc\" }");
    }

    private IEnumerator CreateUnityWebRequest () {
      this.unityWebRequest.method = this.method.ToString ().ToUpper ();
      this.unityWebRequest.SetRequestHeader ("X-HTTP-Method-Override", this.unityWebRequest.method);
      yield return new WaitForSeconds (2);
    }
  }
}