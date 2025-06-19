using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Patients.Application.Interfaces;
using Patients.Application.Models;
using Patients.Domain.Enums;

namespace Patients.Application.Patients
{
    public sealed class UpdatePatientCommand : PatientModel, IRequest<Models.PatientModel>
    {

    }

    public sealed class UpdatePatientCommandValidator : AbstractValidator<UpdatePatientCommand>
    {
        public UpdatePatientCommandValidator()
        {
            RuleFor(t => t.Name).NotEmpty();
            RuleFor(t => t.Name.Id).NotEmpty();
            RuleFor(t => t.Name.Family).NotEmpty();
        }
    }

    public sealed class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand, Models.PatientModel>
    {
        private readonly IApplicationDbContext db;
        private readonly IMapper mapper;

        public UpdatePatientCommandHandler(
            IApplicationDbContext db,
            IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<Models.PatientModel> Handle(UpdatePatientCommand command, CancellationToken cancellationToken)
        {
            var patient = await db.Patients.FirstOrDefaultAsync(t => t.Id == command.Name.Id, cancellationToken)
                ?? throw new Exception("Entity not found");

            patient.Family = command.Name.Family;
            patient.Given = command.Name.Given;
            patient.Gender = command.Gender ?? Gender.Unknown;
            patient.BirthDate = command.BirthDate;
            patient.Active = command.Active;
            patient.Use = command.Name.Use;

            await db.SaveChangesAsync(cancellationToken);

            return mapper.Map<Models.PatientModel>(patient);
        }
    }
}