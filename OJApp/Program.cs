﻿using OrderedJson.Code;
using OrderedJson.Core;
using OrderedJson.Parser;
using OrderedJson.Tokenize;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OJApp
{
    class Program
    {
        private const string Filename = "hello.oj";
        static void Main(string[] args)
        {
            //TestMethod1();
            TestMethod2();
        }

        public static void TestMethod2()
        {
            string buffer = File.ReadAllText(Filename);
            var parser = new OJParser(typeof(CardCommondDefiner));
            IOJMethod method = parser.Parse(buffer, Filename);
            GameContext context = new GameContext();
            context.hostCard = new Card() { health = 10 };
            DateTime time = DateTime.Now;
            for (int i = 0; i < 1; i++)
            {
                method.Invoke(context);
            }
            Console.WriteLine($"{DateTime.Now - time}");
        }

        public static void TestMethod1()
        {
            string buffer = File.ReadAllText(Filename);
            var tokens = DoTokenize.Tokenize(buffer, Filename);
            DoTokenize.PrintTokens(tokens);

            var data = ParseDefiner.ParseClass(typeof(CardCommondDefiner));
            TokenParser parser = new TokenParser(data);
            parser.Compile(tokens);

            GameContext context = new GameContext();
            context.hostCard = new Card() { health = 5 };

            Console.WriteLine($"TurnStart: {context.hostCard.TurnStart}");
            Console.WriteLine($"blocks: {parser.blocks.Count}");
            Block block = parser.blocks.First();
            foreach (var item in parser.blocks)
            {
                item.Invoke(context);
            }
            //block.Invoke(context);
            Console.WriteLine($"TurnStart: {context.hostCard.TurnStart}");
            DateTime time = DateTime.Now;
            for (int i = 0; i < 1; i++)
            {
                context.hostCard.TurnStart.Invoke(context);
            }
            Console.WriteLine($"{DateTime.Now - time}");
            Console.WriteLine($"context: {context.hostCard.health}");

        }
    }
}
