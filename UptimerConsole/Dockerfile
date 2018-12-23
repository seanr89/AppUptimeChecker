FROM microsoft/dotnet:2.2-sdk AS build-env
WORKDIR /app

LABEL key="Sean Rafferty <srafferty89@gmail.com>"

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM microsoft/dotnet:2.2-runtime AS runtime
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "UptimerConsole.dll", "arg1", "arg2"]
CMD ["arg1", "arg2"]