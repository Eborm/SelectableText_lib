from SelectableText_lib import SelectableText_lib as ST_lib

st = ST_lib()

def testFunc():
    print("Test function 1")
    input()

def testFunc2():
    print("Test function 2")
    input()
    
st.addText("run this [func1]", testFunc)
st.addText("run this [func2]", testFunc2)
st.addText("", st.voidfunct)

st.setShownText([0, 2, 1])

while True:
    st.displayText()