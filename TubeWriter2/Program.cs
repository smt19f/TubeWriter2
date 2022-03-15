namespace TubeWriter2;

static class Program {
    
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new TubeWriter());
    }
}
