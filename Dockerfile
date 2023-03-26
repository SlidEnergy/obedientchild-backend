FROM mcr.microsoft.com/dotnet/runtime-deps:6.0-alpine-amd64

EXPOSE 8080

RUN mkdir /app
WORKDIR /app
COPY ./linux64_musl/. ./

RUN chmod +x ./ObedientChild.WebApi
CMD ["./ObedientChild.WebApi", "--urls", "http://0.0.0.0:8080"]