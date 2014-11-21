using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperbEdit.Base;
using System.IO;

namespace SuperbEdit.Tests.Base
{
    /// <summary>
    /// Summary description for KeybindingsTest
    /// </summary>
    [TestClass]
    public class KeybindingsTests
    {
        public KeybindingsTests()
        {
            KeybindingConfig.config = new Config(Path.Combine(Folders.ProgramFolder, "key_bindings.json"),
                Path.Combine(Folders.ProgramFolder, "key_bindings_user.json"));
        }

        [TestMethod]
        public void OverwrittenKeyBinding()
        {
            Assert.AreEqual("Ctrl+K", KeybindingConfig.RetrieveKeyBind("File.New"));
        }

        [TestMethod]
        public void NotOverwrittenKeyBinding()
        {
            Assert.AreEqual("Ctrl+O", KeybindingConfig.RetrieveKeyBind("File.Open"));
        }


        [TestMethod]
        public void UserOnlyKeybinding()
        {
            Assert.AreEqual("Ctrl+X", KeybindingConfig.RetrieveKeyBind("Edit.Cut"));
        }

        [TestMethod]
        public void DefaultOnlyKeybinding()
        {
            Assert.AreEqual("Ctrl+Shift+P", KeybindingConfig.RetrieveKeyBind("View.CommandWindow"));
        }
    }
}
