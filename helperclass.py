import msvcrt

class detection():
    def detect_up_down() -> int:
        while True:
            if msvcrt.kbhit():
                key = msvcrt.getch().decode('utf-8', errors='ignore')
                
                match key:
                    case "P":
                        return -1
                    case "H":
                        return 1
                    case "\r":
                        return 0
                
    def detect_left_right() -> int:
        if msvcrt.kbhit():
            key = msvcrt.getch().decode('utf-8', errors='ignore')
            match key:
                case "M":
                    return 1
                case "K":
                    return -1
                case "\r":
                    return 0