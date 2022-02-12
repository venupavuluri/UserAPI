namespace UserAPI.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICassandraService
    {
        public Cassandra.ISession GetSession();
    }
}
