using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using productionCalculatorLib.components.entityContainer;
using productionCalculatorLib.components.worksheet;

namespace SiteReact.Data.DbContexts;

public class DocumentContext
{
    public MongoClient DbClient { get; }
    public IMongoDatabase Database { get; }
    public IMongoCollection<Worksheet> Worksheets { get; }
    public IMongoCollection<EntityContainer> EntityContainers { get; }

    public DocumentContext(string connectionString)
    {
        var camelCaseConvention = new ConventionPack { new CamelCaseElementNameConvention() };
        ConventionRegistry.Register("CamelCase", camelCaseConvention, t => true);

        BsonClassMap.RegisterClassMap<Worksheet>(w =>
        {
            w.AutoMap();
        });
        
        BsonClassMap.RegisterClassMap<EntityContainer>(ec =>
        {
            ec.AutoMap();
        });
        
        DbClient = new MongoClient(connectionString);
        Database = DbClient.GetDatabase("production_calculator");
        
        EntityContainers = Database.GetCollection<EntityContainer>("entityContainer");

        Worksheets = Database.GetCollection<Worksheet>("worksheet");
        
    }
}