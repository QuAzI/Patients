using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Patients.Application.Interfaces;

namespace Patients.Application.Patients
{
    public sealed class DeletePatientCommand : IRequest
    {
        public Guid Id { get; init; }
    }

    public sealed class DeletePatientCommandValidator : AbstractValidator<DeletePatientCommand>
    {
        public DeletePatientCommandValidator()
        {
            RuleFor(t => t.Id).NotEmpty();
        }
    }

    public sealed class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommand>
    {
        private readonly IApplicationDbContext db;
        private readonly IMapper mapper;

        public DeletePatientCommandHandler(
            IApplicationDbContext db,
            IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task Handle(DeletePatientCommand command, CancellationToken cancellationToken)
        {
            var patient = await db.Patients.FirstOrDefaultAsync(t => t.Id == command.Id, cancellationToken);
            if (patient is not null)
            {
                db.Patients.Remove(patient);
                await db.SaveChangesAsync(cancellationToken);
            }
        }
    }
}