using AutoMapper;
using FluentValidation;
using MediatR;
using Patients.Application.Interfaces;
using Patients.Application.Models;
using Patients.Domain.Entities;
using Patients.Domain.Enums;

namespace Patients.Application.Patients
{
    public sealed class CreatePatientCommand : PatientModel, IRequest<Models.PatientModel>
    {

    }

    public sealed class CreatePatientCommandValidator : AbstractValidator<CreatePatientCommand>
    {
        public CreatePatientCommandValidator()
        {
            RuleFor(t => t.Name).NotEmpty();
            RuleFor(t => t.Name.Family).NotEmpty();
            RuleFor(t => t.BirthDate).NotEmpty();
        }
    }

    public sealed class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, Models.PatientModel>
    {
        private readonly IApplicationDbContext db;
        private readonly IMapper mapper;

        public CreatePatientCommandHandler(
            IApplicationDbContext db,
            IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<Models.PatientModel> Handle(CreatePatientCommand command, CancellationToken cancellationToken)
        {
            var patient = new Patient
            {
                Id = Guid.NewGuid(),
                Family = command.Name.Family,
                Given = command.Name.Given,
                Gender = command.Gender ?? Gender.Unknown,
                BirthDate = command.BirthDate,
                Active = command.Active,
                Use = command.Name.Use
            };

            db.Patients.Add(patient);
            await db.SaveChangesAsync(cancellationToken);

            return mapper.Map<Models.PatientModel>(patient);
        }
    }
}