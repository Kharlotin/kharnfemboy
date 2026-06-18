using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace docapi
{
    public partial class FormValidation : Form
    {
        public FormValidation()
        {
            InitializeComponent();
        }

        private async void buttonGet_Click(object sender, EventArgs e)
        {
            string url = "http://localhost:4444/TransferSimulator/fullName";
            string responseData = await SendGetRequestAsync(url);
            responseData = responseData.Substring(15);
            responseData = responseData.Substring(0, responseData.Length - 3);
            labelText.Text = responseData;
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            string str;
            if (Regex.IsMatch(labelText.Text, @"[#/\$%=|!?*]"))
            {
                str = "ФИО содержит запрещённые символы";
            }
            else
            {
                str = "ФИО не содержит запрещённые символы";
            }
            labelResult.Text = str;
            InsertIntoWordDocument(@"C:\Users\Laudanum\Desktop\demo\bin\Debug\doc\ТестКейс.docx", "СпецСимвол1", str);
        }

        private async Task<string> SendGetRequestAsync(string url)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string result = await response.Content.ReadAsStringAsync();
                    return result;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка запроса: {ex.Message}");
                    return null;
                }
            }
        }

        private void InsertIntoWordDocument(string filePath, string bookmarkName, string text)
        {
            Word.Application wordApp = null;
            Word.Document doc = null;
            try
            {
                wordApp = new Word.Application();
                wordApp.Visible = false;
                doc = wordApp.Documents.Open(filePath);

                if (doc.Bookmarks.Exists(bookmarkName))
                {
                    Word.Bookmark bm = doc.Bookmarks[bookmarkName];
                    bm.Range.Text = text;
                    doc.Bookmarks.Add(bookmarkName, bm.Range);
                }
                else
                {
                    MessageBox.Show($"Закладка '{bookmarkName}' не найдена в документе.");
                }

                doc.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка работы с Word: {ex.Message}");
            }
            finally
            {
                if (doc != null) doc.Close();
                if (wordApp != null) wordApp.Quit();

                if (doc != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(doc);
                if (wordApp != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApp);
            }
        }
    }
}
