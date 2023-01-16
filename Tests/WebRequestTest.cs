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
        var request = new WebRequest ("https://jsonplaceholder.typicode.com/users") {
          headers = new Header[] {
            new Header ("Content-Type", "application/json")
          }
        };
        Debug.Log ($"URL {request.url}");
        var response = await request.Send ();
        var users = response.Json<User[]> ();
        foreach (var user in users) {
          Debug.Log ($"Got user {user.name}!");
        }
        Debug.Log ($"Response status was {response.httpStatus}");
        foreach (var header in response.headers) {
          Debug.Log ($"Response header {header.name} has value {header.value}");
        }
      } catch (WebRequestException exception) {
        Debug.Log ($"Error while getting data from {exception.url}, error {exception.httpStatus}");
      }
    }
  }
}