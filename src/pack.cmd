mkdir .\packages-local

dotnet build -c Release Harbin.DataAccess.DapperFastCRUD\Harbin.DataAccess.DapperFastCRUD.csproj
dotnet pack Harbin.DataAccess.DapperFastCRUD\Harbin.DataAccess.DapperFastCRUD.csproj -c Release -p:NuspecFile=Harbin.DataAccess.DapperFastCRUD.nuspec -p:NuspecBasePath=.

dotnet build -c Release Harbin.DataAccess.DapperSimpleCRUD\Harbin.DataAccess.DapperSimpleCRUD.csproj
dotnet pack Harbin.DataAccess.DapperSimpleCRUD\Harbin.DataAccess.DapperSimpleCRUD.csproj -c Release -p:NuspecFile=Harbin.DataAccess.DapperSimpleCRUD.nuspec -p:NuspecBasePath=.

