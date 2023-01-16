using System.Reflection;
using System;
using System.Collections.Generic;

namespace JeffreyLanters.WebRequests {

  /// <summary>
  /// Utility functions for working with Form data
  /// </summary>
  public class FormDataUtility {

    /// <summary>
    /// The boundary key which will be used to separate the form data.
    /// </summary>
    public static readonly string boundary = "__X_UNITYWEBREQUESTS_BOUNDARY__";

    /// <summary>
    /// Generates a Form Data representation of the public fields and properties
    /// of an object.
    /// </summary>
    /// <param name="targetObject">The target object.</param>
    /// <returns>Raw form data.</returns>
    public static string ToFormData (Object targetObject) {
      var _rawFormData = $"--{FormDataUtility.boundary}";
      var _formDataEntries = new Dictionary<string, string> ();
      var _targetObjectType = targetObject.GetType ();
      // Loop through all the public fields and properties of the target object.
      var _fields = _targetObjectType.GetFields (BindingFlags.Public | BindingFlags.Instance);
      var _properties = _targetObjectType.GetProperties (BindingFlags.Public | BindingFlags.Instance);
      foreach (var _field in _fields) {
        _formDataEntries.Add (_field.Name, _field.GetValue (targetObject).ToString ());
      }
      foreach (var _property in _properties) {
        _formDataEntries.Add (_property.Name, _property.GetValue (targetObject).ToString ());
      }
      // Loop through the entries and add them to the raw form data.
      foreach (var _formDataEntry in _formDataEntries) {
        _rawFormData += $"\r\nContent-Disposition: form-data; name=\"{_formDataEntry.Key}\"";
        _rawFormData += $"\r\n\r\n{_formDataEntry.Value}";
        _rawFormData += $"\r\n--{FormDataUtility.boundary}";
      }
      _rawFormData += "--";
      return _rawFormData;
    }
  }
}