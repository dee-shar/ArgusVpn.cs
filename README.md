# ArgusVpn.cs
Mobile-API for [Argus Vpn](https://argus-vpn.com/) which is an essential tool when it comes to Internet security. It encrypts your connection so that third parties cannot track your online activities, making it more secure than a typical proxy

## Example
```cs
using System;
using ArgusVpnApi;

namespace Application
{
    internal class Program
    {
        static async Task Main()
        {
            var api = new ArgusVpn();
            await api.Login("example@gmail.com", "password"));
            string servers = await api.GetServers();
            Console.WriteLine(servers);
        }
    }
}
```
