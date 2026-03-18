using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace UITests
{
    public class StartBlockTests
    {

        //авторизованный пользователь: сашок - абвгде

        //при каждом перезапуске теста Registration нужно изменить LoginForReg
        const string LoginForReg = "сигма123";

        IWebDriver _webDriver = new ChromeDriver();

        [Fact]
        public void CorrectTitle()
        {
            _webDriver.Url = "https://test.webmx.ru/";
            const string Title = "Сервис заметок";
            Assert.Equal(Title, _webDriver.Title);
            _webDriver.Close();
        }

        [Fact]
        public void Auth_IncorrectLogin_ReturnMessage()
        {
            _webDriver.Url = "https://test.webmx.ru/";

            IWebElement usernameInput = _webDriver.FindElement(By.Id("authUsername"));
            IWebElement passwordInput = _webDriver.FindElement(By.Id("authPassword"));
            IWebElement authSubmit = _webDriver.FindElement(By.Id("authSubmit"));

            usernameInput.SendKeys("абв");
            passwordInput.SendKeys("абвгде");

            authSubmit.Click();
            Thread.Sleep(500);
            const string findXPath = "//*[@id=\"message\"]/span";
            IWebElement message = _webDriver.FindElement(By.XPath(findXPath));

            Assert.Equal("Неверный логин или пароль.", message.Text);

            _webDriver.Close();
        }
        [Fact]
        public void Auth_EmptyFields_ReturnMessage()
        {
            _webDriver.Url = "https://test.webmx.ru/";

            IWebElement usernameInput = _webDriver.FindElement(By.Id("authUsername"));
            IWebElement passwordInput = _webDriver.FindElement(By.Id("authPassword"));
            IWebElement authSubmit = _webDriver.FindElement(By.Id("authSubmit"));

            authSubmit.Click();
            Thread.Sleep(500);

            string message = usernameInput.GetAttribute("validationMessage");

            Assert.Equal("Заполните это поле.", message);

            _webDriver.Close();
        }
        [Fact]
        public void Auth_ShortLogin_ReturnMessage()
        {
            _webDriver.Url = "https://test.webmx.ru/";

            IWebElement usernameInput = _webDriver.FindElement(By.Id("authUsername"));
            IWebElement passwordInput = _webDriver.FindElement(By.Id("authPassword"));
            IWebElement authSubmit = _webDriver.FindElement(By.Id("authSubmit"));

            usernameInput.SendKeys("аб");

            authSubmit.Click();
            Thread.Sleep(500);

            string message = usernameInput.GetAttribute("validationMessage");

            Assert.Contains("Минимально допустимое количество символов: 3", message);

            _webDriver.Close();
        }
        [Fact]
        public void Auth_ShortPassword_ReturnMessage()
        {
            _webDriver.Url = "https://test.webmx.ru/";

            IWebElement usernameInput = _webDriver.FindElement(By.Id("authUsername"));
            IWebElement passwordInput = _webDriver.FindElement(By.Id("authPassword"));
            IWebElement authSubmit = _webDriver.FindElement(By.Id("authSubmit"));

            usernameInput.SendKeys("абв");
            passwordInput.SendKeys("абв");

            authSubmit.Click();
            Thread.Sleep(500);

            string message = passwordInput.GetAttribute("validationMessage");

            Assert.Contains("Минимально допустимое количество символов: 6", message);

            _webDriver.Close();
        }
        [Fact]
        public void AuthAndExit()
        {
            _webDriver.Url = "https://test.webmx.ru/";

            IWebElement usernameInput = _webDriver.FindElement(By.Id("authUsername"));
            IWebElement passwordInput = _webDriver.FindElement(By.Id("authPassword"));
            IWebElement authSubmit = _webDriver.FindElement(By.Id("authSubmit"));

            usernameInput.SendKeys("сашок");
            passwordInput.SendKeys("абвгде");

            authSubmit.Click();
            Thread.Sleep(500);

            IWebElement logoutBtn = _webDriver.FindElement(By.Id("logoutBtn"));
            logoutBtn.Click();
            Thread.Sleep(500);

            const string findXPath = "//*[@id=\"message\"]/span";
            IWebElement message = _webDriver.FindElement(By.XPath(findXPath));

            Assert.Equal("Вы вышли из системы.", message.Text);

            _webDriver.Close();
        }
        
        [Fact]
        public void Registration()
        {
            _webDriver.Url = "https://test.webmx.ru/";

            IWebElement registerTab = _webDriver.FindElement(By.Id("registerTab"));
            registerTab.Click();

            IWebElement usernameInput = _webDriver.FindElement(By.Id("authUsername"));
            IWebElement passwordInput = _webDriver.FindElement(By.Id("authPassword"));
            IWebElement authSubmit = _webDriver.FindElement(By.Id("authSubmit"));

            usernameInput.SendKeys(LoginForReg); 
            passwordInput.SendKeys("абвгде");

            authSubmit.Click();
            Thread.Sleep(500);

            const string findXPath = "//*[@id=\"message\"]/span";
            IWebElement message = _webDriver.FindElement(By.XPath(findXPath));

            Assert.Contains("Регистрация успешна", message.Text);

            _webDriver.Close();
        }
        [Fact]
        public void Registration_ExistingUser_ReturnMessage()
        {
            _webDriver.Url = "https://test.webmx.ru/";

            IWebElement registerTab = _webDriver.FindElement(By.Id("registerTab"));
            registerTab.Click();

            IWebElement usernameInput = _webDriver.FindElement(By.Id("authUsername"));
            IWebElement passwordInput = _webDriver.FindElement(By.Id("authPassword"));
            IWebElement authSubmit = _webDriver.FindElement(By.Id("authSubmit"));

            usernameInput.SendKeys("сашок");
            passwordInput.SendKeys("абвгде");

            authSubmit.Click();
            Thread.Sleep(500);

            const string findXPath = "//*[@id=\"message\"]/span";
            IWebElement message = _webDriver.FindElement(By.XPath(findXPath));

            Assert.Equal("Пользователь с таким логином уже существует.", message.Text);

            _webDriver.Close();
        }



        [Fact]
        public void SaveNote_EmptyFields_ReturnMessage()
        {
            _webDriver.Url = "https://test.webmx.ru/";

            IWebElement usernameInput = _webDriver.FindElement(By.Id("authUsername"));
            IWebElement passwordInput = _webDriver.FindElement(By.Id("authPassword"));
            IWebElement authSubmit = _webDriver.FindElement(By.Id("authSubmit"));

            usernameInput.SendKeys("сашок");
            passwordInput.SendKeys("абвгде");

            authSubmit.Click();
            Thread.Sleep(500);

            IWebElement noteTitle = _webDriver.FindElement(By.Id("noteTitle"));
            IWebElement saveBtn = _webDriver.FindElement(By.Id("saveBtn"));
            saveBtn.Click();
            Thread.Sleep(500);

            string message = noteTitle.GetAttribute("validationMessage");
            Assert.Equal("Заполните это поле.", message);

            _webDriver.Close();
        }

        //я считаю, что нет смысла создавать заметку, у которой только название,
        //потому что по факту получается, что ничего и не замечено
        //лично у меня чаще наоборот заметки без названия, но с текстом
        [Fact]
        public void SaveNote_EmptyContent_ReturnMessage()
        {
            _webDriver.Url = "https://test.webmx.ru/";

            IWebElement usernameInput = _webDriver.FindElement(By.Id("authUsername"));
            IWebElement passwordInput = _webDriver.FindElement(By.Id("authPassword"));
            IWebElement authSubmit = _webDriver.FindElement(By.Id("authSubmit"));

            usernameInput.SendKeys("сашок");
            passwordInput.SendKeys("абвгде");

            authSubmit.Click();
            Thread.Sleep(500);

            IWebElement newNoteBtn = _webDriver.FindElement(By.Id("newNoteBtn"));
            IWebElement noteTitle = _webDriver.FindElement(By.Id("noteTitle"));
            IWebElement noteContent = _webDriver.FindElement(By.Id("noteContent"));
            IWebElement saveBtn = _webDriver.FindElement(By.Id("saveBtn"));

            newNoteBtn.Click();
            Thread.Sleep(500);

            noteTitle.SendKeys("список дел");

            saveBtn.Click();
            Thread.Sleep(500);

            string message = noteContent.GetAttribute("validationMessage");
            Assert.Equal("Заполните это поле.", message);

            _webDriver.Close();
        }
        [Fact]
        public void SaveNote_CreatedNote_ReturnMessage()
        {
            _webDriver.Url = "https://test.webmx.ru/";

            IWebElement usernameInput = _webDriver.FindElement(By.Id("authUsername"));
            IWebElement passwordInput = _webDriver.FindElement(By.Id("authPassword"));
            IWebElement authSubmit = _webDriver.FindElement(By.Id("authSubmit"));

            usernameInput.SendKeys("сашок");
            passwordInput.SendKeys("абвгде");

            authSubmit.Click();
            Thread.Sleep(500);

            IWebElement newNoteBtn = _webDriver.FindElement(By.Id("newNoteBtn"));
            IWebElement noteTitle = _webDriver.FindElement(By.Id("noteTitle"));
            IWebElement noteContent = _webDriver.FindElement(By.Id("noteContent"));
            IWebElement saveBtn = _webDriver.FindElement(By.Id("saveBtn"));

            newNoteBtn.Click();

            noteTitle.SendKeys("список дел");
            noteContent.SendKeys("погулять с собакой");

            saveBtn.Click();
            Thread.Sleep(500);

            const string findXPath = "//*[@id=\"message\"]/span";
            IWebElement message = _webDriver.FindElement(By.XPath(findXPath));

            Assert.Equal("Заметка создана.", message.Text);

            _webDriver.Close();
        }
        [Fact]
        public void SaveNote_UpdatedNote_ReturnMessage()
        {
            _webDriver.Url = "https://test.webmx.ru/";

            IWebElement usernameInput = _webDriver.FindElement(By.Id("authUsername"));
            IWebElement passwordInput = _webDriver.FindElement(By.Id("authPassword"));
            IWebElement authSubmit = _webDriver.FindElement(By.Id("authSubmit"));

            usernameInput.SendKeys("сашок");
            passwordInput.SendKeys("абвгде");

            authSubmit.Click();
            Thread.Sleep(500);

            IWebElement saveBtn = _webDriver.FindElement(By.Id("saveBtn"));
            IWebElement noteTitle = _webDriver.FindElement(By.Id("noteTitle"));

            const string noteXPath = "//*[@id=\"notesList\"]/li[1]";
            IWebElement noteLi = _webDriver.FindElement(By.XPath(noteXPath));
            noteLi.Click();
            Thread.Sleep(500);

            noteTitle.SendKeys("123");
            saveBtn.Click();
            Thread.Sleep(500);

            const string findXPath = "//*[@id=\"message\"]/span";
            IWebElement message = _webDriver.FindElement(By.XPath(findXPath));

            Assert.Contains("Заметка обновлена.", message.Text);

            _webDriver.Close();
        }
        [Fact]
        public void ShareNote_ReturnMessage()
        {
            _webDriver.Url = "https://test.webmx.ru/";

            IWebElement usernameInput = _webDriver.FindElement(By.Id("authUsername"));
            IWebElement passwordInput = _webDriver.FindElement(By.Id("authPassword"));
            IWebElement authSubmit = _webDriver.FindElement(By.Id("authSubmit"));

            usernameInput.SendKeys("сашок");
            passwordInput.SendKeys("абвгде");

            authSubmit.Click();
            Thread.Sleep(500);

            IWebElement shareBtn = _webDriver.FindElement(By.Id("shareBtn"));
            IWebElement shareUsername = _webDriver.FindElement(By.Id("shareUsername"));

            const string noteXPath = "//*[@id=\"notesList\"]/li[1]";
            IWebElement noteLi = _webDriver.FindElement(By.XPath(noteXPath));
            noteLi.Click();
            Thread.Sleep(500);

            shareUsername.SendKeys("сигма");
            shareBtn.Click();
            Thread.Sleep(500);

            const string findXPath = "//*[@id=\"message\"]/span";
            IWebElement message = _webDriver.FindElement(By.XPath(findXPath));

            Assert.Equal("Доступ успешно выдан.", message.Text);

            _webDriver.Close();
        }
        [Fact]
        public void CheckNote_MyNotes_ReturnMessage()
        {
            _webDriver.Url = "https://test.webmx.ru/";

            IWebElement usernameInput = _webDriver.FindElement(By.Id("authUsername"));
            IWebElement passwordInput = _webDriver.FindElement(By.Id("authPassword"));
            IWebElement authSubmit = _webDriver.FindElement(By.Id("authSubmit"));

            usernameInput.SendKeys("сигма");
            passwordInput.SendKeys("абвгде");

            authSubmit.Click();
            Thread.Sleep(500);

            IWebElement noteScopeFilter = _webDriver.FindElement(By.Id("noteScopeFilter"));
            noteScopeFilter.Click();
            Thread.Sleep(500);

            const string filterXPath = "//*[@id=\"noteScopeFilter\"]/option[2]";
            IWebElement filterMy = _webDriver.FindElement(By.XPath(filterXPath));
            filterMy.Click();
            Thread.Sleep(500);

            const string noteXPath = "//*[@id=\"notesList\"]/li[1]";
            IWebElement noteLi = _webDriver.FindElement(By.XPath(noteXPath));

            Assert.Contains("Нет заметок", noteLi.Text);

            _webDriver.Close();
        }
        [Fact]
        public void CheckNote_CommonNotes_ReturnMessage()
        {
            _webDriver.Url = "https://test.webmx.ru/";

            IWebElement usernameInput = _webDriver.FindElement(By.Id("authUsername"));
            IWebElement passwordInput = _webDriver.FindElement(By.Id("authPassword"));
            IWebElement authSubmit = _webDriver.FindElement(By.Id("authSubmit"));

            usernameInput.SendKeys("сигма");
            passwordInput.SendKeys("абвгде");

            authSubmit.Click();
            Thread.Sleep(500);

            IWebElement noteScopeFilter = _webDriver.FindElement(By.Id("noteScopeFilter"));
            noteScopeFilter.Click();
            Thread.Sleep(500);

            const string filterXPath = "//*[@id=\"noteScopeFilter\"]/option[3]";
            IWebElement filterMy = _webDriver.FindElement(By.XPath(filterXPath));
            filterMy.Click();
            Thread.Sleep(500);

            const string noteXPath = "//*[@id=\"notesList\"]/li[1]/small";
            IWebElement noteLi = _webDriver.FindElement(By.XPath(noteXPath));

            Assert.Contains("Автор: сашок", noteLi.Text);

            _webDriver.Close();
        }

        [Fact]
        public void DeleteNote_DisableBtn()
        {
            _webDriver.Url = "https://test.webmx.ru/";

            IWebElement usernameInput = _webDriver.FindElement(By.Id("authUsername"));
            IWebElement passwordInput = _webDriver.FindElement(By.Id("authPassword"));
            IWebElement authSubmit = _webDriver.FindElement(By.Id("authSubmit"));

            usernameInput.SendKeys("сашок");
            passwordInput.SendKeys("абвгде");

            authSubmit.Click();
            Thread.Sleep(500);

            IWebElement newNoteBtn = _webDriver.FindElement(By.Id("newNoteBtn"));
            IWebElement deleteBtn = _webDriver.FindElement(By.Id("deleteBtn"));

            newNoteBtn.Click();
            Thread.Sleep(500);

            deleteBtn.Click();
            Thread.Sleep(500);

            Assert.False(deleteBtn.Enabled);

            _webDriver.Close();
        }
        [Fact]
        public void DeleteNote_ReturnMessage()
        {
            _webDriver.Url = "https://test.webmx.ru/";

            IWebElement usernameInput = _webDriver.FindElement(By.Id("authUsername"));
            IWebElement passwordInput = _webDriver.FindElement(By.Id("authPassword"));
            IWebElement authSubmit = _webDriver.FindElement(By.Id("authSubmit"));

            usernameInput.SendKeys("сашок");
            passwordInput.SendKeys("абвгде");

            authSubmit.Click();
            Thread.Sleep(500);

            IWebElement newNoteBtn = _webDriver.FindElement(By.Id("newNoteBtn"));
            IWebElement noteTitle = _webDriver.FindElement(By.Id("noteTitle"));
            IWebElement noteContent = _webDriver.FindElement(By.Id("noteContent"));
            IWebElement saveBtn = _webDriver.FindElement(By.Id("saveBtn"));

            newNoteBtn.Click();

            noteTitle.SendKeys("список дел");
            noteContent.SendKeys("погулять с собакой");

            saveBtn.Click();
            Thread.Sleep(500);

            const string noteXPath = "//*[@id=\"notesList\"]/li[1]";
            IWebElement noteLi = _webDriver.FindElement(By.XPath(noteXPath));
            noteLi.Click();
            Thread.Sleep(500);

            IWebElement deleteBtn = _webDriver.FindElement(By.Id("deleteBtn"));
            deleteBtn.Click();
            Thread.Sleep(500);

            IAlert alert = _webDriver.SwitchTo().Alert();
            Thread.Sleep(500);
            alert.Accept();

            Thread.Sleep(500);
            const string findXPath = "//*[@id=\"message\"]/span";
            IWebElement message = _webDriver.FindElement(By.XPath(findXPath));

            Assert.Contains("Заметка удалена.", message.Text);

            _webDriver.Close();
        }
    }
}