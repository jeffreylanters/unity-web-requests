using System;
using UnityEngine.Networking;
using UnityEngine;

namespace JeffreyLanters.WebRequests.Core {

  /// <summary>
  /// A web request's response.
  /// </summary>
  [Serializable]
  public class WebRequestResponse {

    /// <summary>
    /// The text response from the web request.
    /// </summary>
    public string webRequestResponseText { get; private set; } = "";

    /// <summary>
    /// Instanciates a new web request response based of off a web request
    /// handler. The required data will be extracted from the download handler.
    /// </summary>
    /// <param name="webRequestHandler"></param>
    public WebRequestResponse (WebRequestHandler webRequestHandler) {
      this.webRequestResponseText = webRequestHandler.downloadHandler.text;
    }

    /// <summary>
    /// Return the Web Request's response text.
    /// </summary>
    /// <returns>The Web Request's response text.</returns>
    public string Text () {
      return this.webRequestResponseText;
    }

    /// <summary>
    /// Converts the Web Request's response text as a JSON Objects to a given
    /// data type.
    /// </summary>
    /// <typeparam name="DataType">The Type to convert the JSON to.</typeparam>
    /// <returns>The Object</returns>
    public DataType Json<DataType> () where DataType : class {
      return typeof (DataType).IsArray ?
        JsonUtility.FromJson<JsonArrayWrapper<DataType>> ($"{{\"array\":{this.webRequestResponseText}}}").array :
        JsonUtility.FromJson<DataType> (this.webRequestResponseText);
    }
  }
}