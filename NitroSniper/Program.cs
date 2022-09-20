using System;
using System.Text.RegularExpressions;
using Discord;
using Discord.Gateway;

namespace NitroSniper
{
    class Program
    {
        static void Main(string[] args)
        {
            string sniper_token = "";

            DiscordSocketClient client = new DiscordSocketClient();
            client.OnLoggedIn += Client_OnLoggedIn;
            client.OnMessageReceived += Client_OnMessageReceived;
            client.Login(sniper_token);

            Console.ReadLine();
        }

        private static void Client_OnMessageReceived(DiscordSocketClient client, MessageEventArgs args)
        {
            const string giftPrefix = "discord.gift/";

            var match = Regex.Match(args.Message.Content, giftPrefix + ".{16,24}");

            if (match.Success)
            {
                string code = match.Value.Substring(match.Value.IndexOf(giftPrefix) + giftPrefix.Length);

                try
                {
                    string your_token = "";

                    DiscordClient your_client = new DiscordClient();
                    your_client.Token = your_token;
                    your_client.RedeemGift(code);

                    Console.WriteLine("Successfully redeemed code " + code);
                }
                catch (DiscordHttpException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static void Client_OnLoggedIn(DiscordSocketClient client, LoginEventArgs args)
        {
            Console.WriteLine("Logged in");
        }
    }
}
