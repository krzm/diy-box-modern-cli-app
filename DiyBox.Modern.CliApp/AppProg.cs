using CommandDotNet;
using CommandDotNet.Repl;
using CommandDotNet.Unity.Helper;
using Config.Wrapper;
using Serilog;

namespace DiyBox.Modern.CliApp;

public class AppProg 
    : AppProgUnity<AppProg>
{
	private static bool inSession;

    [Subcommand]
    public BoxCommands? AppCommands { get; set; }

    [Subcommand]
    public SheetCommands? SheetCommands { get; set; }

    public AppProg(
        ILogger log
        , IConfigReader config) 
            : base(log, config)
    {
        MannuallyRegisterCmds = new Type[]
        {
            typeof(BoxCommands)
            , typeof(SheetCommands)
        };
    }

    [DefaultCommand()]
    public void StartSession(
        CommandContext context,
        ReplSession replSession)
    {
        if (inSession == false)
        {
            context.Console.WriteLine("start session");
            inSession = true;
            replSession.Start();
        }
        else
        {
            context.Console.WriteLine($"no session {inSession}");
            context.ShowHelpOnExit = true;
        }
    }
}