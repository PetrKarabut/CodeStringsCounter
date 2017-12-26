using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeStringsCounter
{
    public class CSharpStateMachine : IStateMachineInit
    {
        //состояния
        public const string normalState = "normal";
        public const string oneQuoteState = "oneQuote";
        public const string doubleQuoteState = "doubleQuote";
        public const string smallCommentState = "smallComment";
        public const string bigCommentState = "bigComment";

        //ключевые слова, при этом значения строк default и newLine не важны
        public const string defaultWord = "default";
        public const string newLineWord = "newLine";
        public const string oneQuoteWord = "'";
        public const string doubleQuoteWord = "\"";
        public const string openSmallCommentWord = "//";
        public const string openBigCommentWord = "/*";
        public const string closeBigCommentWord = "*/";


        public StateMachineStructure GetMachineStructure()
        {
            var machine = new StateMachineStructure()
            {

                States = new List<string> { normalState, oneQuoteState, doubleQuoteState, smallCommentState, bigCommentState },
                Words = new List<string> { defaultWord, newLineWord, oneQuoteWord, doubleQuoteWord, openSmallCommentWord, openBigCommentWord, closeBigCommentWord },
                StartStateIndex = 0,
                DefaultWordIndex = 0,
                NewLineWordIndex = 1,
                Transitions = new List<Transition>()
            };

            //переходы
            machine.AddTransition(normalState, defaultWord, normalState, true);
            machine.AddTransition(normalState, oneQuoteWord, oneQuoteState, false);
            machine.AddTransition(normalState, doubleQuoteWord, doubleQuoteState, false);
            machine.AddTransition(normalState, openSmallCommentWord, smallCommentState, false);
            machine.AddTransition(normalState, openBigCommentWord, bigCommentState, false);

            machine.AddTransition(oneQuoteState, oneQuoteWord, normalState, false);
            machine.AddTransition(doubleQuoteState, doubleQuoteWord, normalState, false);
            machine.AddTransition(smallCommentState, newLineWord, normalState, false);
            machine.AddTransition(bigCommentState, closeBigCommentWord, normalState, false);


            return machine;
        }
    }
}
