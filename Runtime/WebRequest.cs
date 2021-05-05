using UnityEngine;
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

    public string url { get; private set; } = "";

    public RequestMethod method = RequestMethod.Get;

    public ContentType contentType = ContentType.TextPlain;

    public string body = "";

    public Header[] headers = new Header[0];

    private UnityWebRequest unityWebRequest = new UnityWebRequest ();

    public WebRequest (string url) {
      this.url = url;
    }

    public async Task<ResponseDataType> Send () {
      await Task.Delay (2500);

      // how the reponse will be parsed is based on the reponses's content type
      return JsonUtility.FromJson<ResponseDataType> ("{ \"token\":\"123abc\" }");
    }

    // public WebRequest SetBody<ResponseDataType> (string json) {
    //   // TODO
    //   return this;
    // }

    // public WebRequest<ResponseDataType> SetJsonBody<RequestDataType> (RequestDataType data) where RequestDataType : class {
    //   // TODO
    //   return this;
    // }

    // public async Task<ResponseDataType> Send () where ResponseDataType : class {
    //   // while (this.timer < this.duration) {
    //   //   this.timer += Time.deltaTime;
    //   //   await Task.Delay (1);
    //   // }
    //   // if (Random.Range (0, 100) > 50)
    //   //   throw new WebRequestException (500, "Oh", "No");
    //   return JsonUtility.FromJson<ResponseDataType> ("{ }");
    // }
  }
}