namespace dotnetwhois
{
    using System;

    public partial class Card
    {
        public override string ToString()
        {
            var contents = $"{this.DisplayName} / {this.Username}";
            maxInternalWidth = contents.Length;

            var output = BuildTop + Environment.NewLine;
            output += BuildBlank + Environment.NewLine;
            output += BuildLineWithContents(contents) + Environment.NewLine;
            output += BuildBlank + Environment.NewLine;
            output += BuildBottom + Environment.NewLine;

            return output;
        }

        private const char TL = '┌';
        private const char TR = '┐';
        private const char BL = '└';
        private const char BR = '┘';
        private const char FP = '─';
        private const char WP = '│';

        private string BuildLineWithContents(string contents)
        {
            var padding = maxInternalWidth - contents.Length;
            var padLeft = (int)((float)padding / 2f);
            var padRight = padding - padLeft;
            return WP + new string(' ',padLeft+1) + contents + new string(' ', padRight+1) + WP;
        }

        private string BuildTop => BuildLineWithoutContent(TL,TR,FP);
        private string BuildBottom => BuildLineWithoutContent(BL,BR,FP);
        private string BuildBlank => BuildLineWithoutContent(WP,WP,' ');

        private string BuildLineWithoutContent(char L, char R, char F)
        {
            return L + new String(F,maxInternalWidth + 2) + R;
        }

        private int maxInternalWidth;
    }
}