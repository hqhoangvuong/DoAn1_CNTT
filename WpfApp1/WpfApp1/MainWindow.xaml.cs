﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static List<string> tags = new List<string>();
        static List<char> specials = new List<char>();
        static string text;

        static MainWindow()
        {
            string[] specialWords = { "string", "char", "null", "import" };
            tags = new List<string>(specialWords);
            // We also want to know all possible delimiters so adding this stuff.     
            char[] chrs = {
            '.',
            ')',
            '(',
            '[',
            ']',
            '>',
            '<',
            ':',
            ';',
            '\n',
            '\t',
            '\r'
        };
            specials = new List<char>(chrs);
        }

        public static bool IsKnownTag(string tag)
        {
            return tags.Exists(delegate (string s) { return s.ToLower().Equals(tag.ToLower()); });
        }

        private static bool GetSpecials(char i)
        {
            foreach (var item in specials)
            {
                if (item.Equals(i))
                {
                    return true;
                }
            }
            return false;
        }

        new struct Tag
        {
            public TextPointer StartPosition;
            public TextPointer EndPosition;
            public string Word;
        }

        List<Tag> m_tags = new List<Tag>();
        internal void CheckWordsInRun(Run theRun) //do not hightlight keywords in this method
        {
            //How, let's go through our text and save all tags we have to save.               
            int sIndex = 0;
            int eIndex = 0;

            for (int i = 0; i < text.Length; i++)
            {
                if (Char.IsWhiteSpace(text[i]) | GetSpecials(text[i]))
                {
                    if (i > 0 && !(Char.IsWhiteSpace(text[i - 1]) | GetSpecials(text[i - 1])))
                    {
                        eIndex = i - 1;
                        string word = text.Substring(sIndex, eIndex - sIndex + 1);
                        if (IsKnownTag(word))
                        {
                            Tag t = new Tag();
                            t.StartPosition = theRun.ContentStart.GetPositionAtOffset(sIndex, LogicalDirection.Forward);
                            t.EndPosition = theRun.ContentStart.GetPositionAtOffset(eIndex + 1, LogicalDirection.Backward);
                            t.Word = word;
                            m_tags.Add(t);
                        }
                    }
                    sIndex = i + 1;
                }
            }

            //How this works. But wait. If the word is last word in my text I'll never hightlight it, due I'm looking for separators. Let's add some fix for this case
            string lastWord = text.Substring(sIndex, text.Length - sIndex);
            if (IsKnownTag(lastWord))
            {
                Tag t = new Tag();
                t.StartPosition = theRun.ContentStart.GetPositionAtOffset(sIndex, LogicalDirection.Forward);
                t.EndPosition = theRun.ContentStart.GetPositionAtOffset(text.Length, LogicalDirection.Backward); //fix 1
                t.Word = lastWord;
                m_tags.Add(t);
            }
        }

        private void txtStatus_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtStatus.Document == null)
                return;
            txtStatus.TextChanged -= txtStatus_TextChanged;

            m_tags.Clear();

            //first clear all the formats
            TextRange documentRange = new TextRange(txtStatus.Document.ContentStart, txtStatus.Document.ContentEnd);
            documentRange.ClearAllProperties();
            //text = documentRange.Text; //fix 2

            //Now let's create navigator to go though the text, find all the keywords but do not hightlight
            TextPointer navigator = txtStatus.Document.ContentStart;
            while (navigator.CompareTo(txtStatus.Document.ContentEnd) < 0)
            {
                TextPointerContext context = navigator.GetPointerContext(LogicalDirection.Backward);
                if (context == TextPointerContext.ElementStart && navigator.Parent is Run)
                {
                    text = ((Run)navigator.Parent).Text; //fix 2
                    if (text != "")
                        CheckWordsInRun((Run)navigator.Parent);
                }
                navigator = navigator.GetNextContextPosition(LogicalDirection.Forward);
            }

            //only after all keywords are found, then we highlight them
            for (int i = 0; i < m_tags.Count; i++)
            {
                try
                {
                    TextRange range = new TextRange(m_tags[i].StartPosition, m_tags[i].EndPosition);
                    range.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Blue));
                    range.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
                }
                catch { }
            }
            txtStatus.TextChanged += txtStatus_TextChanged;
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
