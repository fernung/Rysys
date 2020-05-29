using Rysys.Client;
using System;

namespace Rysys.Clients.DirectX
{
#if WINDOWS || LINUX
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new BaseClient(new SandboxState()))
                game.Run();
        }
    }
#endif
}
