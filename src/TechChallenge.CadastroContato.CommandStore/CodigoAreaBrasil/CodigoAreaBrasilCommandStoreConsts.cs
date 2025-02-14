namespace TechChallenge.CadastroContato.CommandStore.CodigoAreaBrasil
{
    internal static class CodigoAreaBrasilCommandStoreConsts
    {
        public const string CODIGO_AREA_EXISTE =
            @"SELECT
                TOP 1 1
                FROM CODIGO_AREA_BRASIL
            WHERE DDD = @pr_ddd";
    }
}