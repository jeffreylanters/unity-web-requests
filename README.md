<div align="center">

<img src="https://raw.githubusercontent.com/jeffreylanters/unity-web-requests/master/.github/WIKI/repository-readme-splash.png" width="100%">

</br>
</br>

[![openupm](https://img.shields.io/npm/v/nl.jeffreylanters.web-requests?label=UPM&registry_uri=https://package.openupm.com&style=for-the-badge&color=232c37)](https://openupm.com/packages/nl.jeffreylanters.web-requests/)
[![](https://img.shields.io/github/stars/jeffreylanters/unity-web-requests.svg?style=for-the-badge)]()
[![](https://img.shields.io/badge/build-passing-brightgreen.svg?style=for-the-badge)]()

The WebRequest API provides an interface for accessing and manipulating parts of the HTTP pipeline by implementing a Request model that provides an easy, logical way to fetch resources asynchronously across the network.

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

## Example Usage

```csharp
public class MyGameObject : MonoBehaviour {
  public async void Start () {
    var users = await WebRequest.Send<User>("https://my.api.com/users");
    var user = await WebRequest.Send<User>("https://my.api.com/users") {
      method = RequestMethod.Post,
      payload = new User() {
        userName = "Jeffrey",
        age = 27
      }
    };
  }
}
```
