using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VFS.Helpers;

namespace VFS.Application.GUI.Tab
{
    public class Preview : UserControl
    {
        private PageContainer pc = null;

        public Preview(PageContainer pc)
        {
            this.BackColor = Color.Green;
            this.pc = pc;
        }

        public void SelectionChanged(Element[] items)
        {
            if (items == null || items.Length == 0)
            {
                /// Delete displayControl(s)
            }
            else
            {
                bool isFile = false;
                string type = string.Empty;
                bool sameType = true;

                for (int i = 0; i < items.Length; i++)
                {
                    Element currentElement = items[i];

                    if (i == 0)
                    {
                        isFile = currentElement.IsFile;
                        type = currentElement.Name;
                    }
                    else
                    {
                        if (isFile != currentElement.IsFile && type != currentElement.Name)
                        {
                            sameType = false;
                            break;
                        }
                    }
                }

                if (sameType && isFile)

                    if (Consts.IMAGE_EXTENSIONS.Contains("." + items[0].CurrentFile.ExtractExtension()))
                    {
                        if (pc.SelectedPage != null)
                        {
                            var bt = pc.SelectedPage.CurrentFileSystem.ReadAllBytes(items[0].CurrentFile.GetPath(), pc.SelectedPage.CurrentFileSystem.RootDirectory);
                            if (bt.Result.Success)
                            {
                                // Put bytes into memory stream to convert bytes to image to display in preview
                            }
                        }
                    }
            }
        }
    }
}
