using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

namespace TubeWriter2;

class Extractor {

    private string date;
    private bool retestFlag;
    private int start = 1;
    private int rowCount = 0;

    //Set up excel sheet to be filled
    public Extractor(TubeWriter tubeWriter)
    {
        IWorkbook workbook = new XSSFWorkbook();
        ISheet sheet = workbook.CreateSheet("Tubes Sheet");

        start = tubeWriter.startBox.Text != "" ? Convert.ToInt32(tubeWriter.startBox.Text) : 1;

        for (int i = 0; i < tubeWriter.dateCount; i++) {

            date = tubeWriter.entryBoxes[i, 0].Text;
            retestFlag = tubeWriter.checkBoxes[i].Checked;

            if (date == "") {
                break;
            }

            while (date.Length < 4) {
                date = " " + date;
            }

            for (int j = 1; j < 5; j++) {
                if (!Fill(ref sheet, tubeWriter.entryBoxes[i, j])) {
                    return;
                }
            }
        }

        //Replace and open Tubes.xlsx
        FileHandler fileHandler = new FileHandler(workbook);

        //The compiler gets mad at me if I dont set date to something so you know what:
        date = "Big Chungus";
        //There, you happy now compiler? No? Good.
    }

    //Adds tubes in EntryBox range
    private bool Fill(ref ISheet sheet, EntryBox entryBox)
    {
        if (string.IsNullOrEmpty(entryBox.Text)) {
            return true;
        }

        List<int> list = Range2Ints(entryBox.Text);
        List<int> errorlist = new List<int>(){-1};

        if (list.SequenceEqual(errorlist)) {
            return false;
        }

        foreach (int number in list) {

            IRow row = sheet.CreateRow(rowCount);

            //Incrementing column
            ICell countCell = row.CreateCell(0);
            countCell.SetCellValue(start);

            //Character column, adds an 'R' if retest checkbox is checked
            ICell charCell = row.CreateCell(1);
            charCell.SetCellValue(entryBox.GetChars(retestFlag));

            //Number column
            string numberStr = Convert.ToString(number);
            if (number < 1000) {
                numberStr = ' ' + numberStr;
            }
            ICell numberCell = row.CreateCell(2);
            numberCell.SetCellValue(numberStr);

            //Date column
            ICell dateCell = row.CreateCell(3);
            dateCell.SetCellValue(date);

            rowCount++;
            start++;
        }

        return true;
    }

    //Converts entrybox range into list of ints
    private List<int> Range2Ints(string str)
    {
        char[] delimChars = new char[] { ',', '+' };
        List<int> result = new List<int>();

        try {
            foreach (var subStr in str.Split(delimChars)) {

                if (subStr.Contains('-')) {

                    int lower = Convert.ToInt32(subStr.Split('-')[0]);
                    int upper = Convert.ToInt32(subStr.Split('-')[1]);

                    //Check for invalid range entry
                    if (lower > upper) {
                        MessageBoxButtons buttons = MessageBoxButtons.OK;
                        DialogResult message = MessageBox.Show("Invalid Range", "Error", buttons);
                        break;
                    }

                    //Add range to final list
                    var range = Enumerable.Range(lower, (upper+1)-lower);
                    result.AddRange(range);
                }
                else {
                    int number = Convert.ToInt32(subStr);
                    result.Add(number);
                }
            }
        }catch(System.FormatException) {
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult message = MessageBox.Show("Invalid character in entry box", "Error", buttons);
            result = new List<int>(){-1};
        }

        return result;
    }
}