<div align="center">

<img src="https://raw.githubusercontent.com/jeffreylanters/unity-web-requests/master/.github/WIKI/repository-readme-splash.png" width="100%">

</br>
</br>

[![openupm](https://img.shields.io/npm/v/nl.jeffreylanters.web-requests?label=UPM&registry_uri=https://package.openupm.com&style=for-the-badge&color=232c37)](https://openupm.com/packages/nl.jeffreylanters.web-requests/)
[![](https://img.shields.io/github/stars/jeffreylanters/unity-web-requests.svg?style=for-the-badge)]()
[![](https://img.shields.io/badge/build-passing-brightgreen.svg?style=for-the-badge)]()

The WebRequest API provides an interface for accessing the HTTP pipeline by implementing a Request model that provides an easy, await-able and extendable way to fetch resources asynchronously across the network.

**&Lt;**
[**Installation**](#installation) &middot;
[**Documentation**](#documentation) &middot;
[**License**](./LICENSE.md)
**&Gt;**

</br></br>

[![npm](https://img.shields.io/badge/fund_this_project-sponsor-E12C9A.svg?style=for-the-badge)](https://github.com/sponsors/jeffreylanters)

Hi! My name is Jeffrey Lanters, thanks for checking out my modules! I've been a Unity developer for years when in 2020 I decided to start sharing my modules by open-sourcing them. So feel free to look around. If you're using this module for production, please consider donating to support the project. When using any of the packages, please make sure to **Star** this repository and give credit to **Jeffrey Lanters** somewhere in your app or game. Also keep in mind **it it prohibited to sublicense and/or sell copies of the Software in stores such as the Unity Asset Store!** Thanks for your time.

**&Lt;**
**Made with &hearts; by Jeffrey Lanters**
**&Gt;**

</br>

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

The Web Request API provides a Dot Net for Unity interface for accessing and manipulating parts of the HTTP pipeline, such as requests and responses. It also provides a global WebRequest model that provides an easy, logical way to fetch resources asynchronously across the network.

A basic fetch request is really simple to set up. Have a look at the following code:

```csharp
var username = await new WebRequest<string> ("https://myapi.com/username").Send ();
```

Here we are fetching plain text across the network. The use of the WebRequest model takes one argument — the path to the resource you want to fetch — and returns a task containing the response of the generic response data type.

The response will be based on two factors, the web request will attempt to cast the raw response data into the generic response type based on the response's content type. See [supported content types](#supported-content-types) for more information about request and response content types.

## Making request with different Methods

HTTP defines a set of request methods to indicate the desired action to be performed for a given resource. Although they can also be nouns, these request methods are sometimes referred to as HTTP verbs. Each of them implements a different semantic, but some common features are shared by a group of them: e.g. a request method can be safe, idempotent, or cacheable.

Changing the request method can be done during the initialisation of the web request. Have a look at the following code:

```csharp
var token = await new WebRequest<string> ("https://myapi.com/authentication") {
  method = RequestMethod.Post
}.Send ();
```

## Sending data with the request

Depending on your server's configuration, various request methods will allow a body property which can contain post data.

### Sending plain text

Sending plain text with your web request can be done by assigning the body property, this could be any primitive type such as a string or number. Have a look at the following code:

```csharp
await new WebRequest ("https://myapi.com/authentication") {
  method = RequestMethod.Post,
  body = "r14e77nF09NIy1LE"
}.Send ();
```

### Sending JSON

Have a look at the following code:

```csharp
[Serializable]
public class AuthenticationRequestModel {
  public string username;
  public string password;
}

await new WebRequest ("https://myapi.com/authentication") {
  method = RequestMethod.Post,
  contentType = ContentType.ApplicationJson,
  body = new AuthenticationRequestModel () {
    username = "admin",
    password = "r14e77nF09NIy1LE"
  },
}.Send ();
```
