using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace Validator
{
    public partial class Form1 : Form
    {
        private const string ApiAddress = "http://localhost:4444/TransferSimulator/mobilePhone";
        private const string TestCasePath = @"C:\Users\user\Desktop\демо\Новая папка\6\ТестКейс.docx";

        public Form1()
        {
            InitializeComponent();
        }

        private void getDataButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    string json = client.DownloadString(ApiAddress);
                    phoneLabel.Text = ExtractPhone(json);
                    resultLabel.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось получить данные от эмулятора.\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void sendResultButton_Click(object sender, EventArgs e)
        {
            string phone = phoneLabel.Text.Trim();

            if (string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("Сначала получите данные от эмулятора.", "Нет данных", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool correctFormat = Regex.IsMatch(phone, @"^\+7\s\d{3}\s\d{3}-\d{2}-\d{2}$");
            bool onlyAllowedSymbols = Regex.IsMatch(phone, @"^[0-9+\s-]+$");
            bool isValid = correctFormat && onlyAllowedSymbols;

            string formatResult = correctFormat
                ? "Успешно"
                : "Не успешно: номер не соответствует шаблону +7 900 123-45-67";

            string symbolsResult = onlyAllowedSymbols
                ? "Успешно"
                : "Не успешно: номер содержит недопустимые символы";

            resultLabel.Text = isValid
                ? "Корректный номер телефона"
                : "Не корректный номер телефона";


                WriteResultsToWord(formatResult, symbolsResult);

        }

        private string ExtractPhone(string json)
        {
            Match match = Regex.Match(json, "\"value\"\\s*:\\s*\"(?<phone>[^\"]*)\"");

            if (match.Success)
            {
                return match.Groups["phone"].Value;
            }

            return json;
        }

        private void WriteResultsToWord(string formatResult, string symbolsResult)
        {
            if (!File.Exists(TestCasePath))
            {
                throw new FileNotFoundException("Файл тест-кейса не найден.", TestCasePath);
            }

            Word.Application wordApplication = null;
            Word.Document document = null;

            try
            {
                wordApplication = new Word.Application();
                wordApplication.Visible = false;
                document = wordApplication.Documents.Open(TestCasePath, ReadOnly: false, Visible: false);

                WriteBookmark(document, "СпецСимвол1", formatResult);
                WriteBookmark(document, "СпецСимвол2", symbolsResult);

                document.Save();
            }
            finally
            {
                if (document != null)
                {
                    document.Close();
                    Marshal.ReleaseComObject(document);
                }

                if (wordApplication != null)
                {
                    wordApplication.Quit();
                    Marshal.ReleaseComObject(wordApplication);
                }
            }
        }

        private void WriteBookmark(Word.Document document, string bookmarkName, string text)
        {
            if (!document.Bookmarks.Exists(bookmarkName))
            {
                throw new InvalidOperationException("В документе Word не найдена закладка " + bookmarkName + ".");
            }

            Word.Range range = document.Bookmarks[bookmarkName].Range;
            range.Text = text;
            document.Bookmarks.Add(bookmarkName, range);
            Marshal.ReleaseComObject(range);
        }
    }
}
