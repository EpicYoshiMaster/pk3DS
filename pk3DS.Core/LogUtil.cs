using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace pk3DS.Core
{

    public class LogUtil
    {
        private string fileName = string.Empty;

        private string indentation = string.Empty;

        private int indentationLevel = 0;
        public LogUtil(string aFileName, string aIndentation = "\t")
        {
            fileName = aFileName;

            if (File.Exists(aFileName))
            {
                File.Delete(aFileName);
            }

            this.indentation = aIndentation;
        }

        public void PushIndentationLevel()
        {
            indentationLevel += 1;
        }

        public void PopIndentationLevel()
        {
            indentationLevel = (indentationLevel - 1 < 0) ? 0 : indentationLevel - 1;
        }

        public void WriteManual(params string[] logMessage)
        {
            if(logMessage.Length > 0)
            {
                logMessage[0] = "[Manual]: " + logMessage[0];
            }

            Write(logMessage);
        }

        public void WriteWarning(params string[] logMessage)
        {
            if (logMessage.Length > 0)
            {
                logMessage[0] = "[WARNING]: " + logMessage[0];
            }

            Write(logMessage);
        }

        public void WriteError(params string[] logMessage)
        {
            if (logMessage.Length > 0)
            {
                logMessage[0] = "[ERROR]: " + logMessage[0];
            }

            Write(logMessage);
        }

        public void Write(params string[] logMessage)
        {
            try
            {
                using (StreamWriter w = File.AppendText(fileName))
                {
                    Log(logMessage, w);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Caught Exception: " + ex.Message);
            }
        }

        private void Log(string[] logMessage, TextWriter txtWriter)
        {
            try
            {
                string indentString = "";
                for (int i = 0; i < indentationLevel; i++)
                {
                    indentString += indentation;
                }

                for (int i = 0; i < logMessage.Length; i++)
                {
                    txtWriter.Write(indentString + logMessage[i] + Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Caught Exception: " + ex.Message);
            }
        }
    }
}
