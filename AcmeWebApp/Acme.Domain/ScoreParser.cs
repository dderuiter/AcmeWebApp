using System;
using System.Collections.Generic;

namespace Acme.Domain
{
    public class ScoreParser
    {
        /// <summary>
        /// Parses the bowling game score String.
        /// </summary>
        /// <returns>The scores for each roll.</returns>
        /// <param name="scoreStr">The bowling game score String.</param>
        public List<int> ParseScore(string scoreStr)
        {
            List<int> result = new List<int>();

            foreach(char curChar in scoreStr)
            {
                if(Char.IsNumber(curChar))
                {
                    int num = (int)Char.GetNumericValue(curChar);
                    result.Add(num);
                }
                else if(curChar == '-')
                {
                    result.Add(0);
                }
                else if(curChar == '/')
                {
                    int previousNum = result[result.Count - 1];
                    result.Add(10 - previousNum);
                }
                else if(curChar == 'X')
                {
                    result.Add(10);
                }
            }

            return result;
        }

        /// <summary>
        /// Calculates the frame scores (by looping through 10 frames).
        /// </summary>
        /// <returns>The frame scores.</returns>
        /// <param name="scores">List of roll scores.</param>
        public int[] CalcFrameScores(List<int> scores)
        {
            int curRoll = 0;
            int[] frameScores = new int[10];
            
            for(int frame = 0; frame < 10; frame++)
            {
                if (scores[curRoll] == 10) // Strike
                {
                    int strikeBonus = scores[curRoll + 1] + scores[curRoll + 2];
                    frameScores[frame] = 10 + strikeBonus;
                    curRoll++;
                }
                else if(scores[curRoll] + scores[curRoll + 1] == 10) // Spare
                {
                    int spareBonus = scores[curRoll + 2];
                    frameScores[frame] = 10 + spareBonus;
                    curRoll += 2;
                }
                else // 1-9
                {
                    // Only ever 2 roles in frame
                    frameScores[frame] = scores[curRoll] + scores[curRoll + 1];
                    curRoll += 2;
                }

                // Check if frame is NOT 1st
                if (frame > 0)
                {
                    // Add last frame score to current frame's score
                    int lastFrameScore = frameScores[frame - 1];
                    frameScores[frame] += lastFrameScore;
                }
            }

            return frameScores;
        }

        /// <summary>
        /// Calculates the frame scores (by looping through each roll).
        /// </summary>
        /// <returns>The frame scores.</returns>
        /// <param name="scores">List of roll scores.</param>
        public int[] CalcFrameScores2(List<int> scores)
        {
            int frame = 0;
            int[] frameScores = new int[10];

            for (int curRoll = 0; curRoll < scores.Count; frame++)
            {
                if(frame == 10) {
                    return frameScores;
                }

                if (scores[curRoll] == 10) // Strike
                {
                    int strikeBonus = scores[curRoll + 1] + scores[curRoll + 2];
                    frameScores[frame] = 10 + strikeBonus;
                    curRoll++;
                }
                else if (scores[curRoll] + scores[curRoll + 1] == 10) // Spare
                {
                    int spareBonus = scores[curRoll + 2];
                    frameScores[frame] = 10 + spareBonus;
                    curRoll += 2;
                }
                else // 1-9
                {
                    // Only ever 2 roles in frame
                    frameScores[frame] = scores[curRoll] + scores[curRoll + 1];
                    curRoll += 2;
                }

                // Check if frame is NOT 1st
                if (frame > 0)
                {
                    // Add last frame score to current frame's score
                    int lastFrameScore = frameScores[frame - 1];
                    frameScores[frame] += lastFrameScore;
                }
            }

            return frameScores;
        }
    }
}
