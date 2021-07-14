using UnityEngine;

namespace JeffreyLanters.WebRequests.Tests {

  [AddComponentMenu ("Web Request/Tests/Web Request Tests")]
  public class WebRequestTest : MonoBehaviour {

    [System.Serializable]
    private class User {
      public string id;
      public string name;
      public string email;
    }

    public async void Start () {
      try {
        var response = await new WebRequest ("https://jsonplaceholder.typicode.com/users").Send ();
        var users = response.Json<User[]> ();
        foreach (var user in users) {
          Debug.Log ($"Got user {user.name}!");
        }
      } catch (WebRequestException exception) {
        Debug.Log ($"Error while getting data from {exception.url}, error {exception.httpStatus}");
      }
    }
  }
}