using DNS.Client.RequestResolver;
using DNS.Protocol;
using DNS.Protocol.ResourceRecords;
using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace IPDNS;

internal class IPStyleDomainRequestResolver : IRequestResolver
{
    private static Regex _ipPattern = new Regex(@"^(\d+-\d+-\d+-\d+)\.");

    public Task<IResponse> Resolve(IRequest request, CancellationToken cancellationToken = default)
    {
        IResponse response = Response.FromRequest(request);

        foreach (var question in response.Questions)
        {
            if (question.Type == RecordType.A)
            {
                var match = _ipPattern.Match(question.Name.ToString());
                if (match.Success && IPAddress.TryParse(match.Groups[1].Value.Replace("-", "."), out var ipAddress))
                {
                    response.AnswerRecords.Add(new IPAddressResourceRecord(question.Name, ipAddress, ttl: TimeSpan.FromDays(3650)));
                }
            }
        }

        return Task.FromResult(response);
    }
}