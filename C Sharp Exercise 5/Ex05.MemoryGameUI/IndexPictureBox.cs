using System.Drawing;
using System.Windows.Forms;

namespace Ex05.MemoryGameUI
{
    public class IndexPictureBox : PictureBox
    {
        private readonly int r_HeightIndex;
        private readonly int r_WidthIndex;
        private Image m_IndexPictureBoxImage;

        public IndexPictureBox(int i_HeightIndex, int i_WidthIndex) : base()
        {
            this.r_HeightIndex = i_HeightIndex;
            this.r_WidthIndex = i_WidthIndex;
            SetQuestionMarkImage();
        }

        public int HeightIndex
        {
            get { return this.r_HeightIndex; }
        }

        public int WidthIndex
        {
            get { return this.r_WidthIndex; }
        }

        public Image IndexPictureBoxImage
        {
            get { return this.m_IndexPictureBoxImage; }
            set { this.m_IndexPictureBoxImage = value; }
        }

        public void SetQuestionMarkImage()
        {
            this.Image = Properties.Resources.QuestionMark;
        }

        public void SetIndexPictureBoxImage()
        {
            this.Image = this.m_IndexPictureBoxImage;
        }
    }
}