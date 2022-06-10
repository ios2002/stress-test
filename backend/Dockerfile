FROM  bitnami/aspnet-core
WORKDIR /app
COPY ./bin/Debug/net6.0/publish/ .
# EXPOSE 5000
CMD ["dotnet","backend.dll"]