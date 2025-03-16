using Api.Application.Requests;
using Api.Contracts;
using MediatR;

namespace Api.Application.RequestHandlers;

public class DownloadReportRequestHandler : IRequestHandler<DownloadReportRequest, List<Report>>
{
    public Task<List<Report>> Handle(DownloadReportRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new List<Report>()
        {
            new Report()
            {
                Title = RandomString(12),
                Begin = DateTime.UtcNow.AddDays(-7),
                End = DateTime.UtcNow
            }
        });
    }
    
    private static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[Random.Shared.Next(s.Length)]).ToArray());
    }
}