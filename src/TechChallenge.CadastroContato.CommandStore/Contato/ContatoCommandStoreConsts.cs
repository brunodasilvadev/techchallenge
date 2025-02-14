namespace TechChallenge.CadastroContato.CommandStore.Contato
{
    internal static class ContatoCommandStoreConsts
    {
        internal const string INCLUIR_CONTATO = @"
            INSERT INTO [CONTATO]
                (NMUSUARIO, NRDDD, NRTELEFONE, DSEMAIL)
            VALUES
                (@pr_nome, @pr_ddd, @pr_telefone, @pr_email);

            SELECT CAST(scope_identity() AS INT)
        ";

        internal const string ALTERAR_CONTATO = @"
            UPDATE [CONTATO] SET
	            NMUSUARIO = @pr_nome,
	            NRDDD = @pr_ddd,
	            NRTELEFONE = @pr_telefone,
	            DSEMAIL = @pr_email
            WHERE ID = @pr_idcontato;
        ";

        internal const string CONTATO_EXISTE = @"
            SELECT
                TOP 1 1
                FROM [CONTATO]
            WHERE ID = @pr_idcontato;
        ";

        internal const string DELETAR_CONTATO = @"
            DELETE FROM [CONTATO] WHERE ID = @pr_id;

            SELECT CAST(scope_identity() AS INT)
        ";
    }
}