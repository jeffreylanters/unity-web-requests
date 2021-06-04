using System;

namespace JeffreyLanters.WebRequests.Core {

  /// <summary>
  /// A class to wrap top level JSON arrays. When the Model type is a top level
  /// array, it will be wrapped into this empty model in order for Unity to
  /// parse it contents since top level arrays are not supported in Unity.
  /// </summary>
  /// <typeparam name="ArrayType">Type of the data.</typeparam>
  [Serializable]
  public class JsonArrayWrapper<ArrayType> {

    /// <summary>
    /// The data that should be wrapped.
    /// </summary>
    public ArrayType array = default;
  }
}