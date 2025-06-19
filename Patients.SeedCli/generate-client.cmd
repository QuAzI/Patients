docker run --rm -v "./Patients:/local/src/Patients" openapitools/openapi-generator-cli generate -i http://host.docker.internal:8000/swagger/v1/swagger.json -g csharp --additional-properties packageName=Patients -o /local

@pause