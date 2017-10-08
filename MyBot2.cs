// Discord Client with C# created by Ignacio Guillermo Martínez Rincón, on the Summer of 2017. University of Carlos III de Madrid, Spain

using Discord;
using Discord.Commands;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscoBot
{
    class MyBot2
    {
        DiscordClient discord;
        CommandService commands;

        string[] spacerand;

        public MyBot2()
        {
            discord = new DiscordClient(x =>
            {
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });


            discord.UsingCommands(x =>
            {
                x.PrefixChar = '-';
                x.AllowMentionPrefix = true;
            });

            commands = discord.GetService<CommandService>();

            sayHello();
            spamRandom();
            picturePublish();
            picturePublishRand();
            messagePurger10();
            messagePurger100();
            messagePurger1000();
            commandPublish();
            onServerJoin();
            onServerLeave();
            spamEris();

            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect("MzExMDkzNDU2ODAwMDU1MzA2.DBhejw.d7C2H5uk9oRWwsEPa25p6V0wTQ8", TokenType.Bot);
            });
        }
        private void sayHello()
        {
            commands.CreateCommand("jhello")
                .Do(async (e) =>
                {
                     await e.Channel.SendMessage("Hello");
                });
        }
        private void spamRandom()
        {
            commands.CreateCommand("jspam")
                .Do(async (e) =>
                 {
                    Random rnd = new Random();
                    int sleepVal;
                    int sendVal;
                    for (;;)
                    {
                        sleepVal = rnd.Next(650, 7500);
                        sendVal = rnd.Next(0, 999999999);
                        await e.Channel.SendMessage(sendVal.ToString());
                        System.Threading.Thread.Sleep(sleepVal); // Wait for random amount of time between 650 milliseconds and 7.5 seconds
                    }
                });
        }
        private void spamEris()
        {
            commands.CreateCommand("jeris")
                .Do(async (e) =>
                {
                    Random rnd = new Random();
                    int sleepVal;
                    int sendVal;
                    for (;;)
                    {
                        sleepVal = rnd.Next(60000, 70000);
                        sendVal = rnd.Next(0, 999999999);
                        await e.Channel.SendMessage(sendVal.ToString());
                        System.Threading.Thread.Sleep(sleepVal); // Wait for random amount of time between 650 milliseconds and 7.5 seconds
                    }
                });
        }
        private void commandPublish()
        {
            commands.CreateCommand("commands")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("Mis comandos son: -jhello, -jspace, -jspacerand, y estoy trabajando en: -jdel10, -jdel100, -jdel1000.");
                    await e.Channel.SendMessage("Mi creador es jasper, y funciono en los servidores: 'Tavern' y 'LCK Official Server / EL EXILIO'");
                });
            commands.CreateCommand("help")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("Para más información de mis comandos, escribe -commands");
                });
        }
        private void picturePublish()
        {
            commands.CreateCommand("jspace")
                .Do(async (e) =>
                {
                    await e.Channel.SendFile("Pictures/pic1.jpg");
                    System.Threading.Thread.Sleep(1000);
                    await e.Channel.SendFile("Pictures/pic2.jpg");
                    System.Threading.Thread.Sleep(1000);
                    await e.Channel.SendFile("Pictures/pic3.jpg");
                });
        }
        private void picturePublishRand()
        {
            commands.CreateCommand("jspacerand")
                .Do(async (e) =>
                {
                    spacerand = new string[]
                    {
                        "Pictures/pic1.jpg",
                        "Pictures/pic2.jpg",
                        "Pictures/pic3.jpg"
                    };

                    Random rand = new Random();
                    int index = rand.Next(spacerand.Length);
                    string picToPost = spacerand[index];
                    await e.Channel.SendFile(picToPost);
                    System.Threading.Thread.Sleep(1000);
                    await e.Channel.SendFile("Pictures/pic2.jpg");
                    System.Threading.Thread.Sleep(1000);
                    await e.Channel.SendFile("Pictures/pic3.jpg");
                });
        }
        private void messagePurger10()
        {
            commands.CreateCommand("jdel10")
                .Do(async (e) =>
                {
                    Message[] targets;
                    targets = await e.Channel.DownloadMessages(10);
                    await e.Channel.DeleteMessages(targets);
                    await e.Channel.SendMessage("Borrados 10 mensajes");
                });
        }
        private void messagePurger100()
        {
            commands.CreateCommand("jdel100")
             .Do(async (e) =>
                 {
                    Message[] targets;
                    targets = await e.Channel.DownloadMessages(100);
                     await e.Channel.DeleteMessages(targets);
                     await e.Channel.SendMessage("Borrados 100 mensajes");
                 });
        }
        private void messagePurger1000()
        {
            commands.CreateCommand("jdel1000")
             .Do(async (e) =>
             {
                 Message[] targets;
                 for (int aux = 0; aux < 10; aux++)
                 {
                     targets = await e.Channel.DownloadMessages(100);
                     await e.Channel.DeleteMessages(targets);
                     await e.Channel.SendMessage("Esperando 20 segundos para borrar los próximos 100 mensajes");
                     System.Threading.Thread.Sleep(30000); // Waits 20 seconds, does it again.
                 }
                 await e.Channel.SendMessage("Borrados 1000 mensajes");
             });
        }
        /*
        private void messagePurgerArgs() // Does an approximate, safe deletion of X messages.
        {
            commands.CreateCommand("jdelc").Parameter("param", ParameterType.Required)
                .Do(async (e) =>
                {
                    int valToDelete;
                    bool works = int.TryParse(e.Args[0], out valToDelete);
                    if (works)
                    {
                        valToDelete = System.Convert.ToInt32(e.Args[0]);
                        if (valToDelete > 100)
                        {
                            Message[] targets;
                            int x = valToDelete / 100;
                            for (int aux = 0; aux < x; aux++)
                            {
                                targets = await e.Channel.DownloadMessages(100);
                                await e.Channel.DeleteMessages(targets);
                                System.Threading.Thread.Sleep(30000); // Waits 30 seconds, does it again.
                            }

                        }
                        else
                        {
                            Message[] targets;
                            targets = await e.Channel.DownloadMessages(valToDelete);
                            await e.Channel.DeleteMessages(targets);
                        }
                    }
                    else
                    {
                        await e.Channel.SendMessage("Sorry, the number you entered is invalid");
                    }
                });
        }
        */

        private void onServerJoin()
        {
            discord.UserJoined += async (s, e) =>
            {
                var channel = e.Server.FindChannels("general", ChannelType.Text).FirstOrDefault();
                var user = e.User;
                await channel.SendMessage(string.Format("{0} se ha unido al server", user.Name));
            };
        }
        private void onServerLeave()
        {
            discord.UserLeft += async (s, e) =>
            {
                var channel = e.Server.FindChannels("general", ChannelType.Text).FirstOrDefault();
                var user = e.User;          
                await channel.SendMessage(string.Format("{0} se ha ido del server", user.Name));
            };
        }
        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

    }
}
