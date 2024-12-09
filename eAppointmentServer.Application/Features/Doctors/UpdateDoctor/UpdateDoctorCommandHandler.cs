using AutoMapper;
using eAppointmentServer.Application.Features.Doctors.UpdateDoctor;
using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

internal sealed class UpdateDoctorCommandHandler(
    IDoctorRepository doctorRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<UpdateDoctorCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
    {
        Doctor? doctor = await doctorRepository.GetByExpressionAsync(p=>p.Id == request.Id, cancellationToken);

        if(doctor is null)
        {
            return Result<string>.Failure("doctor not found");
        }

        mapper.Map(request,doctor);

        doctorRepository.Update(doctor);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Doctor update is succesfull";
    }
}

