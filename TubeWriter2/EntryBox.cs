namespace TubeWriter2;

public class EntryBox : TextBox {
    public bool shouldRead = false;
    private string printedChars;

    public EntryBox(int index) 
    {
        switch ((index+1) % 5) {
            case 2:
                printedChars = " C";
                break;
            case 3:
                printedChars = " E";
                break;
            case 4:
                printedChars = " A";
                break;
            case 0:
                printedChars = " AS";
                break;
            default:
                printedChars = "ERR";
                break;
        }
    }

    public string GetChars(bool retestFlag)
    {
        return  retestFlag ? printedChars + "R" : printedChars;
    }

}