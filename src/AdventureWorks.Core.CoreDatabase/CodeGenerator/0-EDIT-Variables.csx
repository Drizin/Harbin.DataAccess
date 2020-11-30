// JSON file (relative to the CSX scripts)
string jsonSchemaPath = @"..\CoreDbSchema.json";

// Target folder for generated Entities (relative to the CSX scripts)
string targetFolderPath = @"..\..\AdventureWorks.Core.Domain\Entities\";

string entitiesNamespace = "AdventureWorks.Core.Domain.Entities";
string crudClassName = "CoreDbReadWriteConnection";
string crudClassNamespace = "AdventureWorks.Core.CoreDatabase";

// Must be relative to the targetFolderPath
string crudClassFile = @"..\..\AdventureWorks.Core.CoreDatabase\CoreDbReadWriteConnection.CRUD.generated.cs";

// Connection String
string connectionString = @"Data Source=LENOVOFLEX5;
                            Initial Catalog=AdventureWorks;
                            Integrated Security=True;";
