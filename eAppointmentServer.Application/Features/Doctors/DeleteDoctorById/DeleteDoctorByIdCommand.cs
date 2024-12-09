using MediatR;
using System.Runtime.InteropServices.Marshalling;
using TS.Result;

namespace eAppointmentServer.Application.Features.Doctors.DeleteDoctorById;

public sealed record DeleteDoctorByIdCommand(Guid Id) : IRequest<Result<string>>;
