namespace TaskScheduler.Migrations.V_1_0
{
    [FluentMigrator.Migration(1, "1.0")]
    class V_1_0 : FluentMigrator.Migration
    {
        public override void Up()
        {

        }

        public override void Down()
        {
            Execute.EmbeddedScript("public.sql");
        }
    }
}
