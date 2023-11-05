using DNS.Server;
using IPDNS;
using System;

var server = new DnsServer(new IPStyleDomainRequestResolver());
if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("IPDNS_DEBUG")))
{
    server.Requested += (s, e) => Console.WriteLine($"Request for: {e.Request}");
    server.Responded += (s, e) => Console.WriteLine($"Response: {e.Request} -> {e.Response}");
}
server.Errored += (s, e) => Console.Error.WriteLine(e.Exception);

Console.WriteLine("IPDNS is listening on 5533");
await server.Listen(5533);
