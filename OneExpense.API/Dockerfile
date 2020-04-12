#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["OneExpense.API/OneExpense.API.csproj", "OneExpense.API/"]
COPY ["OneExpense.Data/OneExpense.Data.csproj", "OneExpense.Data/"]
COPY ["OneExpense.Business/OneExpense.Business.csproj", "OneExpense.Business/"]
RUN dotnet restore "OneExpense.API/OneExpense.API.csproj"
COPY . .
WORKDIR "/src/OneExpense.API"
RUN dotnet build "OneExpense.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OneExpense.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OneExpense.API.dll"]