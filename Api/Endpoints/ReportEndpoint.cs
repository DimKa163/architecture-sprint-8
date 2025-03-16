using System.Security.Claims;
using Api.Application.Requests;
using Api.Contracts;
using MediatR;

namespace Api.Endpoints;

public static class ReportEndpoint
{
    public static IEndpointRouteBuilder MapReports(this IEndpointRouteBuilder endpoints)
    {
        var reports = endpoints.MapGroup("reports")
            .WithTags("Reports").RequireAuthorization(op =>
            {
                op.RequireRole("administrator", "prothetic_user");
            });

        reports.MapGet("", GetReportListHandlerAsync);
        return endpoints;
    }

    private static async Task<IResult> GetReportListHandlerAsync(ISender sender, CancellationToken cancellationToken)
    {
        return Results.Ok(await sender.Send(new DownloadReportRequest(), cancellationToken));
    }
}