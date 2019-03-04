namespace dotnetwhois
{
    using System;
    using System.Linq;

    public partial class Card
    {
        public override string ToString()
        {
            widthsForLabelledLines = GetMaxWidthsOfLabelsAndContents();
            maxInternalWidth = CalcMaxInternalWidth();
            var nameLine = FormattedNames;

            var output = BuildTop + Environment.NewLine;
            output += BuildBlank + Environment.NewLine;
            output += BuildLineWithContents(nameLine) + Environment.NewLine;
            output += BuildLineWithContents(new string('~', nameLine.Length)) + Environment.NewLine;
            output += BuildBlank + Environment.NewLine;
            output += string.Join(Environment.NewLine, CardLines.Select(BuildLineFromCardLine)) + Environment.NewLine;
            output += BuildBlank + Environment.NewLine;
            output += BuildBottom + Environment.NewLine;

            return output;
        }

        private int CalcMaxInternalWidth()
        {
            var nameSectionWidth = FormattedNames.Length;

            var maxWidthOfLabelledLines = widthsForLabelledLines.MaxLabel + 2 + widthsForLabelledLines.MaxContents;

            var maxWidthOfLineContents = this.CardLines.Select(l => l.Contents.Length).Max();

            return Math.Max(nameSectionWidth, Math.Max(maxWidthOfLabelledLines, maxWidthOfLineContents));
        }

        private (int LabelWidth, int ContentsWidth) GetMaxWidthsOfLabelsAndContents()
        {
            var linesWithLabels = this.CardLines.Where(l => !string.IsNullOrWhiteSpace(l.Label));

            if (!linesWithLabels.Any())
            {
                return (0, 0);
            }

            var maxLabel = linesWithLabels.Select(l => l.Label.Length).Max();
            var maxContents = linesWithLabels.Select(l => l.Contents.Length).Max();

            return (maxLabel, maxContents);
        }

        private const char TL = '┌';
        private const char TR = '┐';
        private const char BL = '└';
        private const char BR = '┘';
        private const char FP = '─';
        private const char WP = '│';

        private string BuildLineFromCardLine(CardLine line)
        {
            if (string.IsNullOrWhiteSpace(line.Label))
            {
                return BuildLineWithContents(line.Contents);
            }
            else
            {
                var leftPad = widthsForLabelledLines.MaxLabel - line.Label.Length;
                var rightPad = widthsForLabelledLines.MaxContents - line.Contents.Length;
                var contents = new string(' ', leftPad) + line.Label + ": " + line.Contents + new string(' ', rightPad);
                return BuildLineWithContents(contents);
            }
        }

        private string BuildLineWithContents(string contents)
        {
            var padding = maxInternalWidth - contents.Length;
            var padLeft = (int)((float)padding / 2f);
            var padRight = padding - padLeft;
            return WP + new string(' ', padLeft + 1) + contents + new string(' ', padRight + 1) + WP;
        }

        private string BuildTop => BuildLineWithoutContent(TL, TR, FP);
        private string BuildBottom => BuildLineWithoutContent(BL, BR, FP);
        private string BuildBlank => BuildLineWithoutContent(WP, WP, ' ');

        private string BuildLineWithoutContent(char L, char R, char F)
        {
            return L + new String(F, maxInternalWidth + 2) + R;
        }

        private string FormattedNames => $"{this.DisplayName} / {this.Username}";

        private int maxInternalWidth;
        private (int MaxLabel, int MaxContents) widthsForLabelledLines;
    }
}