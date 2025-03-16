using Api.Contracts;
using MediatR;

namespace Api.Application.Requests;

public record DownloadReportRequest() : IRequest<List<Report>>;