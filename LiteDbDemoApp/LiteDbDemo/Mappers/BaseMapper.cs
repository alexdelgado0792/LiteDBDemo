using LiteDB;

namespace LiteDbDemo.Mappers
{
    public abstract class BaseMapper
    {
        protected BsonMapper mapper = BsonMapper.Global;
    }
}
