using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Patients.Application.Interfaces;
using Patients.Application.Models;

namespace Patients.Application.Patients
{
    public sealed class GetPatientByIdQuery : IRequest<PatientModel>
    {
        public Guid Id { get; init; }
    }

    public sealed class GetPatientByIdQueryValidator : AbstractValidator<GetPatientByIdQuery>
    {
        public GetPatientByIdQueryValidator()
        {
            RuleFor(t => t.Id).NotEmpty();
        }
    }

    public sealed class GetPatientByIdQueryHandler : IRequestHandler<GetPatientByIdQuery, Models.PatientModel>
    {
        private readonly IApplicationDbContext db;
        private readonly IMapper mapper;

        public GetPatientByIdQueryHandler(IApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<PatientModel> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
        {
            var patient = await db.Patients.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

            return mapper.Map<Models.PatientModel>(patient);
        }
    }
}