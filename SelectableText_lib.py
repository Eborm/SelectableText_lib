from helperclass import detection
import os
class SelectableText_lib:
    startup : str = "          _____                _____                    _____            _____                    _____          \n         /\    \              /\    \                  /\    \          /\    \                  /\    \         \n        /::\    \            /::\    \                /::\____\        /::\    \                /::\    \        \n       /::::\    \           \:::\    \              /:::/    /        \:::\    \              /::::\    \       \n      /::::::\    \           \:::\    \            /:::/    /          \:::\    \            /::::::\    \      \n     /:::/\:::\    \           \:::\    \          /:::/    /            \:::\    \          /:::/\:::\    \     \n    /:::/__\:::\    \           \:::\    \        /:::/    /              \:::\    \        /:::/__\:::\    \    \n    \:::\   \:::\    \          /::::\    \      /:::/    /               /::::\    \      /::::\   \:::\    \    \n  ___\:::\   \:::\    \        /::::::\    \    /:::/    /       ____    /::::::\    \    /::::::\   \:::\    \   \n /\   \:::\   \:::\    \      /:::/\:::\    \  /:::/    /       /\   \  /:::/\:::\    \  /:::/\:::\   \:::\ ___\  \n/::\   \:::\   \:::\____\    /:::/  \:::\____\/:::/____/       /::\   \/:::/  \:::\____\/:::/__\:::\   \:::|    | \n\:::\   \:::\   \::/    /   /:::/    \::/    /\:::\    \       \:::\  /:::/    \::/    /\:::\   \:::\  /:::|____| \n \:::\   \:::\   \/____/   /:::/    / \/____/  \:::\    \       \:::\/:::/    / \/____/  \:::\   \:::\/:::/    /  \n  \:::\   \:::\    \      /:::/    /            \:::\    \       \::::::/    /            \:::\   \::::::/    /   \n   \:::\   \:::\____\    /:::/    /              \:::\    \       \::::/____/              \:::\   \::::/    /    \n    \:::\  /:::/    /    \::/    /                \:::\    \       \:::\    \               \:::\  /:::/    /     \n     \:::\/:::/    /      \/____/                  \:::\    \       \:::\    \               \:::\/:::/    /      \n      \::::::/    /                                 \:::\    \       \:::\    \               \::::::/    /       \n       \::::/    /                                   \:::\____\       \:::\____\               \::::/    /        \n        \::/    /                                     \::/    /        \::/    /                \::/    /         \n         \/____/                                       \/____/          \/____/                  \/____/               \n                                                                                                                 "
    
    _TextDictonary = {}
    SelectedText : int = -1
    TextCount : int = -1
    WriteThisText : list = [0]
    SelectableText : list = [0]
    
    def voidfunct():
        return None
    
    def addText(self, text: str, functionname = None):
        if (functionname==None):
            functionname=self.voidfunct
        self.TextCount += 1
        self._TextDictonary[self.TextCount] = (text, functionname)
    
    def setShownText(self, textindices : list):
        self.WriteThisText = textindices
        if (self.WriteThisText.count() == 0):
            self.WriteThisText = [0]
        for key in self.WriteThisText:
            if (self._TextDictonary[key][1] != self.voidfunct):
                self.SelectableText.append(key)
    
    def displayText(self):
        self.SelectedText = min(max(self.SelectedText, self.WriteThisText[0]), self.WriteThisText[self.WriteThisText.count()-1])
        os.system('cls')
        for TextKey in self.WriteThisText:
            if TextKey == self.SelectedText:
                pass
            else:
                print(self._TextDictonary[TextKey][0])
        Input : int = detection.detect_up_down()
        
        match Input:
            case -1:
                self.SelectedText -= 1
                while self.SelectableText.__contains__(self.SelectedText) == False:
                    self.SelectedText -= 1
            case 1:
                self.SelectedText += 1
                while self.SelectableText.__contains__(self.SelectedText) == False:
                    self.SelectedText += 1
            case 0:
                if (self._TextDictonary[self.SelectedText][1] != self.voidfunct):
                    self._TextDictonary[self.SelectedText][1]()
    
    def writeText(self, Text : str):
        SplitText : list = list(Text)
        PrintThisText : str = ""
        for c in SplitText:
            if c == "[":
                PrintThisText += "\x1b[5m\x1b[7m"
            elif c == "]":
                PrintThisText += "\x1b[25m\x1b[27m"
            else:
                PrintThisText += c
        print(PrintThisText)