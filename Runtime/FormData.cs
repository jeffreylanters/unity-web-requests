namespace JeffreyLanters.WebRequests {

  /// <summary>
  /// The form data can be send as a web request body.
  /// </summary>
  public class FormData {

    /// <summary>
    /// The boundary key which will be used to separate the form data.
    /// </summary>
    public static readonly string boundary = "__X_UNITYWEBREQUESTS_BOUNDARY__";

    /// <summary>
    /// The fields of the form data.
    /// </summary>
    public Field[] fields = new Field[0];

    /// <summary>
    /// Instanciates a new FormData object.
    /// </summary>
    public FormData (params Field[] fields) {
      this.fields = fields;
    }

    /// <summary>
    /// Turns the form data into a payload string.
    /// </summary>
    /// <returns>The stringified form data.</returns>
    public override string ToString () {
      var _content = $"--{FormData.boundary}";
      foreach (var _field in this.fields) {
        _content += $"\r\nContent-Disposition: form-data; name=\"{_field.name}\"";
        _content += $"\r\n\r\n{_field.value}";
        _content += $"\r\n--{FormData.boundary}";
      }
      _content += "--";
      return _content;
    }

    /// <summary>
    /// A field in the form data.
    /// </summary>
    public class Field {

      /// <summary>
      /// The name of field.
      /// </summary>
      public string name { get; } = "";

      /// <summary>
      /// The value of the field.
      /// </summary>
      public string value { get; } = "";

      /// <summary>
      /// Creates a new field.
      /// </summary>
      /// <param name="name">The name of the field.</param>
      /// <param name="value">The value of the field.</param>
      public Field (string name, object value) {
        this.name = name;
        this.value = value.ToString ();
      }
    }
  }

}