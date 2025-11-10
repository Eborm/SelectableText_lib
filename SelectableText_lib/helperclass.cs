using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelectableText_lib_namespace
{
    internal class detection
    {
        public int detect_up_down()
        {
            int direction = 0;
            var key = Console.ReadKey(false).Key;

            switch (key.ToString())
            {
                case "UpArrow":
                    direction = -1;
                    break;

                case "DownArrow":
                    direction = 1;
                    break;

                case "Enter":
                    direction = 2;
                    break;
                default:
                    break;
            }
            return direction;
        }

        public int advanced_detection()
        {
            int direction = 0;
            var key = Console.ReadKey(false).Key;

            switch (key.ToString())
            {
                case "UpArrow":
                    direction = -1;
                    break;

                case "DownArrow":
                    direction = 1;
                    break;

                case "Enter":
                    direction = 3;
                    break;

                case "LeftArrow":
                    direction = -2;
                    break;

                case "RightArrow":
                    direction = 2;
                    break;
                default:
                    break;
            }
            return direction;
        }
    }

    internal class sleepingbeauty
    {
        public void NOP(double durationSeconds)
        {
            var durationTicks = Math.Round(durationSeconds * Stopwatch.Frequency);
            var sw = Stopwatch.StartNew();

            while (sw.ElapsedTicks < durationTicks)
            {

            }
        }
    }
}
