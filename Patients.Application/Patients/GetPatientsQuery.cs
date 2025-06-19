using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Patients.Application.Interfaces;

namespace Patients.Application.Patients
{
    public sealed class GetPatientsQuery : IRequest<IReadOnlyList<Models.PatientModel>>
    {
        public string[] Filters { get; set; } = Array.Empty<string>();

        public int? Limit { get; set; }

        public int? Offset { get; set; }
    }

    public sealed class GetPatientsQueryHandler : IRequestHandler<GetPatientsQuery, IReadOnlyList<Models.PatientModel>>
    {
        private readonly IApplicationDbContext db;
        private readonly IMapper mapper;

        public GetPatientsQueryHandler(
            IApplicationDbContext db,
            IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<IReadOnlyList<Models.PatientModel>> Handle(GetPatientsQuery request, CancellationToken cancellationToken)
        {
            var query = db.Patients.AsQueryable();

            if (request.Filters.Any())
            {
                // TODO birthDate filters https://www.hl7.org/fhir/search.html#date
                foreach (var filter in request.Filters)
                {
                    var rule = filter[..2];
                    var timestamp = DateTime.Parse(filter[2..]);

                    query = rule switch
                    {
                        "eq" => query.Where(t => t.BirthDate == timestamp),
                        "ap" => query.Where(t => t.BirthDate == timestamp),
                        "ne" => query.Where(t => t.BirthDate != timestamp),
                        "le" => query.Where(t => t.BirthDate <= timestamp),
                        "lt" => query.Where(t => t.BirthDate < timestamp),
                        "eb" => query.Where(t => t.BirthDate < timestamp),
                        "ge" => query.Where(t => t.BirthDate >= timestamp),
                        "gt" => query.Where(t => t.BirthDate > timestamp),
                        "sa" => query.Where(t => t.BirthDate > timestamp),
                        _ => throw new Exception($"Unknown filter: {filter}")
                    };
                }
            }

            if (request.Limit.HasValue && request.Offset.HasValue)
            {
                query = query
                    .Skip(request.Offset.Value)
                    .Take(request.Limit.Value);
            }

            var patients = await query
                .AsNoTracking()
                .ToArrayAsync(cancellationToken) ;

            return mapper.Map<IReadOnlyList<Models.PatientModel>>(patients);
        }
    }
}
