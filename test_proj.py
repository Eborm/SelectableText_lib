from SelectableText_lib import SelectableText_lib as ST_lib

st = ST_lib()

def testFunc():
    print("Test function 1")
    input()

def testFunc2():
    print("Test function 2")
    input()

def testFunc3():
    print("Test function 3")
    input()
    
st.addText("run this [func1]", testFunc)
st.addText("run this [func2]", testFunc2)
st.addText("run this [func3]", testFunc3)
st.addText("")

st.setShownText([0, 1, 2, 3])

while True:
    st.displayText()