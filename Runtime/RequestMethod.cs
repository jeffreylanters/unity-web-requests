namespace JeffreyLanters.WebRequests {

  /// <summary>
  /// Defines a set of request methods to indicate the desired action to be 
  /// performed for a given resource. Although they can also be nouns, these 
  /// request methods are sometimes referred to as HTTP verbs. Each of them 
  /// implements a different semantic, but some common features are shared by a 
  /// group of them: e.g. a request method can be safe, idempotent, or cacheable.
  /// </summary>
  public enum RequestMethod {

    /// <summary>
    /// The GET method requests a representation of the specified resource. 
    /// Requests using GET should only retrieve data.
    /// </summary>
    Get = 1,

    /// <summary>
    /// The POST method is used to submit an entity to the specified resource, 
    /// often causing a change in state or side effects on the server.
    /// </summary>
    Post = 2,

    /// <summary>
    /// The PATCH method is used to apply partial modifications to a resource.
    /// </summary>
    Patch = 3,

    /// <summary>
    /// The PUT method replaces all current representations of the target 
    /// resource with the request payload.
    /// </summary>
    Put = 4,

    /// <summary>
    /// The DELETE method deletes the specified resource.
    /// </summary>
    Delete = 5,
  }
}