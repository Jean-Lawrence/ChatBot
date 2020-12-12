# escape=`

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env

WORKDIR C:\src

 

# Copy csproj and restore as distinct layers

COPY ChatBot.sln C:\src
COPY ChatBot C:\src

RUN dotnet restore ChatBot.csproj

RUN dotnet publish  -c release -o ./build_output ChatBot.csproj

 

 

# Build runtime image

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

WORKDIR C:\ChatBot

COPY --from=build-env C:\src\ChatBot\build_output .

ENTRYPOINT ["dotnet", "ChatBot.dll"]
