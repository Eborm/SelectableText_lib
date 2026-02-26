using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelectableText_lib.Classes
{
    internal class Detection
    {
        public int DetectUpDown()
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

        public int AdvancedDetection()
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
}
