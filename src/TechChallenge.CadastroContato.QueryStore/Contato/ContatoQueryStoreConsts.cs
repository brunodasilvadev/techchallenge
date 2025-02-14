namespace TechChallenge.CadastroContato.QueryStore.Contato
{
    internal static class ContatoQueryStoreConsts
    {
        internal const string PESQUISAR_CONTATOS = @"
            SELECT ID Id,
                   NMUSUARIO Nome,
                   NRDDD + NRTELEFONE AS NumeroTelefone,
                   DSEMAIL Email
            FROM CONTATO
            WHERE NRDDD = @pr_nrddd OR @pr_nrddd IS NULL;
        ";
    }
}