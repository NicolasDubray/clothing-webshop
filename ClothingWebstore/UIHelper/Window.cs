using System;
using System.Collections.Generic;
using System.Text;

namespace ClothingWebstore.UIHelper
{
    public class Window(string header, int left, int top, List<string> textRows)
    {
        public string Header { get; set; } = header;
        public int Left { get; set; } = left;
        public int Top { get; set; } = top;
        public List<string> TextRows { get; set; } = textRows;

        public void Draw()
        {
            var width = TextRows.OrderByDescending(s => s.Length).FirstOrDefault()!.Length;


            if (width < Header.Length + 4)
            {
                width = Header.Length + 4;
            };

            Console.SetCursorPosition(Left, Top);
            if (Header != "")
            {
                Console.Write('┌' + " ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(Header);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" " + new String('─', width - Header.Length) + '┐');
            }
            else
            {
                Console.Write('┌' + new String('─', width + 2) + '┐');
            }


            for (int j = 0; j < TextRows.Count; j++)
            {
                Console.SetCursorPosition(Left, Top + j + 1);
                Console.WriteLine('│' + " " + TextRows[j] + new String(' ', width - TextRows[j].Length + 1) + '│');
            }


            Console.SetCursorPosition(Left, Top + TextRows.Count + 1);
            Console.Write('└' + new String('─', width + 2) + '┘');



            if (Lowest.LowestPosition < Top + TextRows.Count + 2)
            {
                Lowest.LowestPosition = Top + TextRows.Count + 2;
            }

            Console.SetCursorPosition(0, Lowest.LowestPosition);
        }
    }

    public static class Lowest
    {
        public static int LowestPosition { get; set; }
    }
}

