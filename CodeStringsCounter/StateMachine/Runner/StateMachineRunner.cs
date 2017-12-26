using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CodeStringsCounter
{
    public class StateMachineRunner
    {
        private StateMachineStructure machine;
        private int currentState;

        private const int notUsed = -1;
        private int[,] destinations;
        private bool[,] countBoolArray;
        private WordsSet[] statesWords;
        private WordsSet currentWordsSet;

        private bool thisStringCounted = false;
        private int stringsCount = 0;

        public int StringsCount
        {
            get
            {
                return stringsCount;
            }
        }

        public StateMachineRunner(StateMachineStructure machine)
        {
            this.machine = machine;
            currentState = machine.StartStateIndex;
            destinations = new int[machine.States.Count, machine.Words.Count];
            countBoolArray = new bool[machine.States.Count, machine.Words.Count];

            for (int i = 0; i < destinations.GetLength(0); i++)
            {
                for (int j = 0; j < destinations.GetLength(1); j++)
                {
                    destinations[i, j] = notUsed;
                }
            }

            foreach (Transition transition in machine.Transitions)
            {
                destinations[transition.DepartureIndex, transition.WordIndex] = transition.DestinationIndex;
                countBoolArray[transition.DepartureIndex, transition.WordIndex] = transition.CountAfterTransition;
            }

            statesWords = new WordsSet[machine.States.Count];
            for (int i = 0; i < statesWords.Length; i++)
            {
                var list = new List<string>();
                foreach (Transition transition in machine.Transitions)
                {
                    if (transition.DepartureIndex == i && transition.WordIndex != machine.DefaultWordIndex && transition.WordIndex != machine.NewLineWordIndex)
                    {
                        list.Add(machine.Words[transition.WordIndex]);
                    }
                }

                statesWords[i] = new WordsSet(list, machine.Words);
            }

            currentWordsSet = statesWords[machine.StartStateIndex];
        }

        private void Command(int wordIndex)
        {
            if (wordIndex == machine.NewLineWordIndex)
            {
                thisStringCounted = false;
            }

            if (destinations[currentState, wordIndex] == notUsed)
            {
                return;
            }

            if (countBoolArray[currentState, wordIndex] && !thisStringCounted)
            {
                stringsCount++;
                thisStringCounted = true;
            }

            currentState = destinations[currentState, wordIndex];
            currentWordsSet = statesWords[currentState];
        }

        private void RunString(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                bool isDefault = true;
                if (char.IsWhiteSpace(c))
                {
                    continue;
                }

                for (int j = 0; j < currentWordsSet.UsingWords.Count; j++)
                {
                    if (s.IndexOf(currentWordsSet.UsingWords[j], i) == i)
                    {
                        i += currentWordsSet.UsingWords[j].Length;
                        Command(currentWordsSet.WordsIndexes[j]);
                        isDefault = false;
                        break;
                    }
                }

                if (isDefault)
                {
                    Command(machine.DefaultWordIndex);
                }
            }
        }



        public void RunMachine(StreamReader reader)
        {
            if (reader.EndOfStream)
            {
                return;
            }

            while (true)
            {
                RunString(reader.ReadLine());
                if (reader.EndOfStream)
                {
                    break;
                }
                Command(machine.NewLineWordIndex);
            }
        }

    }
}
