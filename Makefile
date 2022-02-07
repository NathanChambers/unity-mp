make:
	@echo server-build;\
	echo server-run;

server-build:
	@dotnet build -o ./Builds/Server -c Release ./Server.csproj

server-run:
	@./Builds/Server/Server.exe