<h1><img src="https://raw.githubusercontent.com/jeffreylanters/unity-web-requests/master/.github/WIKI/repository-readme-splash.png" width="100%"></h1>

<div align="center">

[![openupm](https://img.shields.io/npm/v/nl.jeffreylanters.web-requests?label=UPM&registry_uri=https://package.openupm.com&style=for-the-badge&color=232c37)](https://openupm.com/packages/nl.jeffreylanters.web-requests/)
[![](https://img.shields.io/github/stars/jeffreylanters/unity-web-requests.svg?style=for-the-badge)]()
[![](https://img.shields.io/badge/build-passing-brightgreen.svg?style=for-the-badge)]()

The WebRequest API provides an easy interface for accessing the HTTP pipeline by implementing a Request model that provides an easy, await-able and extendable way to fetch resources asynchronously across the network.

**&Lt;**
[**Installation**](#installation) &middot;
[**Documentation**](#documentation) &middot;
[**License**](./LICENSE.md)
**&Gt;**

</br></br>

[![sponsors](https://img.shields.io/github/sponsors/jeffreylanters?color=E12C9A&label=sponsor%20this%20project&style=for-the-badge)](https://github.com/sponsors/jeffreylanters)

Hi! My name is Jeffrey Lanters, thanks for checking out my modules! I've been a Unity developer for years when in 2020 I decided to start sharing my modules by open-sourcing them. So feel free to look around. If you're using this module for production, please consider donating to support the project. When using any of the packages, please make sure to **Star** this repository and give credit to **Jeffrey Lanters** somewhere in your app or game. Also keep in mind **it it prohibited to sublicense and/or sell copies of the Software in stores such as the Unity Asset Store!** Thanks for your time.

**&Lt;**
**Made with &hearts; by Jeffrey Lanters**
**&Gt;**

</br></br>

</div>

# Installation

### Using the Unity Package Manager

Install the latest stable release using the Unity Package Manager by adding the following line to your `manifest.json` file located within your project's Packages directory, or by adding the Git URL to the Package Manager Window inside of Unity.

```json
"nl.jeffreylanters.web-requests": "git+https://github.com/jeffreylanters/unity-web-requests"
```

### Using OpenUPM

The module is availble on the OpenUPM package registry, you can install the latest stable release using the OpenUPM Package manager's Command Line Tool using the following command.

```sh
openupm add nl.jeffreylanters.web-requests
```

# Documentation

The Web Request module provides a Dot Net for Unity interface for accessing and manipulating parts of the HTTP pipeline, such as requests and responses. It also provides a global WebRequest model that provides an easy, logical way to fetch resources asynchronously across the network. The module delivers the following key features:

- Working out-of-the-box, support for HTTPS and working on all platforms
- No callback-, coroutine- or action-hell thanks to full Async / Await support
- Built on top of the existing Unity Web Request framework
- Full support for sending and receiving JSON data including arrays
- Inspired on the worlds most populair Fetch API used in browsers

A basic Web Request is really simple to set up. Have a look at the following code:

```csharp
var response = await new WebRequest ("https://example.com/resource").Send ();
```

Here we are fetching text across the network. The simplest use of a Web Request takes one argument, the URL to the resource you want to fetch. This is typically an absolute URL with the host component. When building for WebGL, If the URL has the host of another site, the request is performed in accordance to CORS.

This request returns a Task containing the response (a Response object). This is just an HTTP response, not the actual text or JSON. To extract the Text or JSON content from the response, we use the `Response.Text ()` method, or the `Response.Json ()` method accordingly. When extracting the content as a JSON object, top level array types are supported and will be wrapped automatically in order to be parsed by Unity.

When breaking it down, have a look at the following code:

```csharp
var request = new WebRequest ("https://example.com/resource");
var response = await request.Send ();
var text = response.Text ();
var model = response.Json<DataType> ();
```

## Catching Request and HTTP Errors

When the Web Request ran into a problem while fetching the data from the server, a Web Request Exception will be thrown. This Exception can easily be caught using a try catch closure. The Web Request's Exception contains useful information such as the HTTP Status Code and Typed HTTP Status which can be matched against the build in enum of standardised HTTP Status Codes.

Catching errors can be done very easily and multiple Web Requests can be made within the try closure. Have a look at the following code:

```csharp
try {
  var response = await new WebRequest ("https://example.com/resource").Send ();
} catch (WebRequestException exception) {
  Debug.Log ($"Error while getting data from {exception.url}");
  if (exception.httpStatus == HttpStatus.Unauthorized) {
    Debug.Log ("Not authorized!");
  }
}
```

## Making request with different Methods

HTTP defines a set of request methods to indicate the desired action to be performed for a given resource. Although they can also be nouns, these request methods are sometimes referred to as HTTP verbs. Each of them implements a different semantic, but some common features are shared by a group of them: e.g. a request method can be safe, idempotent, or cacheable.

Changing the request method can be done during the initialisation of the web request. Have a look at the following code:

```csharp
var request = new WebRequest ("https://example.com/resource") {
  method = RequestMethod.Post
};
```

## Sending data with the request

Depending on your server's configuration, various request methods will allow a body property which can contain post data. This can vary from sending plain text as the request's body to creating actual form data.

### Sending plain text

Sending plain text with your web request can be done by assigning the body property, this could be any type such as a primitive string or number or any class which will be stringified right before sending. Have a look at the following code:

```csharp
var response = await new WebRequest ("https://example.com/resource") {
  method = RequestMethod.Post,
  body = "Hello, World!"
}.Send ();
```

### Sending JSON data

The body property holds any data you want to send as part of your HTTP (or API) request. Depending on the endpoint, this data may be sent as a JSON object or a query string. Some APIs allow both types, while some require just one or the other. API requests are sent with headers that include information about the request.

When sending data with a Web Request, you will need to specify the Content-type, which tells the API if the data you sent is JSON or a query string. This is another property you can pass into the options with your Web Request. To send data as a JSON object, use the built-in JsonUtility method to convert your data into a string. For your headers Content Type use ApplicationJson as the value.

Have a look at the following code:

> Note that objects that should be able to be parsed and stringified from and into JSON do require to be Serializable as shown below in the example.

```csharp
var response = await new WebRequest ("https://example.com/resource") {
  method = RequestMethod.Post,
  contentType = ContentType.ApplicationJson,
  body = JsonUtility.ToJson (new User () {
    firstName = "John",
    lastName = "Doe"
  })
}.Send ();

[Serializable]
public class User {
  public string firstName;
  public string lastName;
}
```

### Sending Multipart Form Data

Unity Web Requests comes with a custom Form Data Utility allowing for data to be represented as raw Form Data.

The body property holds any data you want to send as part of your HTTP (or API) request. Depending on the endpoint, this data may be sent as Multipart Form Data or a query string. Some APIs allow both types, while some require just one or the other. API requests are sent with headers that include information about the request.

When sending data with a Web Request, you will need to specify the Content-type, which tells the API if the data you sent is Multipart Form Data or a query string. This is another property you can pass into the options with your Web Request. To send data as a Multipart Form Data, use the custom FormDataUtility method to convert your data into raw Form Data. For your headers Content Type use MultipartFormData as the value.

Have a look at the following code:

> Note that objects that should be able to be parsed and stringified from and into Form Data do require to be Serializable as shown below in the example.

```csharp
var response = await new WebRequest ("https://example.com/resource") {
  method = RequestMethod.Post,
  contentType = ContentType.MultipartFormData,
  body = FormDataUtility.ToFormData (new User () {
    firstName = "John",
    lastName = "Doe"
  })
}.Send ();

[Serializable]
public class User {
  public string firstName;
  public string lastName;
}
```

## Adding custom headers to the request

The Header interface of the Web Request module allows you to add specific header values with your HTTP requests. A Web Request consists of a list of Header objects, which is initially empty and can consist of zero or more name and value pairs. You can add when instantiating a new Web Request (see Examples.) In all methods of this model, header names are matched by case-insensitive byte sequence.

For security reasons, some headers can only be controlled by the user agent. These headers include the forbidden header names and forbidden response header names.

```cs
var request = new WebRequest ("https://example.com/resource") {
  headers = new Header[] {
    new Header("Authorization", "some-very-secret-token"),
    new Header("Content-Type", "Custom/ContentType"),
  }
};
```
